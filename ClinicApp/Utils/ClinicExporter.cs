using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using ClinicApp;

namespace ClinicApp.Utils
{
    public class ClinicExporter
    {
        private readonly Clinic _clinic;
        private readonly string _baseDirectory;

        public ClinicExporter(Clinic clinic, string baseDirectory = "reports")
        {
            _clinic = clinic;
            _baseDirectory = baseDirectory;
        }

        private string CreateReport(string fileName, string title, IEnumerable<string> lines)
        {
            string dateFolder = DateTime.Now.ToString("yyyy-MM-dd");
            string folderPath = Path.Combine(_baseDirectory, dateFolder);

            Directory.CreateDirectory(folderPath);

            string filePath = Path.Combine(folderPath, fileName);

            using (StreamWriter writer = new StreamWriter(filePath))
            {
                writer.WriteLine("================================");
                writer.WriteLine(title);
                writer.WriteLine($"Дата генерації: {DateTime.Now:yyyy-MM-dd HH:mm:ss}");
                writer.WriteLine("================================");
                writer.WriteLine();

                foreach (string line in lines)
                {
                    writer.WriteLine(line);
                }

                writer.WriteLine();
                writer.WriteLine("================================");
                writer.WriteLine("Звіт сформовано автоматично.");
            }

            return Path.GetFullPath(filePath);
        }

        public string ExportPatients()
        {
            var patients = _clinic.Patients.GetAll();

            return CreateReport(
                "patients.txt",
                "ЗВІТ ПРО ПАЦІЄНТІВ",
                patients.Select(p => p.ToString() ?? ""));
        }

        public string ExportDoctors()
        {
            var doctors = _clinic.Doctors.GetAll();

            return CreateReport(
                "doctors.txt",
                "ЗВІТ ПРО ЛІКАРІВ",
                doctors.Select(d => d.ToString() ?? ""));
        }

        public string ExportAppointments()
        {
            var appointments = _clinic.Appointments.GetAll();

            return CreateReport(
                "appointments.txt",
                "ЗВІТ ПРО ПРИЙОМИ",
                appointments.Select(a => a.ToString() ?? ""));
        }

        public string ExportSummary()
        {
            List<string> lines = new List<string>
            {
                $"Кількість пацієнтів: {_clinic.Patients.Count}",
                $"Кількість лікарів: {_clinic.Doctors.Count}",
                $"Кількість записів: {_clinic.Appointments.GetAll().Length}",
                $"Загальний борг: {_clinic.Billing.GetTotalDebt():F2} грн"
            };

            return CreateReport(
                "summary.txt",
                "ЗАГАЛЬНИЙ ЗВІТ КЛІНІКИ",
                lines);
        }

        public string[] ExportAll()
        {
            return new[]
            {
                ExportPatients(),
                ExportDoctors(),
                ExportAppointments(),
                ExportSummary()
            };
        }
    }
}