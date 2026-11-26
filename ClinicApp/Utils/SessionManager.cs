using System;
using System.IO;
using System.Linq;
using System.Collections.Generic;
using System.Globalization;

namespace ClinicApp.Utils
{
    public class SessionManager
    {
        private readonly string _filePath;

        public SessionManager(string filePath = "session.dat")
        {
            _filePath = filePath;
        }

        public void SavePatients(Clinic clinic)
        {
            try
            {
                using (StreamWriter writer = new StreamWriter(_filePath, false))
                {
                    writer.WriteLine("[Patients]");

                    foreach (Patient patient in clinic.Patients.GetAll())
                    {
                        writer.WriteLine(
                            $"{patient.FirstName}|" +
                            $"{patient.LastName}|" +
                            $"{patient.DateOfBirth:yyyy-MM-dd}|" +
                            $"{patient.Phone}|" +
                            $"{patient.Email}|" +
                            $"{patient.BloodType}");
                    }
                }

                Console.WriteLine($"Сесію збережено: {_filePath}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Помилка збереження сесії: {ex.Message}");
            }
        }

        public void LoadPatients(Clinic clinic)
        {
            if (!File.Exists(_filePath))
            {
                Console.WriteLine("Файл сесії не знайдено.");
                return;
            }

            int loaded = 0;
            int skipped = 0;
            bool inPatientsSection = false;

            try
            {
                string[] lines = File.ReadAllLines(_filePath);

                for (int i = 0; i < lines.Length; i++)
                {
                    string line = lines[i].Trim();

                    if (string.IsNullOrWhiteSpace(line))
                        continue;

                    if (line.StartsWith("[") && line.EndsWith("]"))
                    {
                        inPatientsSection = line.Equals(
                            "[Patients]",
                            StringComparison.OrdinalIgnoreCase);

                        continue;
                    }

                    if (!inPatientsSection)
                        continue;

                    try
                    {
                        string[] parts = line.Split('|');

                        if (parts.Length != 6)
                            throw new FormatException("Неправильна кількість полів.");

                        string firstName = parts[0].Trim();
                        string lastName = parts[1].Trim();
                        string dateText = parts[2].Trim();
                        string phone = parts[3].Trim();
                        string email = parts[4].Trim();
                        string bloodType = parts[5].Trim();

                        if (string.IsNullOrWhiteSpace(firstName) ||
                            string.IsNullOrWhiteSpace(lastName))
                            throw new FormatException("Ім'я або прізвище відсутні.");

                        if (!DateTime.TryParseExact(
                            dateText,
                            "yyyy-MM-dd",
                            CultureInfo.InvariantCulture,
                            DateTimeStyles.None,
                            out DateTime birthDate))
                            throw new FormatException("Неправильний формат дати.");

                        if (birthDate.Date > DateTime.Today)
                            throw new FormatException("Дата народження в майбутньому.");

                        if (string.IsNullOrWhiteSpace(phone) ||
                            string.IsNullOrWhiteSpace(email) ||
                            string.IsNullOrWhiteSpace(bloodType))
                            throw new FormatException("Відсутні обов'язкові поля.");

                        bool duplicate = clinic.Patients.GetAll().Any(p =>
                            p.FirstName.Equals(firstName, StringComparison.OrdinalIgnoreCase) &&
                            p.LastName.Equals(lastName, StringComparison.OrdinalIgnoreCase) &&
                            p.DateOfBirth.Date == birthDate.Date);

                        if (duplicate)
                        {
                            skipped++;
                            Console.WriteLine($"Рядок {i + 1}: пацієнт вже існує.");
                            continue;
                        }

                        Patient patient = new Patient(
                            firstName,
                            lastName,
                            birthDate,
                            phone,
                            email,
                            bloodType
                        );

                        clinic.Patients.Add(patient);
                        loaded++;
                    }
                    catch (Exception ex)
                    {
                        skipped++;
                        Console.WriteLine($"Рядок {i + 1} пропущено: {ex.Message}");
                    }
                }

                Console.WriteLine($"Завантажено пацієнтів: {loaded}");
                Console.WriteLine($"Пропущено рядків: {skipped}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Помилка завантаження сесії: {ex.Message}");
            }
        }

        public bool Exists()
        {
            return File.Exists(_filePath);
        }
    }
}