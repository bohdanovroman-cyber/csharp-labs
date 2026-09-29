using System;

namespace ClinicApp
{
    public class AppointmentManager
    {
        private const int MaxAppointments = 200;
        private Appointment[] _appointments = new Appointment[MaxAppointments];
        private int _count = 0;

        private PatientManager _patientManager;
        private DoctorManager _doctorManager;

        public int Count => _count;

        public AppointmentManager(PatientManager patientManager, DoctorManager doctorManager)
        {
            _patientManager = patientManager;
            _doctorManager = doctorManager;
        }

        public bool CreateAppointment(int patientId, int doctorId, DateTime dateTime)
        {
            var patient = _patientManager.FindById(patientId);
            var doctor = _doctorManager.FindById(doctorId);

            if (patient == null || doctor == null || !doctor.CanAcceptAt(dateTime.Hour))
                return false;

            if (_count < MaxAppointments)
            {
                _appointments[_count++] = new Appointment(patientId, doctorId, dateTime);
                return true;
            }
            return false;
        }

        public void DisplayAll()
        {
            Console.WriteLine($"=== Записи на прийом ({_count}) ===");
            for (int i = 0; i < _count; ++i) Console.WriteLine(_appointments[i]);
        }
    }
}