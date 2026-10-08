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
        set { firstName = value; }
    }

    public string LastName
    {
        get { return lastName; }
        set { lastName = value; }
    }

    public string LicenseNumber
    {
        get { return licenseNumber; }
        set { licenseNumber = value; }
    }

    public string Phone
    {
        get { return phone; }
        set { phone = value; }
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