using ClinicApp.Models;
 
namespace ClinicApp.Managers;
 
public class MedicalRecordManager
{
    private const int MaxRecords = 1000;
 
    // Поліморфний масив: тут разом живуть
    // Diagnosis, LabResult і Prescription.
    private MedicalRecord[] _records =
        new MedicalRecord[MaxRecords];
 
    private int _count = 0;
 
    public int Count => _count;
 
    public void Add(MedicalRecord record)
    {
        if (record == null)
        {
            return;
        }
 
        if (_count < MaxRecords)
        {
            _records[_count++] = record;
 
            Console.WriteLine(
                $"Запис [{record.Id}] ({record.GetRecordType()}) додано.");
        }
        else
        {
            Console.WriteLine(
                $"Не вдалося додати запис, " +
                $"перевищено ліміт ({MaxRecords}).");
        }
    }
 
    public MedicalRecord? FindById(int id)
    {
        for (int i = 0; i < _count; i++)
        {
            if (_records[i].Id == id)
            {
                return _records[i];
            }
        }
 
        return null;
    }
 
    public MedicalRecord[] GetByPatient(int patientId)
    {
        int matchCount = 0;
 
        for (int i = 0; i < _count; i++)
        {
            if (_records[i].PatientId == patientId)
            {
                matchCount++;
            }
        }
 
        MedicalRecord[] result =
            new MedicalRecord[matchCount];
 
        int index = 0;
 
        for (int i = 0; i < _count; i++)
        {
            if (_records[i].PatientId == patientId)
            {
                result[index++] = _records[i];
            }
        }
 
        return result;
    }
 
    public MedicalRecord[] GetByDoctor(int doctorId)
    {
        int matchCount = 0;
 
        for (int i = 0; i < _count; i++)
        {
            if (_records[i].DoctorId == doctorId)
            {
                matchCount++;
            }
        }
 
        MedicalRecord[] result =
            new MedicalRecord[matchCount];
 
        int index = 0;
 
        for (int i = 0; i < _count; i++)
        {
            if (_records[i].DoctorId == doctorId)
            {
                result[index++] = _records[i];
            }
        }
 
        return result;
    }
 
    // ===== Виведення =====
 
    public void DisplayAll()
    {
        if (_count == 0)
        {
            Console.WriteLine(
                "Список медичних записів порожній.");
 
            return;
        }
 
        Console.WriteLine(
            $"=== Медичні записи ({_count} / {MaxRecords}) ===");
 
        for (int i = 0; i < _count; i++)
        {
            Console.WriteLine(_records[i]);
        }
    }
 
    public void DisplayList(MedicalRecord[] list)
    {
        if (list == null || list.Length == 0)
        {
            Console.WriteLine("Записів не знайдено.");
 
            return;
        }
 
        for (int i = 0; i < list.Length; i++)
        {
            Console.WriteLine(list[i]);
        }
    }
 
    public MedicalRecord? this[int index]
    {
        get
        {
            if (index < 0 || index >= _count)
            {
                return null;
            }
 
            return _records[index];
        }
    }
}