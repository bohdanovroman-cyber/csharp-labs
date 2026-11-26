namespace ClinicApp.Events;

public class PatientEventArgs : EventArgs
{
    public int PatientId { get; }
    public string PatientName { get; }

    public PatientEventArgs(int patientId, string patientName)
    {
        PatientId = patientId;
        PatientName = patientName;
    }
}