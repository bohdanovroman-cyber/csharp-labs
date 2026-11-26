using ClinicApp.Events;
using ClinicApp.Models;
using ClinicApp.Utils;

namespace ClinicApp.Managers;

public class TreatmentPlanManager
{
    private readonly List<TreatmentPlan> _plans = new();

    public IReadOnlyList<TreatmentPlan> Plans => _plans;

    public event EventHandler<TreatmentPlanEventArgs>? PlanCompleted;

    public bool Add(TreatmentPlan plan) => Add(plan, out _);

    public bool Add(TreatmentPlan plan, out ValidationResult validation)
    {
        validation = ModelValidator.Validate(plan);
        if (!validation.IsValid) return false;

        _plans.Add(plan);
        return true;
    }

    public TreatmentPlan? GetById(int id) => _plans.FirstOrDefault(p => p.Id == id);

    // Подія піднімається лише тоді, коли план справді завершено
    public bool Complete(int planId)
    {
        TreatmentPlan? plan = GetById(planId);

        if (plan == null || !plan.Complete())
            return false;

        PlanCompleted?.Invoke(this, new TreatmentPlanEventArgs(plan));
        return true;
    }
}