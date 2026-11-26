using ClinicApp.Models;

namespace ClinicApp.Events;

public class TreatmentPlanEventArgs : EventArgs
{
    public int PlanId { get; }
    public int PatientId { get; }
    public string Diagnosis { get; }
    public decimal Cost { get; }

    public TreatmentPlanEventArgs(TreatmentPlan plan)
    {
        PlanId = plan.Id;
        PatientId = plan.PatientId;
        Diagnosis = plan.Diagnosis;
        Cost = plan.Cost;
    }
}