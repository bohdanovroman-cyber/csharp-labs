using ClinicApp.Enums;
using ClinicApp.Interfaces;
using ClinicApp.Utils;

namespace ClinicApp.Models;

public class Appointment : IPayable, ICancellable
{
    private const decimal PricePerMinute = 10m;

    private static int _nextId = 1;

    private int durationMinutes;
    private bool _isPaid;
    private string _cancellationReason = "";

    public int Id { get; }

    public int PatientId { get; set; }

    public int DoctorId { get; set; }

    public DateTime ScheduledAt { get; set; }

    public int DurationMinutes
    {
        get { return durationMinutes; }
        set
        {
            ClinicValidator.ValidatePositive(
                value,
                nameof(DurationMinutes));

            durationMinutes = value;
        }
    }

    public AppointmentStatus Status { get; private set; }

    public string Notes { get; private set; } = "";

    public DateTime EndsAt => ScheduledAt.AddMinutes(DurationMinutes);

    public bool IsUpcoming =>
        Status == AppointmentStatus.Scheduled &&
        ScheduledAt > DateTime.Now;

    public bool IsPaid => _isPaid;

    public bool IsCancelled => Status == AppointmentStatus.Cancelled;

    public string CancellationReason =>
        IsCancelled ? _cancellationReason : "";

    public Appointment(
        int patientId,
        int doctorId,
        DateTime scheduledAt,
        int durationMinutes = 30)
    {
        Id = _nextId++;
        PatientId = patientId;
        DoctorId = doctorId;
        ScheduledAt = scheduledAt;
        DurationMinutes = durationMinutes;
        Status = AppointmentStatus.Scheduled;
    }

    public bool Cancel(string reason = "")
    {
        if (Status != AppointmentStatus.Scheduled)
            return false;

        Status = AppointmentStatus.Cancelled;
        _cancellationReason = reason ?? "";
        Notes = _cancellationReason;

        return true;
    }

    public bool Complete()
    {
        if (Status != AppointmentStatus.Scheduled)
            return false;

        Status = AppointmentStatus.Completed;

        return true;
    }

    public decimal GetCost()
    {
        return DurationMinutes * PricePerMinute;
    }

    public void MarkPaid()
    {
        if (IsCancelled)
        {
            throw new InvalidOperationException(
                "Неможливо оплатити скасований запис.");
        }

        _isPaid = true;
    }

    public override string ToString()
    {
        return $"[{Id}] Пацієнт #{PatientId} -> Лікар #{DoctorId} | " +
               $"{ScheduledAt:dd.MM.yyyy HH:mm}-{EndsAt:HH:mm} | {Status}";
    }
}