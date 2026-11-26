using ClinicApp.Utils;
 
namespace ClinicApp.Models;
 
public abstract class MedicalRecord
{
    private static int _nextId = 1;
 
    public int Id { get; }
 
    public int PatientId { get; }
 
    public int DoctorId { get; }
 
    public DateTime Date { get; }
 
    public string Notes { get; set; }
 
    protected MedicalRecord(
        int patientId,
        int doctorId,
        DateTime date,
        string notes = "")
    {
        ClinicValidator.ValidatePositive(
            patientId,
            nameof(PatientId));
 
        ClinicValidator.ValidatePositive(
            doctorId,
            nameof(DoctorId));
 
        PatientId = patientId;
        DoctorId = doctorId;
        Date = date;
        Notes = notes;
 
        // Id присвоюється останнім: якщо валідація вище
        // кине виняток, лічильник не "згорить" даремно.
        Id = _nextId++;
    }
 
    public abstract string GetSummary();
 
    public virtual string GetRecordType()
    {
        return "Медичний запис";
    }
 
    public virtual bool IsActive()
    {
        return Date >= DateTime.Today.AddMonths(-6);
    }
 
    public override string ToString()
    {
        return $"[{Id}] {GetRecordType()} | " +
               $"{Date:dd.MM.yyyy} | " +
               $"{GetSummary()}";
    }
}