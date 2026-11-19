using ClinicApp.Models;
using ClinicApp.Utils;

namespace ClinicApp.Managers;

public class TreatmentPlanManager
{
    private readonly List<TreatmentPlan> _plans = new();

    public IReadOnlyList<TreatmentPlan> Plans => _plans;

    public bool Add(TreatmentPlan plan) => Add(plan, out _);

    public bool Add(TreatmentPlan plan, out ValidationResult validation)
    {
        validation = ModelValidator.Validate(plan);
        if (!validation.IsValid) return false;

        _plans.Add(plan);
        return true;
    }

    public TreatmentPlan? GetById(int id) => _plans.FirstOrDefault(p => p.Id == id);
}