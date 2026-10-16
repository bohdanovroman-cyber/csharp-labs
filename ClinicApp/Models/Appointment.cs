using ClinicApp.Enums;
using ClinicApp.Utils;

namespace ClinicApp.Models;

public class Appointment
{
    private int durationMinutes;

    public Patient Patient { get; set; }

    public Doctor Doctor { get; set; }

    public DateTime Date { get; set; }

    public int DurationMinutes
    {
        get { return durationMinutes; }
        set
        {
            ClinicValidator.ValidatePositive(
                value,
                nameof(DurationMinutes));

            durationMinutes = value;
        }
    }

    public AppointmentStatus Status { get; set; }

    public Appointment(
        Patient patient,
        Doctor doctor,
        DateTime date,
        int durationMinutes,
        AppointmentStatus status)
    {
        Patient = patient;
        Doctor = doctor;
        Date = date;
        DurationMinutes = durationMinutes;
        Status = status;
    }
}