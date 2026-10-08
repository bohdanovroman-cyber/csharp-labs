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
        set { firstName = value; }
    }

    public string LastName
    {
        get { return lastName; }
        set { lastName = value; }
    }

    public DateTime DateOfBirth
    {
        get { return dateOfBirth; }
        set { dateOfBirth = value; }
    }

    public string Phone
    {
        get { return phone; }
        set { phone = value; }
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