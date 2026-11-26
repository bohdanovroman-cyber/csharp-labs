namespace ClinicApp.Events;

public class PaymentEventArgs : EventArgs
{
    public int AppointmentId { get; }
    public int PatientId { get; }
    public decimal Amount { get; }
    public DateTime PaidAt { get; }

    public PaymentEventArgs(int appointmentId, int patientId, decimal amount)
    {
        AppointmentId = appointmentId;
        PatientId = patientId;
        Amount = amount;
        PaidAt = DateTime.Now;
    }
}