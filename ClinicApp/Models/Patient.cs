using ClinicApp.Enums;
using ClinicApp.Utils;

namespace ClinicApp.Models;

public class Patient
{
    private static int nextId = 1;

    private string firstName = "";
    private string lastName = "";
    private DateTime dateOfBirth;
    private string phone = "";
    private string email = "";

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

    public DateTime DateOfBirth
    {
        get => dateOfBirth;
        set
        {
            ClinicValidator.ValidateDate(
                value,
                nameof(DateOfBirth));

            dateOfBirth = value;
        }
    }

    public BloodType BloodType { get; set; }

    public string Phone
    {
        get => phone;
        set
        {
            ClinicValidator.ValidatePhone(value);
            phone = value;
        }
    }

    public string Email
    {
        get => email;
        set
        {
            ClinicValidator.ValidateEmail(value);
            email = value;
        }
    }

    public int Age
    {
        get
        {
            DateTime today = DateTime.Today;

            int age =
                today.Year - DateOfBirth.Year;

            if (DateOfBirth.Date >
                today.AddYears(-age))
            {
                age--;
            }

            return age;
        }
    }

    public string FullName =>
        $"{FirstName} {LastName}";

    public bool IsAdult =>
        Age >= 18;

    public Patient(
        string firstName,
        string lastName,
        DateTime dateOfBirth,
        BloodType bloodType,
        string phone,
        string email)
    {
        Id = nextId++;

        FirstName = firstName;
        LastName = lastName;
        DateOfBirth = dateOfBirth;
        BloodType = bloodType;
        Phone = phone;
        Email = email;
    }

    public Patient(
        string firstName,
        string lastName)
        : this(
            firstName,
            lastName,
            new DateTime(2000, 3, 15),
            BloodType.Unknown,
            "0000000000",
            "")
    {
    }

    public Patient()
        : this(
            "Невідомий",
            "Пацієнт")
    {
    }

    public string GetAgeCategory()
    {
        if (Age < 18)
            return "дитина";

        if (Age < 60)
            return "дорослий";

        return "літній";
    }

    public override string ToString()
    {
        return $"[{Id}] {FullName} | " +
               $"Вік: {ClinicFormatter.FormatAge(Age)} | " +
               $"Гр. крові: {ClinicFormatter.FormatBloodType(BloodType)} | " +
               $"Тел: {ClinicFormatter.FormatPhone(Phone)}";
    }
}