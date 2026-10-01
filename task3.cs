using System;
using System.Linq;

namespace ClinicApp
{
    public static class ClinicFormatter
    {
        public static string FormatBloodType(BloodType bt)
        {
            return bt switch
            {
                BloodType.APositive => "A+",
                BloodType.ANegative => "A-",
                BloodType.BPositive => "B+",
                BloodType.BNegative => "B-",
                BloodType.ABPositive => "AB+",
                BloodType.ABNegative => "AB-",
                BloodType.OPositive => "O+",
                BloodType.ONegative => "O-",
                _ => "Невідомо"
            };
        }

        public static string FormatSpeciality(Speciality s)
        {
            return s switch
            {
                Speciality.General => "Загальна медицина",
                Speciality.Cardiology => "Кардіологія",
                Speciality.Surgery => "Хірургія",
                Speciality.Pediatrics => "Педіатрія",
                Speciality.Neurology => "Неврологія",
                Speciality.Dermatology => "Дерматологія",
                Speciality.Dentistry => "Стоматологія",
                Speciality.Ophthalmology => "Офтальмологія",
                _ => s.ToString()
            };
        }

        public static string FormatAge(int age)
        {
            int lastTwo = age % 100;

            if (lastTwo >= 11 && lastTwo <= 19)
                return $"{age} років";

            int last = age % 10;

            if (last == 1)
                return $"{age} рік";

            if (last >= 2 && last <= 4)
                return $"{age} роки";

            return $"{age} років";
        }

        public static string FormatPhone(string phone)
        {
            if (string.IsNullOrWhiteSpace(phone))
                return phone;

            string digits = new string(
                phone.Where(char.IsDigit).ToArray());

            if (digits.StartsWith("380") &&
                digits.Length == 12)
            {
                digits = "0" + digits.Substring(3);
            }

            if (digits.Length == 10)
            {
                return $"({digits.Substring(0, 3)}) " +
                       $"{digits.Substring(3, 3)}-" +
                       $"{digits.Substring(6, 4)}";
            }

            return phone;
        }
    }

    public class Task3PatientManager
    {
        private Task1Patient[] patients =
            Array.Empty<Task1Patient>();

        public void Add(Task1Patient patient)
        {
            Array.Resize(
                ref patients,
                patients.Length + 1);

            patients[patients.Length - 1] = patient;
        }

        public Task1Patient this[int index]
        {
            get
            {
                if (index < 0 || index >= patients.Length)
                    return null;

                return patients[index];
            }
        }
    }

    public class Task3DoctorManager
    {
        private Task1Doctor[] doctors =
            Array.Empty<Task1Doctor>();

        public void Add(Task1Doctor doctor)
        {
            Array.Resize(
                ref doctors,
                doctors.Length + 1);

            doctors[doctors.Length - 1] = doctor;
        }

        public Task1Doctor this[int index]
        {
            get
            {
                if (index < 0 || index >= doctors.Length)
                    return null;

                return doctors[index];
            }
        }
    }

    public class Task3AppointmentManager
    {
        private Task1Appointment[] appointments =
            Array.Empty<Task1Appointment>();

        public void Add(Task1Appointment appointment)
        {
            Array.Resize(
                ref appointments,
                appointments.Length + 1);

            appointments[appointments.Length - 1] =
                appointment;
        }

        public Task1Appointment this[int index]
        {
            get
            {
                if (index < 0 ||
                    index >= appointments.Length)
                    return null;

                return appointments[index];
            }
        }
    }
}