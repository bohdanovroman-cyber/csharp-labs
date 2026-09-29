using System;

namespace ClinicApp
{
    public class Doctor
    {
        private static int _nextId = 1;

        public int Id { get; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Speciality { get; set; }
        public string LicenseNumber { get; set; }
        public string Phone { get; set; }
        public int WorkStartHour { get; set; }
        public int WorkEndHour { get; set; }

        public string FullName => $"{FirstName} {LastName}";
        public int WorkingHoursPerDay => WorkEndHour - WorkStartHour;
        public string WorkSchedule => $"{WorkStartHour:D2}:00-{WorkEndHour:D2}:00";

        public bool CanAcceptAt(int hour) => hour >= WorkStartHour && hour < WorkEndHour;
        public bool IsAvailableNow => CanAcceptAt(DateTime.Now.Hour);

        public Doctor(string firstName, string lastName, string speciality, string licenseNumber, string phone, int workStartHour = 8, int workEndHour = 17)
        {
            Id = _nextId++;
            FirstName = firstName;
            LastName = lastName;
            Speciality = speciality;
            LicenseNumber = licenseNumber;
            Phone = phone;
            WorkStartHour = workStartHour;
            WorkEndHour = workEndHour;
        }

        public Doctor(string firstName, string lastName, string speciality)
            : this(firstName, lastName, speciality, "LIC-000", "0000000000", 8, 17) { }

        public Doctor()
            : this("Невідомий", "Лікар", "Загальна практика") { }

        public override string ToString()
        {
            string status = IsAvailableNow ? "доступний зараз" : "не в робочий час";
            return $"[{Id}] {FullName} | {Speciality} | {LicenseNumber} | Тел: {Phone} | {WorkSchedule} ({WorkingHoursPerDay} год) | {status}";
        }
    }
}