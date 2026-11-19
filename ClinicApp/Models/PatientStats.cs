namespace ClinicApp.Models;

public class PatientStats : IComparable<PatientStats>
{
    public int PatientId { get; }
    public string PatientName { get; }
    public int VisitCount { get; }
    public decimal TotalSpent { get; }
    public DateTime LastVisit { get; }

    public PatientStats(
        int patientId,
        string patientName,
        int visitCount,
        decimal totalSpent,
        DateTime lastVisit)
    {
        PatientId = patientId;
        PatientName = patientName;
        VisitCount = visitCount;
        TotalSpent = totalSpent;
        LastVisit = lastVisit;
    }

    // Природний порядок: більше візитів, значить, вище
    public int CompareTo(PatientStats? other)
    {
        if (other == null)
            return 1;

        return other.VisitCount.CompareTo(VisitCount);
    }

    public override string ToString()
    {
        // 01.01.0001 означає «візитів не було», показуємо тире
        string last = LastVisit == DateTime.MinValue
            ? "—"
            : LastVisit.ToString("dd.MM.yyyy HH:mm");

        return $"[{PatientId}] {PatientName} | візитів: {VisitCount} | " +
               $"витрачено: {TotalSpent} грн | останній: {last}";
    }
}