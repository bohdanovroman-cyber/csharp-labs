using ClinicApp.Models;

namespace ClinicApp.Events;

public class AppointmentEventArgs : EventArgs
{
    public int AppointmentId { get; }
    public int PatientId { get; }
    public int DoctorId { get; }
    public DateTime ScheduledAt { get; }
    public int DurationMinutes { get; }
    public decimal Cost { get; }
    public string Description { get; }
    public bool IsUrgent { get; }
    public string Reason { get; }

    public AppointmentEventArgs(Appointment appointment)
    {
        AppointmentId = appointment.Id;
        PatientId = appointment.PatientId;
        DoctorId = appointment.DoctorId;
        ScheduledAt = appointment.ScheduledAt;
        DurationMinutes = appointment.DurationMinutes;
        Cost = appointment.GetCost();
        Description = appointment.GetDescription();
        IsUrgent = appointment is UrgentAppointment;
        Reason = appointment.CancellationReason;
    }
}