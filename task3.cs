using System;

namespace ClinicApp
{
    public class PatientManager
    {
        private const int MaxPatients = 100;
        private Patient[] _patients = new Patient[MaxPatients];
        private int _count = 0;

        public int Count => _count;

        public void Add(Patient patient)
        {
            if (_count < MaxPatients)
            {
                _patients[_count++] = patient;
                Console.WriteLine($"Пацієнта [{patient.Id}] {patient.FullName} додано.");
            }
            else
            {
                Console.WriteLine($"Не вдалося додати пацієнта, ліміт ({MaxPatients}).");
            }
        }

        public Patient? FindById(int id)
        {
            for (int i = 0; i < _count; ++i)
                if (_patients[i].Id == id) return _patients[i];
            return null;
        }

        public Patient[] FindByName(string name)
        {
            int matchCount = 0;
            for (int i = 0; i < _count; ++i)
                if (_patients[i].FirstName.Contains(name, StringComparison.OrdinalIgnoreCase) ||
                    _patients[i].LastName.Contains(name, StringComparison.OrdinalIgnoreCase))
                    matchCount++;

            Patient[] result = new Patient[matchCount];
            int index = 0;
            for (int i = 0; i < _count; ++i)
                if (_patients[i].FirstName.Contains(name, StringComparison.OrdinalIgnoreCase) ||
                    _patients[i].LastName.Contains(name, StringComparison.OrdinalIgnoreCase))
                    result[index++] = _patients[i];

            return result;
        }

        public bool Remove(int id)
        {
            for (int i = 0; i < _count; ++i)
            {
                if (_patients[i].Id == id)
                {
                    for (int j = i; j < _count - 1; ++j)
                        _patients[j] = _patients[j + 1];
                    _count--;
                    _patients[_count] = null!;
                    return true;
                }
            }
            return false;
        }

        public void DisplayAll()
        {
            if (_count == 0) { Console.WriteLine("Список пацієнтів порожній."); return; }
            Console.WriteLine($"=== Пацієнти ({_count} / {MaxPatients}) ===");
            for (int i = 0; i < _count; ++i) Console.WriteLine(_patients[i]);
        }

        public void DisplayStats()
        {
            if (_count == 0) return;
            double sumAge = 0;
            int smallestIdx = 0, biggestIdx = 0, adults = 0;

            for (int i = 0; i < _count; ++i)
            {
                sumAge += _patients[i].Age;
                if (_patients[i].Age < _patients[smallestIdx].Age) smallestIdx = i;
                if (_patients[i].Age > _patients[biggestIdx].Age) biggestIdx = i;
                if (_patients[i].IsAdult) adults++;
            }

            Console.WriteLine("\n=== Статистика пацієнтів ===");
            Console.WriteLine($"Всього: {_count}");
            Console.WriteLine($"Середній вік: {sumAge / _count:F1} р.");
            Console.WriteLine($"Наймолодший: {_patients[smallestIdx].FullName} ({_patients[smallestIdx].Age} р.)");
            Console.WriteLine($"Найстарший: {_patients[biggestIdx].FullName} ({_patients[biggestIdx].Age} р.)");
            Console.WriteLine($"Дорослих: {adults} з {_count}");
        }
    }
}