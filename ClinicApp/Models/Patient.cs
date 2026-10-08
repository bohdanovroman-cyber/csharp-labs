using ClinicApp.Enums;
using ClinicApp.Utils;

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

    public DateTime DateOfBirth
    {
        get { return dateOfBirth; }
        set
        {
            ClinicValidator.ValidateDate(value, nameof(DateOfBirth));
            dateOfBirth = value;
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

    public string Email
    {
        get { return email; }
        set
        {
            ClinicValidator.ValidateEmail(value);
            email = value;
        }
    }

    public BloodType BloodType { get; set; }

    public Patient(
        string firstName,
        string lastName,
        DateTime dateOfBirth,
        BloodType bloodType,
        string phone,
        string email)
    {
        FirstName = firstName;
        LastName = lastName;
        DateOfBirth = dateOfBirth;
        BloodType = bloodType;
        Phone = phone;
        Email = email;
    }
}