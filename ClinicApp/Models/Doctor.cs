using ClinicApp.Enums;
using ClinicApp.Interfaces;
using ClinicApp.Utils;

namespace ClinicApp.Models;

public class Doctor : ISchedulable
{
    private static int _nextId = 1;

    private string firstName;
    private string lastName;
    private string licenseNumber;
    private string phone;

    public int Id { get; }

    public string FirstName
    {
        get { return firstName; }
        set
        {
            ClinicValidator.ValidateName(value, nameof(FirstName));
            firstName = value;
        }
    }

    public string LastName
    {
        get { return lastName; }
        set
        {
            ClinicValidator.ValidateName(value, nameof(LastName));
            lastName = value;
        }
    }

    public string FullName => $"{FirstName} {LastName}";

    public string LicenseNumber
    {
        get { return licenseNumber; }
        set
        {
            if (string.IsNullOrWhiteSpace(value))
                throw new ArgumentException(
                    "Номер ліцензії не може бути порожнім.",
                    nameof(LicenseNumber));

            licenseNumber = value;
        }
    }

    public string Phone
    {
        get { return phone; }
        set
        {
            ClinicValidator.ValidatePhone(value);
            phone = value;
        }
    }

    public Speciality Speciality { get; set; }

    public WorkSchedule WorkSchedule { get; set; }

    public Doctor(
        string firstName,
        string lastName,
        Speciality speciality,
        string licenseNumber,
        string phone,
        WorkSchedule workSchedule)
    {
        Id = _nextId++;
        FirstName = firstName;
        LastName = lastName;
        Speciality = speciality;
        LicenseNumber = licenseNumber;
        Phone = phone;
        WorkSchedule = workSchedule;
    }

    public bool CanSchedule(DateTime at)
    {
        return WorkSchedule.Contains(at.Hour);
    }

    public DateTime[] GetAvailableSlots(DateTime date, int slotCount)
    {
        ClinicValidator.ValidatePositive(slotCount, nameof(slotCount));

        int total = Math.Min(slotCount, WorkSchedule.HoursPerDay);

        DateTime[] slots = new DateTime[total];

        for (int i = 0; i < total; i++)
        {
            slots[i] = date.Date.AddHours(WorkSchedule.Start + i);
        }

        return slots;
    }

    public override string ToString()
    {
        return $"[{Id}] {FullName} ({Speciality}) | {WorkSchedule}";
    }
}