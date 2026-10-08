using ClinicApp.Enums;

namespace ClinicApp.Models;

public class Patient
{
    private string firstName;
    private string lastName;
    private DateTime dateOfBirth;
    private string phone;
    private string email;

    public string FirstName
    {
        get { return firstName; }
        set
        {
            if (string.IsNullOrWhiteSpace(value) || value.Length > 50)
                throw new ArgumentException("Некоректне ім'я.");

            firstName = value;
        }
    }

    public string LastName
    {
        get { return lastName; }
        set
        {
            if (string.IsNullOrWhiteSpace(value) || value.Length > 50)
                throw new ArgumentException("Некоректне прізвище.");

            lastName = value;
        }
    }

    public DateTime DateOfBirth
    {
        get { return dateOfBirth; }
        set
        {
            if (value > DateTime.Today)
                throw new ArgumentOutOfRangeException(nameof(value), "Дата не може бути в майбутньому.");

            if (value.Year < 1900)
                throw new ArgumentOutOfRangeException(nameof(value), "Дата не може бути раніше 1900 року.");

            dateOfBirth = value;
        }
    }

    public string Phone
    {
        get { return phone; }
        set
        {
            if (string.IsNullOrWhiteSpace(value) || value.Length != 10)
                throw new ArgumentException("Номер телефону має містити 10 цифр.");

            foreach (char c in value)
            {
                if (!char.IsDigit(c))
                    throw new ArgumentException("Номер телефону має містити тільки цифри.");
            }

            phone = value;
        }
    }

    public string Email
    {
        get { return email; }
        set { email = value; }
    }

    public BloodType BloodType { get; set; }

    public Patient(
        string firstName,
        string lastName,
        DateTime dateOfBirth,
        string phone,
        string email,
        BloodType bloodType)
    {
        FirstName = firstName;
        LastName = lastName;
        DateOfBirth = dateOfBirth;
        Phone = phone;
        Email = email;
        BloodType = bloodType;
    }
}