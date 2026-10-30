namespace ClinicApp.Models;

public class UrgentAppointment : Appointment
{
    // Причина терміновості: тільки читання, задається в конструкторі
    public string UrgencyNote { get; }

    public UrgentAppointment(
        int patientId,
        int doctorId,
        DateTime scheduledAt,
        string urgencyNote = "",
        int durationMinutes = 30)
        : base(patientId, doctorId, scheduledAt, durationMinutes)
    {
        UrgencyNote = urgencyNote ?? "";
    }

    // Ціна будується від базової: base.GetCost() + 50%
    public override decimal GetCost()
    {
        return base.GetCost() * 1.5m;
    }

    // sealed override: нащадки цього класу вже не зможуть змінити опис
    public sealed override string GetDescription()
    {
        if (string.IsNullOrWhiteSpace(UrgencyNote))
            return "Терміновий";

        return $"Терміновий ({UrgencyNote})";
    }

    // new: це ХОВАЄ метод батька, а не перевизначає його
    public new int GetPriority()
    {
        return 1;
    }
}