using ClinicApp.Enums;
using ClinicApp.Utils;

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
        FirstName = firstName;
        LastName = lastName;
        Speciality = speciality;
        LicenseNumber = licenseNumber;
        Phone = phone;
        WorkSchedule = workSchedule;
    }
}