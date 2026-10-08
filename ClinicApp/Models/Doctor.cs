using ClinicApp.Enums;

namespace ClinicApp.Models;

public class Doctor
{
    private string firstName;
    private string lastName;
    private string licenseNumber;
    private string phone;

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

    public string LicenseNumber
    {
        get { return licenseNumber; }
        set
        {
            if (string.IsNullOrWhiteSpace(value))
                throw new ArgumentException("Номер ліцензії не може бути порожнім.");

            licenseNumber = value;
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

    public Speciality Speciality { get; set; }
    public WorkSchedule WorkSchedule { get; set; }

    public Doctor(
        string firstName,
        string lastName,
        string licenseNumber,
        string phone,
        Speciality speciality,
        WorkSchedule workSchedule)
    {
        FirstName = firstName;
        LastName = lastName;
        LicenseNumber = licenseNumber;
        Phone = phone;
        Speciality = speciality;
        WorkSchedule = workSchedule;
    }
}