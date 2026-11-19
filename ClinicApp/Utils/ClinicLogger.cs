using System;
using System.IO;
using System.Linq;

namespace ClinicApp.Utils
{
    public class ClinicLogger
    {
        private readonly string _filePath;

        public ClinicLogger(string filePath = "clinic.log")
        {
            _filePath = filePath;
        }

        public void LogInfo(string message)
        {
            WriteLog("INFO", message);
        }

        public void LogWarning(string message)
        {
            WriteLog("WARNING", message);
        }

        public void LogError(string message)
        {
            WriteLog("ERROR", message);
        }

        private void WriteLog(string level, string message)
        {
            string entry =
                $"[{DateTime.Now:yyyy-MM-dd HH:mm:ss}] [{level}] {message}";

            File.AppendAllText(_filePath, entry + Environment.NewLine);
        }

        public string[] GetLastLines(int count)
        {
            if (count <= 0)
                return Array.Empty<string>();

            if (!File.Exists(_filePath))
                return Array.Empty<string>();

            return File.ReadAllLines(_filePath)
                .TakeLast(count)
                .ToArray();
        }

        public void Clear()
        {
            File.WriteAllText(_filePath, string.Empty);
        }
    }
}