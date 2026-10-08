using ClinicApp.Enums;
using ClinicApp.Utils;

namespace ClinicApp.Models;

public class Doctor
{
    private static int nextId = 1;

    private string firstName = "";
    private string lastName = "";
    private string licenseNumber = "";
    private string phone = "";

    public int Id { get; }

    public string FirstName
    {
        get => firstName;
        set
        {
            ClinicValidator.ValidateName(
                value,
                nameof(FirstName));

            firstName = value;
        }
    }

    public string LastName
    {
        get => lastName;
        set
        {
            ClinicValidator.ValidateName(
                value,
                nameof(LastName));

            lastName = value;
        }
    }

    public Speciality Speciality { get; set; }

    public string LicenseNumber
    {
        get => licenseNumber;
        set
        {
            if (string.IsNullOrWhiteSpace(value))
            {
                throw new ArgumentException(
                    "Некоректний номер ліцензії.",
                    nameof(LicenseNumber));
            }

            licenseNumber = value;
        }
    }

    public string Phone
    {
        get => phone;
        set
        {
            ClinicValidator.ValidatePhone(value);
            phone = value;
        }
    }

    public WorkSchedule Schedule { get; set; }

    public string FullName =>
        $"{FirstName} {LastName}";

    public int WorkingHoursPerDay =>
        Schedule.End - Schedule.Start;

    public string WorkSchedule =>
        Schedule.Display;

    public bool CanAcceptAt(int hour)
    {
        return Schedule.Contains(hour);
    }

    public bool IsAvailableNow =>
        Schedule.Contains(DateTime.Now.Hour);

    public Doctor(
        string firstName,
        string lastName,
        Speciality speciality,
        string licenseNumber,
        string phone,
        WorkSchedule schedule)
    {
        Id = nextId++;

        FirstName = firstName;
        LastName = lastName;
        Speciality = speciality;
        LicenseNumber = licenseNumber;
        Phone = phone;
        Schedule = schedule;
    }

    public Doctor(
        string firstName,
        string lastName,
        Speciality speciality)
        : this(
            firstName,
            lastName,
            speciality,
            "LIC-000",
            "0000000000",
            new WorkSchedule(8, 17))
    {
    }

    public Doctor()
        : this(
            "Невідомий",
            "Лікар",
            Speciality.General)
    {
    }

    public override string ToString()
    {
        return $"[{Id}] {FullName} | " +
               $"{ClinicFormatter.FormatSpeciality(Speciality)} | " +
               $"{LicenseNumber} | " +
               $"Тел: {ClinicFormatter.FormatPhone(Phone)} | " +
               $"{Schedule}";
    }
}