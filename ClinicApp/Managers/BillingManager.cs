using ClinicApp.Interfaces;
using ClinicApp.Models;

namespace ClinicApp.Managers;

public class BillingManager
{
    private readonly AppointmentManager _appointments;

    public BillingManager(AppointmentManager appointments)
    {
        _appointments = appointments;
    }

    private IPayable[] FilterUnpaid(IPayable[] source, ICancellable[] cancellable)
    {
        int count = 0;

        for (int i = 0; i < source.Length; i++)
        {
            if (!source[i].IsPaid && !cancellable[i].IsCancelled)
                count++;
        }

        IPayable[] result = new IPayable[count];
        int index = 0;

        for (int i = 0; i < source.Length; i++)
        {
            if (!source[i].IsPaid && !cancellable[i].IsCancelled)
            {
                result[index++] = source[i];
            }
        }

        return result;
    }

    private IPayable[] CollectUnpaid(Appointment[] list)
    {
        IPayable[] payables = new IPayable[list.Length];
        ICancellable[] cancellable = new ICancellable[list.Length];

        for (int i = 0; i < list.Length; i++)
        {
            payables[i] = list[i];
            cancellable[i] = list[i];
        }

        return FilterUnpaid(payables, cancellable);
    }

    public IPayable[] GetAllUnpaid()
    {
        return CollectUnpaid(_appointments.GetAll());
    }

    public IPayable[] GetUnpaidByPatient(int patientId)
    {
        return CollectUnpaid(_appointments.GetByPatient(patientId));
    }

    public decimal GetTotalDebt()
    {
        return SumCost(GetAllUnpaid());
    }

    public decimal GetPatientDebt(int patientId)
    {
        return SumCost(GetUnpaidByPatient(patientId));
    }

    private decimal SumCost(IPayable[] items)
    {
        decimal sum = 0m;

        for (int i = 0; i < items.Length; i++)
        {
            sum += items[i].GetCost();
        }

        return sum;
    }

    public bool PayAppointment(int appointmentId)
    {
        Appointment[] all = _appointments.GetAll();

        for (int i = 0; i < all.Length; i++)
        {
            if (all[i].Id == appointmentId)
            {
                if (all[i].IsCancelled || all[i].IsPaid)
                    return false;

                all[i].MarkPaid();
                return true;
            }
        }

        return false;
    }

    public void DisplayUnpaid(IPayable[] items)
    {
        if (items.Length == 0)
        {
            Console.WriteLine("Немає неоплачених записів.");
            return;
        }

        Console.WriteLine($"=== Неоплачені записи ({items.Length}) ===");

        for (int i = 0; i < items.Length; i++)
        {
            Console.WriteLine($"[{i + 1}] {items[i]} | Сума: {items[i].GetCost():F2} грн");
        }
    }
}