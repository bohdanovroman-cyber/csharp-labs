using System;

namespace ClinicApp
{
    public class Clinic
    {
        public PatientManager Patients { get; } = new PatientManager();
        public DoctorManager Doctors { get; } = new DoctorManager();
        public AppointmentManager Appointments { get; }

        public Clinic()
        {
            Appointments = new AppointmentManager(Patients, Doctors);
        }

        public void DisplaySchedule()
        {
            Doctors.DisplayAll();
            Appointments.DisplayAll();
        }

        public void GenerateReport()
        {
            Console.WriteLine($"Пацієнтів: {Patients.Count}, Лікарів: {Doctors.Count}, Записів: {Appointments.Count}");
        }
    }
}