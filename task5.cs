using System;

namespace ClinicApp
{
    public class Appointment
    {
        private static int _nextId = 1;

        public int Id { get; }
        public int PatientId { get; }
        public int DoctorId { get; }
        public DateTime DateTime { get; set; }
        public string Status { get; private set; }

        public Appointment(int patientId, int doctorId, DateTime dateTime)
        {
            Id = _nextId++;
            PatientId = patientId;
            DoctorId = doctorId;
            DateTime = dateTime;
            Status = "Scheduled";
        }

        public bool ChangeStatus(string newStatus)
        {
            if (Status == "Scheduled" && (newStatus == "Cancelled" || newStatus == "Completed"))
            {
                Status = newStatus;
                return true;
            }
            return false;
        }

        public override string ToString()
        {
            return $"Запис [{Id}] | Пацієнт ID: {PatientId} -> Лікар ID: {DoctorId} | Час: {DateTime:dd.MM.yyyy HH:mm} | Статус: {Status}";
        }
    }
}