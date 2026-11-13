using ClinicApp.Models;

namespace ClinicApp.Managers;

public class AnalyticsManager
{
    private readonly AppointmentManager _appointments;
    private readonly DoctorManager _doctors;
    private readonly PatientManager _patients;

    public AnalyticsManager(
        AppointmentManager appointments,
        DoctorManager doctors,
        PatientManager patients)
    {
        _appointments = appointments;
        _doctors = doctors;
        _patients = patients;
    }

    // Лінива послідовність: статистика одного лікаря рахується лише тоді,
    // коли foreach просить наступний елемент
    public IEnumerable<DoctorStats> ComputeDoctorStats()
    {
        Appointment[] allAppointments = _appointments.GetAll();
        Doctor[] doctors = _doctors.GetAll();

        foreach (Doctor doctor in doctors)
        {
            int count = 0;
            decimal revenue = 0;
            DateTime last = DateTime.MinValue;

            foreach (Appointment appointment in allAppointments)
            {
                if (appointment.DoctorId != doctor.Id)
                    continue;

                count++;
                revenue += appointment.GetCost();

                if (appointment.ScheduledAt > last)
                    last = appointment.ScheduledAt;
            }

            // Лікар без прийомів теж повертається: 0, 0, 01.01.0001
            yield return new DoctorStats(
                doctor.Id,
                doctor.FullName,
                count,
                revenue,
                last);
        }
    }

    public IEnumerable<PatientStats> ComputePatientStats()
    {
        Appointment[] allAppointments = _appointments.GetAll();
        Patient[] patients = _patients.GetAll();

        foreach (Patient patient in patients)
        {
            int count = 0;
            decimal spent = 0;
            DateTime last = DateTime.MinValue;

            foreach (Appointment appointment in allAppointments)
            {
                if (appointment.PatientId != patient.Id)
                    continue;

                count++;
                spent += appointment.GetCost();

                if (appointment.ScheduledAt > last)
                    last = appointment.ScheduledAt;
            }

            yield return new PatientStats(
                patient.Id,
                patient.FullName,
                count,
                spent,
                last);
        }
    }
}