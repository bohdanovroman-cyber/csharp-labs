using ClinicApp.Enums;

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
            if (value <= 0)
                throw new ArgumentOutOfRangeException(nameof(value), "Тривалість має бути більше 0.");

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