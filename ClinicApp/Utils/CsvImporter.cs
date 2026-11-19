using System;
using System.IO;
using System.Globalization;

namespace ClinicApp.Utils
{
    public class CsvImporter
    {
        public ImportResult ImportPatients(Clinic clinic, string filePath)
        {
            ImportResult result = new ImportResult();

            if (!File.Exists(filePath))
            {
                result.AddError(0, $"Файл не знайдено: {filePath}");
                return result;
            }

            string[] lines;

            try
            {
                lines = File.ReadAllLines(filePath);
            }
            catch (Exception ex)
            {
                result.AddError(0, $"Не вдалося прочитати файл: {ex.Message}");
                return result;
            }

            for (int i = 1; i < lines.Length; i++)
            {
                string line = lines[i].Trim();

                if (string.IsNullOrWhiteSpace(line))
                    continue;

                try
                {
                    string[] parts = line.Split(',');

                    if (parts.Length != 6)
                        throw new FormatException("Має бути 6 полів.");

                    string firstName = parts[0].Trim();
                    string lastName = parts[1].Trim();
                    string dateText = parts[2].Trim();
                    string phone = parts[3].Trim();
                    string email = parts[4].Trim();
                    string bloodType = parts[5].Trim();

                    if (string.IsNullOrWhiteSpace(firstName) ||
                        string.IsNullOrWhiteSpace(lastName))
                    {
                        throw new FormatException("Ім'я або прізвище відсутні.");
                    }

                    if (!DateTime.TryParseExact(
                        dateText,
                        "yyyy-MM-dd",
                        CultureInfo.InvariantCulture,
                        DateTimeStyles.None,
                        out DateTime birthDate))
                    {
                        throw new FormatException("Неправильний формат дати. Очікується yyyy-MM-dd.");
                    }

                    if (birthDate.Date > DateTime.Today)
                        throw new FormatException("Дата народження в майбутньому.");

                    if (string.IsNullOrWhiteSpace(phone))
                        throw new FormatException("Телефон відсутній.");

                    if (string.IsNullOrWhiteSpace(email))
                        throw new FormatException("Email відсутній.");

                    if (string.IsNullOrWhiteSpace(bloodType))
                        throw new FormatException("Група крові відсутня.");

                    Patient patient = new Patient(
                        firstName,
                        lastName,
                        birthDate,
                        phone,
                        email,
                        bloodType
                    );

                    clinic.Patients.Add(patient);
                    result.AddImported();
                }
                catch (Exception ex)
                {
                    result.AddError(i + 1, ex.Message);
                }
            }

            return result;
        }
    }
}