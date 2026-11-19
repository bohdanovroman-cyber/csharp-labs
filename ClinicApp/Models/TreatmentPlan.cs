using ClinicApp.Attributes;
using ClinicApp.Enums;

namespace ClinicApp.Models;

public class TreatmentPlan
{
    private static int _counter = 0;

    public int Id { get; private set; }

    [Required]
    [MaxLength(100)]
    public string Diagnosis { get; set; } = string.Empty;

    [MaxLength(500)]
    public string Description { get; set; } = string.Empty;

    [MinValue(1)]
    public int DurationDays { get; set; }

    [MinValue(0)]
    public decimal Cost { get; set; }

    public TreatmentStatus Status { get; private set; }

    public TreatmentPlan()
    {
        Id = ++_counter;
        Status = TreatmentStatus.Planned;
    }

    public bool Activate()
    {
        if (Status != TreatmentStatus.Planned) return false;
        Status = TreatmentStatus.Active;
        return true;
    }

    public bool Complete()
    {
        if (Status != TreatmentStatus.Active) return false;
        Status = TreatmentStatus.Completed;
        return true;
    }

    public bool Cancel()
    {
        if (Status != TreatmentStatus.Planned && Status != TreatmentStatus.Active) return false;
        Status = TreatmentStatus.Cancelled;
        return true;
    }

    public override string ToString() =>
        $"#{Id} {Diagnosis} | {DurationDays} дн. | {Cost} грн | {Status}";
}