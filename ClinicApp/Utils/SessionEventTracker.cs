using System.Text;
using ClinicApp.Events;

namespace ClinicApp.Utils;

public class SessionEventTracker
{
    private readonly Clinic _clinic;

    // Ззовні можна лише читати
    public int PatientsAdded { get; private set; }
    public int PatientsRemoved { get; private set; }
    public int AppointmentsBooked { get; private set; }
    public int AppointmentsCompleted { get; private set; }
    public int AppointmentsCancelled { get; private set; }
    public int PaymentsProcessed { get; private set; }
    public int PlansCompleted { get; private set; }

    public SessionEventTracker(Clinic clinic)
    {
        _clinic = clinic;
    }

    public void OnPatientAdded(object? sender, PatientEventArgs e)
    {
        PatientsAdded++;
    }

    public void OnPatientRemoved(object? sender, PatientEventArgs e)
    {
        PatientsRemoved++;
    }

    public void OnAppointmentBooked(object? sender, AppointmentEventArgs e)
    {
        AppointmentsBooked++;
    }

    public void OnAppointmentCompleted(object? sender, AppointmentEventArgs e)
    {
        AppointmentsCompleted++;
    }

    // Скасування прийому -> перевірка черги очікування
    public void OnAppointmentCancelled(object? sender, AppointmentEventArgs e)
    {
        AppointmentsCancelled++;

        if (_clinic.WaitingRoom.Count > 0)
        {
            // Peek: пацієнта з черги не видаляємо
            Console.WriteLine(
                $"[ЧЕРГА] Слот звільнився. Наступний: {_clinic.WaitingRoom.Peek().FullName}");
        }
    }

    public void OnPaymentProcessed(object? sender, PaymentEventArgs e)
    {
        PaymentsProcessed++;
    }

    public void OnPlanCompleted(object? sender, TreatmentPlanEventArgs e)
    {
        PlansCompleted++;
    }

    private string BuildSummary()
    {
        StringBuilder sb = new();

        sb.AppendLine("=== Підсумок сесії ===");
        sb.AppendLine($"Пацієнтів додано: {PatientsAdded}");
        sb.AppendLine($"Пацієнтів видалено: {PatientsRemoved}");
        sb.AppendLine($"Записів створено: {AppointmentsBooked}");
        sb.AppendLine($"Прийомів завершено: {AppointmentsCompleted}");
        sb.AppendLine($"Записів скасовано: {AppointmentsCancelled}");
        sb.AppendLine($"Платежів проведено: {PaymentsProcessed}");
        sb.AppendLine($"Планів лікування завершено: {PlansCompleted}");

        return sb.ToString();
    }

    public void PrintSummary()
    {
        Console.WriteLine();
        Console.Write(BuildSummary());
    }

    // Той самий підсумок у файл, з датою й часом формування
    public void SaveSummary(string path = "session_summary.txt")
    {
        string content =
            $"Сформовано: {DateTime.Now:dd.MM.yyyy HH:mm:ss}{Environment.NewLine}" +
            BuildSummary();

        File.WriteAllText(path, content);
    }
}