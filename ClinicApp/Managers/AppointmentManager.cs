using ClinicApp.Events;
using ClinicApp.Interfaces;
using ClinicApp.Models;

namespace ClinicApp.Managers;

public class AppointmentManager
{
    private const int MaxAppointments = 500;

    private Appointment[] _appointments;

    private int _count;

    private PatientManager _patients;
    private DoctorManager _doctors;

    public int Count => _count;

    public event EventHandler<AppointmentEventArgs>? AppointmentBooked;
    public event EventHandler<AppointmentEventArgs>? AppointmentCompleted;
    public event EventHandler<AppointmentEventArgs>? AppointmentCancelled;

    // ?. робить подію безпечною, коли підписників немає
    private void OnAppointmentBooked(Appointment appointment)
    {
        AppointmentBooked?.Invoke(this, new AppointmentEventArgs(appointment));
    }

    public AppointmentManager(
        PatientManager patients,
        DoctorManager doctors)
    {
        _patients = patients;
        _doctors = doctors;

        _appointments = new Appointment[MaxAppointments];

        _count = 0;
    }

    private Appointment? FindById(int id)
    {
        for (int i = 0; i < _count; i++)
        {
            if (_appointments[i].Id == id)
            {
                return _appointments[i];
            }
        }

        return null;
    }

    // Спільна перевірка для всіх видів запису
    private bool TryResolve(
        int patientId,
        int doctorId,
        out Patient? patient,
        out Doctor? doctor)
    {
        patient = null;
        doctor = null;

        if (_count >= MaxAppointments)
        {
            Console.WriteLine(
                "Помилка: досягнуто ліміт записів.");

            return false;
        }

        patient = _patients.FindById(patientId);

        if (patient == null)
        {
            Console.WriteLine(
                $"Помилка: пацієнта з ID {patientId} не знайдено.");

            return false;
        }

        doctor = _doctors.FindById(doctorId);

        if (doctor == null)
        {
            Console.WriteLine(
                $"Помилка: лікаря з ID {doctorId} не знайдено.");

            return false;
        }

        return true;
    }

    // Додає запис, і лише після успішного додавання піднімає подію
    private void Register(
        Appointment appointment,
        Patient patient,
        Doctor doctor)
    {
        _appointments[_count] = appointment;

        _count++;

        Console.WriteLine(
            $"Запис [{appointment.Id}] створено: " +
            $"{patient.FullName} -> " +
            $"{doctor.FullName} о " +
            $"{appointment.ScheduledAt:dd.MM.yyyy HH:mm}");

        OnAppointmentBooked(appointment);
    }

    public bool Book(
        int patientId,
        int doctorId,
        DateTime scheduledAt,
        int durationMinutes = 30)
    {
        if (!TryResolve(patientId, doctorId, out Patient? patient, out Doctor? doctor))
            return false;

        Register(
            new Appointment(patientId, doctorId, scheduledAt, durationMinutes),
            patient!,
            doctor!);

        return true;
    }

    public bool BookUrgent(
        int patientId,
        int doctorId,
        DateTime scheduledAt,
        string reason,
        int durationMinutes = 30)
    {
        if (!TryResolve(patientId, doctorId, out Patient? patient, out Doctor? doctor))
            return false;

        Register(
            new UrgentAppointment(patientId, doctorId, scheduledAt, reason, durationMinutes),
            patient!,
            doctor!);

        return true;
    }

    public bool BookSpecialist(
        int patientId,
        int doctorId,
        DateTime scheduledAt,
        string reason,
        int durationMinutes = 30)
    {
        if (!TryResolve(patientId, doctorId, out Patient? patient, out Doctor? doctor))
            return false;

        Register(
            new SpecialistAppointment(patientId, doctorId, scheduledAt, reason, durationMinutes),
            patient!,
            doctor!);

        return true;
    }

    public bool Cancel(
        int id,
        string reason = "")
    {
        Appointment? app = FindById(id);

        if (app == null)
        {
            Console.WriteLine(
                $"Запис з ID {id} не знайдено.");

            return false;
        }

        bool success = app.Cancel(reason);

        if (success)
        {
            Console.WriteLine(
                $"[Успіх] Запис [{id}] успішно скасовано.");

            AppointmentCancelled?.Invoke(this, new AppointmentEventArgs(app));
        }
        else
        {
            Console.WriteLine(
                $"[Помилка] Запис [{id}] неможливо скасувати " +
                $"(поточний статус: {app.Status}).");
        }

        return success;
    }

    public static int CancelAll(
        ICancellable[] items,
        string reason = "")
    {
        if (items == null)
            return 0;

        int cancelled = 0;

        for (int i = 0; i < items.Length; i++)
        {
            if (!items[i].IsCancelled &&
                items[i].Cancel(reason))
            {
                cancelled++;
            }
        }

        return cancelled;
    }

    public bool Complete(int id)
    {
        Appointment? app = FindById(id);

        if (app == null)
        {
            Console.WriteLine(
                $"Запис з ID {id} не знайдено.");

            return false;
        }

        bool success = app.Complete();

        if (success)
        {
            Console.WriteLine(
                $"Запис [{id}] завершено.");

            AppointmentCompleted?.Invoke(this, new AppointmentEventArgs(app));
        }
        else
        {
            Console.WriteLine(
                $"[Помилка] Запис [{id}] неможливо завершити " +
                $"(поточний статус: {app.Status}).");
        }

        return success;
    }

