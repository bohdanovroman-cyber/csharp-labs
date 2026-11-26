using ClinicApp.Events;

namespace ClinicApp.Utils
{
    public class ClinicLogger
    {
        private readonly string _filePath;

        public ClinicLogger(string filePath = "clinic.log")
        {
            _filePath = filePath;
        }

        public void LogInfo(string message)
        {
            WriteLog("INFO", message);
        }

        public void LogWarning(string message)
        {
            WriteLog("WARNING", message);
        }

        public void LogError(string message)
        {
            WriteLog("ERROR", message);
        }

        private void WriteLog(string level, string message)
        {
            string entry =
                $"[{DateTime.Now:yyyy-MM-dd HH:mm:ss}] [{level}] {message}";

            File.AppendAllText(_filePath, entry + Environment.NewLine);
        }

        public string[] GetLastLines(int count)
        {
            if (count <= 0)
                return Array.Empty<string>();

            if (!File.Exists(_filePath))
                return Array.Empty<string>();

            return File.ReadAllLines(_filePath)
                .TakeLast(count)
                .ToArray();
        }

        public void Clear()
        {
            File.WriteAllText(_filePath, string.Empty);
        }

        // ===== Обробники подій =====

        public void OnPatientAdded(object? sender, PatientEventArgs e)
        {
            LogInfo($"Додано пацієнта #{e.PatientId} {e.PatientName}");
        }

        public void OnPatientRemoved(object? sender, PatientEventArgs e)
        {
            LogInfo($"Видалено пацієнта #{e.PatientId} {e.PatientName}");
        }

        public void OnAppointmentBooked(object? sender, AppointmentEventArgs e)
        {
            string message =
                $"Запис #{e.AppointmentId} створено: пацієнт #{e.PatientId}, " +
                $"лікар #{e.DoctorId}, {e.ScheduledAt:dd.MM.yyyy HH:mm}";

            if (e.IsUrgent)
            {
                // Терміновий запис: попередження і додатковий рядок у файлі alerts
                LogWarning("ТЕРМІНОВО. " + message);
                WriteUrgentAlert(message);
            }
            else
            {
                LogInfo(message);
            }
        }

        public void OnAppointmentCompleted(object? sender, AppointmentEventArgs e)
        {
            LogInfo($"Прийом #{e.AppointmentId} завершено (пацієнт #{e.PatientId})");
        }

        public void OnAppointmentCancelled(object? sender, AppointmentEventArgs e)
        {
            LogInfo($"Запис #{e.AppointmentId} скасовано. Причина: {e.Reason}");
        }

        public void OnPaymentProcessed(object? sender, PaymentEventArgs e)
        {
            LogInfo($"Оплачено запис #{e.AppointmentId}: {e.Amount:F2} грн");
        }

        public void OnPlanCompleted(object? sender, TreatmentPlanEventArgs e)
        {
            LogInfo($"План лікування #{e.PlanId} ({e.Diagnosis}) завершено");
        }

        // alerts/urgent_{yyyy-MM-dd}.txt
        private void WriteUrgentAlert(string message)
        {
            Directory.CreateDirectory("alerts");

            string path = Path.Combine(
                "alerts",
                $"urgent_{DateTime.Now:yyyy-MM-dd}.txt");

            File.AppendAllText(
                path,
                $"[{DateTime.Now:HH:mm:ss}] {message}{Environment.NewLine}");
        }
    }
}