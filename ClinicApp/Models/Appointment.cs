using ClinicApp.Enums;
using ClinicApp.Utils;

namespace ClinicApp.Models;

public class Appointment
{
    private static int nextId = 1;

    private int durationMinutes;

    public int Id { get; }

    public int PatientId { get; }

    public int DoctorId { get; }

    public DateTime ScheduledAt { get; set; }

    public int DurationMinutes
    {
        get => durationMinutes;
        set
        {
            ClinicValidator.ValidatePositive(
                value,
                nameof(DurationMinutes));

            durationMinutes = value;
        }
    }

    public AppointmentStatus Status { get; private set; }

    public string Notes { get; private set; }

    public DateTime EndsAt =>
        ScheduledAt.AddMinutes(DurationMinutes);

    public bool IsUpcoming =>
        ScheduledAt > DateTime.Now &&
        Status == AppointmentStatus.Scheduled;

    public Appointment(
        int patientId,
        int doctorId,
        DateTime scheduledAt,
        int durationMinutes = 30)
    {
        Id = nextId++;

        PatientId = patientId;
        DoctorId = doctorId;
        ScheduledAt = scheduledAt;
        DurationMinutes = durationMinutes;

        Status = AppointmentStatus.Scheduled;
        Notes = "";
    }

    public Appointment()
        : this(
            0,
            0,
            DateTime.Now)
    {
    }

    public bool Cancel(string reason = "")
    {
        if (Status != AppointmentStatus.Scheduled)
            return false;

        Status = AppointmentStatus.Cancelled;

        if (!string.IsNullOrWhiteSpace(reason))
        {
            Notes =
                $"Причина скасування: {reason}";
        }

        return true;
    }

    public bool Complete()
    {
        if (Status != AppointmentStatus.Scheduled)
            return false;

        Status = AppointmentStatus.Completed;

        return true;
    }

    public override string ToString()
    {
        string text =
            $"[{Id}] Пацієнт #{PatientId} -> " +
            $"Лікар #{DoctorId} | " +
            $"{ScheduledAt:dd.MM.yyyy HH:mm}-" +
            $"{EndsAt:HH:mm} | {Status}";

        if (!string.IsNullOrEmpty(Notes))
        {
            text += $" | {Notes}";
        }

        return text;
    }
}