    // Застосування VIP-знижки 20%
    public bool ApplyVip(int id)
    {
        Appointment? app = FindById(id);

        if (app == null)
        {
            Console.WriteLine(
                $"Запис з ID {id} не знайдено.");

            return false;
        }

        app.DiscountMultiplier = 0.8m;

        Console.WriteLine(
            $"VIP-знижку 20% застосовано до запису [{id}].");

        return true;
    }

    public Appointment[] GetAll()
    {
        Appointment[] copy = new Appointment[_count];

        for (int i = 0; i < _count; i++)
        {
            copy[i] = _appointments[i];
        }

        return copy;
    }

    public Appointment[] GetByPatient(int patientId)
    {
        int count = 0;

        for (int i = 0; i < _count; i++)
        {
            if (_appointments[i].PatientId == patientId)
            {
                count++;
            }
        }

        Appointment[] result = new Appointment[count];

        int index = 0;

        for (int i = 0; i < _count; i++)
        {
            if (_appointments[i].PatientId == patientId)
            {
                result[index++] = _appointments[i];
            }
        }

        return result;
    }

    public Appointment[] GetByDoctor(int doctorId)
    {
        int count = 0;

        for (int i = 0; i < _count; i++)
        {
            if (_appointments[i].DoctorId == doctorId)
            {
                count++;
            }
        }

        Appointment[] result = new Appointment[count];

        int index = 0;

        for (int i = 0; i < _count; i++)
        {
            if (_appointments[i].DoctorId == doctorId)
            {
                result[index++] = _appointments[i];
            }
        }

        return result;
    }

    public Appointment[] GetByDate(DateTime date)
    {
        int count = 0;

        for (int i = 0; i < _count; i++)
        {
            if (_appointments[i].ScheduledAt.Date == date.Date)
            {
                count++;
            }
        }

        Appointment[] result = new Appointment[count];

        int index = 0;

        for (int i = 0; i < _count; i++)
        {
            if (_appointments[i].ScheduledAt.Date == date.Date)
            {
                result[index++] = _appointments[i];
            }
        }

        return result;
    }

    public Appointment[] GetByDate(
        int year,
        int month,
        int day)
    {
        return GetByDate(
            new DateTime(year, month, day));
    }

    public Appointment[] GetUpcoming()
    {
        int count = 0;

        for (int i = 0; i < _count; i++)
        {
            if (_appointments[i].IsUpcoming)
            {
                count++;
            }
        }

        Appointment[] result = new Appointment[count];

        int index = 0;

        for (int i = 0; i < _count; i++)
        {
            if (_appointments[i].IsUpcoming)
            {
                result[index++] = _appointments[i];
            }
        }

        return result;
    }

    public UrgentAppointment[] GetUrgent()
    {
        int count = 0;

        for (int i = 0; i < _count; i++)
        {
            if (_appointments[i] is UrgentAppointment)
                count++;
        }

        UrgentAppointment[] result =
            new UrgentAppointment[count];

        int index = 0;

        for (int i = 0; i < _count; i++)
        {
            if (_appointments[i] is UrgentAppointment urgent)
                result[index++] = urgent;
        }

        return result;
    }

    public SpecialistAppointment[] GetSpecialist()
    {
        int count = 0;

        for (int i = 0; i < _count; i++)
        {
            if (_appointments[i] is SpecialistAppointment)
                count++;
        }

        SpecialistAppointment[] result =
            new SpecialistAppointment[count];

        int index = 0;

        for (int i = 0; i < _count; i++)
        {
            if (_appointments[i] is SpecialistAppointment specialist)
                result[index++] = specialist;
        }

        return result;
    }

    public RegularAppointment[] GetRegular()
    {
        int count = 0;

        for (int i = 0; i < _count; i++)
        {
            if (_appointments[i] is RegularAppointment)
                count++;
        }

        RegularAppointment[] result =
            new RegularAppointment[count];

        int index = 0;

        for (int i = 0; i < _count; i++)
        {
            if (_appointments[i] is RegularAppointment regular)
                result[index++] = regular;
        }

        return result;
    }

    public void DisplayAppointment(Appointment app)
    {
        if (app == null)
            return;

        Patient? patient = _patients.FindById(app.PatientId);
        Doctor? doctor = _doctors.FindById(app.DoctorId);

        string patientName =
            patient != null
                ? patient.FullName
                : $"Пацієнт #{app.PatientId}";

        string doctorName =
            doctor != null
                ? doctor.FullName
                : $"Лікар #{app.DoctorId}";

        // Один код для всіх видів: поліморфізм сам підставляє потрібний опис і ціну
        string line =
            $"{app.GetDescription()} | [{app.Id}] {patientName} -> " +
            $"{doctorName} | " +
            $"{app.ScheduledAt:dd.MM.yyyy HH:mm}-" +
            $"{app.EndsAt:HH:mm} | " +
            $"{app.Status} | " +
            $"{app.GetCost():F2} грн";

        if (app.Notes != null && app.Notes.Length > 0)
        {
            line += $" | {app.Notes}";
        }

        Console.WriteLine(line);
    }

    public void DisplayList(Appointment[] list)
    {
        if (list == null || list.Length == 0)
        {
            Console.WriteLine(
                "Записів не знайдено.");

            return;
        }

        for (int i = 0; i < list.Length; i++)
        {
            DisplayAppointment(list[i]);
        }
    }

    public Appointment? this[int index]
    {
        get
        {
            if (index < 0 || index >= _count)
            {
                return null;
            }

            return _appointments[index];
        }
    }
}