using System;

namespace ClinicApp
{
    public enum AppointmentStatus
    {
        Scheduled,
        Cancelled,
        Completed
    }

    public enum BloodType
    {
        Unknown,
        APositive,
        ANegative,
        BPositive,
        BNegative,
        ABPositive,
        ABNegative,
        OPositive,
        ONegative
    }

    public enum Speciality
    {
        General,
        Cardiology,
        Surgery,
        Pediatrics,
        Neurology,
        Dermatology,
        Dentistry,
        Ophthalmology
    }

    public class Task1Patient
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public BloodType BloodType { get; set; }

        public Task1Patient(
            int id,
            string firstName,
            string lastName,
            BloodType bloodType)
        {
            Id = id;
            FirstName = firstName;
            LastName = lastName;
            BloodType = bloodType;
        }
    }

    public class Task1Doctor
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public Speciality Speciality { get; set; }

        public Task1Doctor(
            int id,
            string firstName,
            string lastName,
            Speciality speciality)
        {
            Id = id;
            FirstName = firstName;
            LastName = lastName;
            Speciality = speciality;
        }
    }

    public class Task1Appointment
    {
        public int Id { get; set; }
        public int PatientId { get; set; }
        public int DoctorId { get; set; }
        public AppointmentStatus Status { get; set; }

        public Task1Appointment(
            int id,
            int patientId,
            int doctorId)
        {
            Id = id;
            PatientId = patientId;
            DoctorId = doctorId;
            Status = AppointmentStatus.Scheduled;
        }
    }
}