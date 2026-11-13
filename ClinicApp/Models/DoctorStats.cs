namespace ClinicApp.Models;

public class DoctorStats : IComparable<DoctorStats>
{
    public int DoctorId { get; }
    public string DoctorName { get; }
    public int AppointmentCount { get; }
    public decimal TotalRevenue { get; }
    public DateTime LastAppointment { get; }

    public DoctorStats(
        int doctorId,
        string doctorName,
        int appointmentCount,
        decimal totalRevenue,
        DateTime lastAppointment)
    {
        DoctorId = doctorId;
        DoctorName = doctorName;
        AppointmentCount = appointmentCount;
        TotalRevenue = totalRevenue;
        LastAppointment = lastAppointment;
    }

    // Природний порядок: більше прийомів, значить, вище
    public int CompareTo(DoctorStats? other)
    {
        if (other == null)
            return 1;

        return other.AppointmentCount.CompareTo(AppointmentCount);
    }

    public override string ToString()
    {
        string last = LastAppointment == DateTime.MinValue
            ? "—"
            : LastAppointment.ToString("dd.MM.yyyy HH:mm");

        return $"[{DoctorId}] {DoctorName} | прийомів: {AppointmentCount} | " +
               $"виручка: {TotalRevenue} грн | останній: {last}";
    }
}