using ClinicApp.Managers;
using ClinicApp.Models;
using ClinicApp.Utils;

namespace ClinicApp;

public class Clinic
{
    public string Name { get; }

    public PatientManager Patients { get; }
    public DoctorManager Doctors { get; }
    public AppointmentManager Appointments { get; }
    public MedicalRecordManager MedicalRecords { get; }
    public BillingManager Billing { get; }
    public WaitingQueue<Patient> WaitingRoom { get; }
    public AnalyticsManager Analytics { get; }
    public TreatmentPlanManager TreatmentPlans { get; }
    public ClinicLogger Logger { get; }
    public ClinicExporter Exporter { get; }
    public SessionManager Session { get; }
    public PatientPassportWriter Passport { get; }
    public SessionEventTracker Tracker { get; }

    public Clinic(string name)
    {
        Name = name;
        Patients = new PatientManager();
        Doctors = new DoctorManager();
        Appointments = new AppointmentManager(Patients, Doctors);
        MedicalRecords = new MedicalRecordManager();
        Billing = new BillingManager(Appointments);
        WaitingRoom = new WaitingQueue<Patient>();
        Analytics = new AnalyticsManager(Appointments, Doctors, Patients);
        TreatmentPlans = new TreatmentPlanManager();
        Logger = new ClinicLogger();
        Exporter = new ClinicExporter(this);
        Session = new SessionManager();
        Passport = new PatientPassportWriter(this);
        Tracker = new SessionEventTracker(this);

        SubscribeEvents();
    }

    // Усі підписки зібрані в одному місці
    private void SubscribeEvents()
    {
        Patients.PatientAdded += Logger.OnPatientAdded;
        Patients.PatientRemoved += Logger.OnPatientRemoved;
        Appointments.AppointmentBooked += Logger.OnAppointmentBooked;
        Appointments.AppointmentCompleted += Logger.OnAppointmentCompleted;
        Appointments.AppointmentCancelled += Logger.OnAppointmentCancelled;
        Billing.PaymentProcessed += Logger.OnPaymentProcessed;
        TreatmentPlans.PlanCompleted += Logger.OnPlanCompleted;

        Patients.PatientAdded += Passport.OnPatientAdded;
        Appointments.AppointmentCompleted += Passport.OnAppointmentCompleted;
        TreatmentPlans.PlanCompleted += Passport.OnPlanCompleted;

        Patients.PatientAdded += Tracker.OnPatientAdded;
        Patients.PatientRemoved += Tracker.OnPatientRemoved;
        Appointments.AppointmentBooked += Tracker.OnAppointmentBooked;
        Appointments.AppointmentCompleted += Tracker.OnAppointmentCompleted;
        Appointments.AppointmentCancelled += Tracker.OnAppointmentCancelled;
        Billing.PaymentProcessed += Tracker.OnPaymentProcessed;
        TreatmentPlans.PlanCompleted += Tracker.OnPlanCompleted;
    }

    public void DisplaySchedule(DateTime date)
    {
        Console.WriteLine($"\n=== Розклад на {date:dd.MM.yyyy} ===");

        Appointment[] appointments = Appointments.GetByDate(date);
        Appointments.DisplayList(appointments);
    }

    public void GenerateReport()
    {
        Appointment[] upcoming = Appointments.GetUpcoming();
        Doctor[] doctors = Doctors.GetAll();

        Console.WriteLine("\n╔═════════════════════════════════════════╗");
        Console.WriteLine($"║ Звіт — {Name,-32} ║");
        Console.WriteLine("╠═════════════════════════════════════════╣");
        Console.WriteLine($"║ Пацієнтів:        {Patients.Count,-21} ║");
        Console.WriteLine($"║ Лікарів:          {Doctors.Count,-21} ║");
        Console.WriteLine($"║ Майбутніх записів: {upcoming.Length,-20} ║");
        Console.WriteLine("╠═════════════════════════════════════════╣");
        Console.WriteLine("║ Навантаження лікарів (майбутні записи): ║");

        foreach (Doctor doctor in doctors)
        {
            int count = 0;

            foreach (Appointment appointment in upcoming)
            {
                if (appointment.DoctorId == doctor.Id)
                    count++;
            }

            string information =
                $"  {doctor.FullName} ({doctor.Speciality}): {count} записів";

            Console.WriteLine($"║ {information,-39} ║");
        }

        Console.WriteLine("╚═════════════════════════════════════════╝");
    }
}