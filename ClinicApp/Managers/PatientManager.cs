using ClinicApp.Enums;
using ClinicApp.Models;

namespace ClinicApp.Managers;

public class PatientManager
{
    private List<Patient> _patients = new();

    public int Count => _patients.Count;

    public void Add(Patient patient)
    {
        _patients.Add(patient);

        Console.WriteLine(
            $"Пацієнта [{patient.Id}] {patient.FullName} додано.");
    }

    public Patient? FindById(int id)
    {
        foreach (Patient patient in _patients)
        {
            if (patient.Id == id)
            {
                return patient;
            }
        }

        return null;
    }

    public bool TryFindById(
        int id,
        out Patient? patient)
    {
        patient = FindById(id);

        return patient != null;
    }

    public Patient[] FindByName(string name)
    {
        List<Patient> result = new();

        foreach (Patient patient in _patients)
        {
            if (patient.FirstName.Contains(
                    name,
                    StringComparison.OrdinalIgnoreCase) ||
                patient.LastName.Contains(
                    name,
                    StringComparison.OrdinalIgnoreCase))
            {
                result.Add(patient);
            }
        }

        return result.ToArray();
    }

    public bool RemoveById(int id)
    {
        int index = _patients.FindIndex(p => p.Id == id);

        if (index == -1)
        {
            Console.WriteLine(
                $"Пацієнта з ID {id} не знайдено.");

            return false;
        }

        string removedName = _patients[index].FullName;

        _patients.RemoveAt(index);

        Console.WriteLine(
            $"Пацієнта [{id}] {removedName} успішно видалено.");

        return true;
    }

    public Patient[] FindByBloodType(
        BloodType bloodType)
    {
        List<Patient> result = new();

        foreach (Patient patient in _patients)
        {
            if (patient.BloodType == bloodType)
            {
                result.Add(patient);
            }
        }

        return result.ToArray();
    }

    // Повертає масив однією операцією
    public Patient[] GetAll()
    {
        return _patients.ToArray();
    }

    public void DisplayAll()
    {
        if (_patients.Count == 0)
        {
            Console.WriteLine(
                "Список пацієнтів порожній.");
        }
        else
        {
            Console.WriteLine(
                $"=== Пацієнти ({_patients.Count}) ===");

            foreach (Patient patient in _patients)
            {
                Console.WriteLine(patient);
            }
        }
    }

    public void DisplayStats()
    {
        if (_patients.Count == 0)
        {
            Console.WriteLine(
                "Немає даних для статистики.");

            return;
        }

        double sumAge = 0;

        Patient youngest = _patients[0];
        Patient oldest = _patients[0];
        int howManyAdults = 0;

        foreach (Patient patient in _patients)
        {
            sumAge += patient.Age;

            if (patient.Age < youngest.Age)
            {
                youngest = patient;
            }

            if (patient.Age > oldest.Age)
            {
                oldest = patient;
            }

            if (patient.IsAdult)
            {
                howManyAdults++;
            }
        }

        double averageAge = sumAge / _patients.Count;

        Console.WriteLine(
            "\n=== Статистика пацієнтів ===");

        Console.WriteLine(
            $"Всього: {_patients.Count}");

        Console.WriteLine(
            $"Середній вік: {averageAge:F1} р.");

        Console.WriteLine(
            $"Наймолодший: {youngest.FullName} ({youngest.Age} р.)");

        Console.WriteLine(
            $"Найстарший: {oldest.FullName} ({oldest.Age} р.)");

        Console.WriteLine(
            $"Дорослих: {howManyAdults} з {_patients.Count}");
    }

    public Patient? this[int index]
    {
        get
        {
            if (index < 0 ||
                index >= _patients.Count)
            {
                return null;
            }

            return _patients[index];
        }
    }
}