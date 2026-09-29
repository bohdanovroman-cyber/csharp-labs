using System;

namespace ClinicApp
{
    public class DoctorManager
    {
        private const int MaxDoctors = 50;
        private Doctor[] _doctors = new Doctor[MaxDoctors];
        private int _count = 0;

        public int Count => _count;

        public void Add(Doctor doctor)
        {
            if (_count < MaxDoctors)
            {
                _doctors[_count++] = doctor;
                Console.WriteLine($"Лікаря [{doctor.Id}] {doctor.FullName} додано.");
            }
        }

        public Doctor? FindById(int id)
        {
            for (int i = 0; i < _count; ++i)
                if (_doctors[i].Id == id) return _doctors[i];
            return null;
        }

        public void DisplayAll()
        {
            if (_count == 0) { Console.WriteLine("Список лікарів порожній."); return; }
            Console.WriteLine($"=== Лікарі ({_count} / {MaxDoctors}) ===");
            for (int i = 0; i < _count; ++i) Console.WriteLine(_doctors[i]);
        }

        public void DisplayStats()
        {
            Console.WriteLine($"\n=== Статистика лікарів ===");
            Console.WriteLine($"Всього лікарів: {_count}");
        }
    }
}