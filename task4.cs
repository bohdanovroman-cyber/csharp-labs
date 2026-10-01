using System;
using System.Linq;

namespace ClinicApp
{
    public class Task4PatientManager
    {
        private Task1Patient[] patients =
        {
            new Task1Patient(
                1,
                "Олександр",
                "Коваленко",
                BloodType.APositive),

            new Task1Patient(
                2,
                "Марія",
                "Шевченко",
                BloodType.ONegative),

            new Task1Patient(
                3,
                "Іван",
                "Бондаренко",
                BloodType.APositive)
        };

        public Task1Patient FindById(int id)
        {
            return patients.FirstOrDefault(
                p => p.Id == id);
        }

        public bool TryFindById(
            int id,
            out Task1Patient patient)
        {
            patient = FindById(id);
            return patient != null;
        }

        public Task1Patient[] FindByBloodType(
            BloodType bloodType)
        {
            return patients
                .Where(p => p.BloodType == bloodType)
                .ToArray();
        }
    }

    public class Task4DoctorManager
    {
        private Task1Doctor[] doctors =
        {
            new Task1Doctor(
                1,
                "Андрій",
                "Мельник",
                Speciality.Cardiology),

            new Task1Doctor(
                2,
                "Олена",
                "Ткаченко",
                Speciality.General),

            new Task1Doctor(
                3,
                "Сергій",
                "Кравченко",
                Speciality.Surgery)
        };

        public Task1Doctor FindById(int id)
        {
            return doctors.FirstOrDefault(
                d => d.Id == id);
        }

        public bool TryFindById(
            int id,
            out Task1Doctor doctor)
        {
            doctor = FindById(id);
            return doctor != null;
        }

        public Task1Doctor[] FindBySpeciality(
            Speciality speciality)
        {
            return doctors
                .Where(d => d.Speciality == speciality)
                .ToArray();
        }

        public Task1Doctor[] FindBySpeciality(
            string query)
        {
            if (string.IsNullOrWhiteSpace(query))
                return Array.Empty<Task1Doctor>();

            return doctors
                .Where(d =>
                    d.Speciality
                        .ToString()
                        .Contains(
                            query,
                            StringComparison.OrdinalIgnoreCase))
                .ToArray();
        }
    }

    public class Task4AppointmentManager
    {
        private Task1Appointment[] appointments =
        {
            new Task1Appointment(1, 1, 1),
            new Task1Appointment(2, 2, 2)
        };

        public Task1Appointment[] GetByDate(
            DateTime date)
        {
            return appointments
                .Where(a =>
                    a.Id > 0)
                .ToArray();
        }

        public Task1Appointment[] GetByDate(
            int year,
            int month,
            int day)
        {
            return GetByDate(
                new DateTime(year, month, day));
        }
    }
}