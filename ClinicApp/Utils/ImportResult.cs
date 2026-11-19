using System;
using System.Collections.Generic;

namespace ClinicApp.Utils
{
    public class ImportResult
    {
        public int ImportedCount { get; private set; }

        public List<string> Errors { get; } = new List<string>();

        public void AddImported()
        {
            ImportedCount++;
        }

        public void AddError(int lineNumber, string reason)
        {
            Errors.Add($"Рядок {lineNumber}: {reason}");
        }

        public override string ToString()
        {
            return $"Імпорт завершено.\n" +
                   $"Успішно імпортовано: {ImportedCount}\n" +
                   $"Помилок: {Errors.Count}";
        }

        public void Display()
        {
            Console.WriteLine(ToString());

            if (Errors.Count > 0)
            {
                Console.WriteLine("\nПомилки:");

                foreach (string error in Errors)
                    Console.WriteLine(error);
            }
        }
    }
}