using System.Text;
using ClinicApp.Events;
using ClinicApp.Models;

namespace ClinicApp.Utils;

public class PatientPassportWriter
{
    private readonly Clinic _clinic;
    private readonly string _folder;

    public PatientPassportWriter(Clinic clinic, string folder = "patients")
    {
        _clinic = clinic;
        _folder = folder;

        Directory.CreateDirectory(_folder);
    }

    // Три публічні обробники, усі ведуть до одного методу запису
    public void OnPatientAdded(object? sender, PatientEventArgs e)
    {
        WritePassport(e.PatientId);
    }

    public void OnAppointmentCompleted(object? sender, AppointmentEventArgs e)
    {
        WritePassport(e.PatientId);
    }

    public void OnPlanCompleted(object? sender, TreatmentPlanEventArgs e)
    {
        WritePassport(e.PatientId);
    }

    private void WritePassport(int patientId)
    {
        Patient? patient = _clinic.Patients.FindById(patientId);

        // Пацієнта не знайдено: нічого не пишемо
        if (patient == null)
            return;

        StringBuilder sb = new();

        sb.AppendLine($"=== Паспорт пацієнта #{patient.Id} ===");
        sb.AppendLine($"ПІБ: {patient.FullName}");
        sb.AppendLine($"Дата народження: {patient.DateOfBirth:dd.MM.yyyy}");
        sb.AppendLine($"Група крові: {patient.BloodType}");
        sb.AppendLine($"Телефон: {patient.Phone}");
        sb.AppendLine($"Email: {patient.Email}");

        Appointment[] appointments = _clinic.Appointments.GetByPatient(patientId);

        sb.AppendLine();
        sb.AppendLine($"Записи ({appointments.Length}):");

        foreach (Appointment appointment in appointments)
        {
            sb.AppendLine($"  {appointment}");
        }

        sb.AppendLine();
        sb.AppendLine($"Борг: {_clinic.Billing.GetPatientDebt(patientId):F2} грн");

        sb.AppendLine();
        sb.AppendLine("Плани лікування:");

        bool anyPlan = false;

        foreach (TreatmentPlan plan in _clinic.TreatmentPlans.Plans)
        {
            if (plan.PatientId == patientId)
            {
                sb.AppendLine($"  {plan}");
                anyPlan = true;
            }
        }

        if (!anyPlan)
            sb.AppendLine("  —");

        sb.AppendLine();
        sb.AppendLine($"Оновлено: {DateTime.Now:dd.MM.yyyy HH:mm:ss}");

        // Файл перезаписується повністю
        File.WriteAllText(
            Path.Combine(_folder, $"passport_{patientId}.txt"),
            sb.ToString());
    }
}