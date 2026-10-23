using ClinicApp.Models;
using ClinicApp.Enums;
using ClinicApp.Interfaces;
using ClinicApp.Managers;
using ClinicApp;
using System.Globalization;

CultureInfo.DefaultThreadCurrentCulture = CultureInfo.InvariantCulture;
CultureInfo.DefaultThreadCurrentUICulture = CultureInfo.InvariantCulture;
CultureInfo.CurrentCulture = CultureInfo.InvariantCulture;
CultureInfo.CurrentUICulture = CultureInfo.InvariantCulture;

Console.OutputEncoding = System.Text.Encoding.UTF8;

Clinic clinic = new Clinic("Медична Клініка");

clinic.Patients.Add(new Patient(
    "Олександр",
    "Коваленко",
    new DateTime(1990, 5, 14),
    BloodType.APositive,
    "0971112233",
    "kovalenko@gmail.com"));

clinic.Patients.Add(new Patient(
    "Марія",
    "Шевченко",
    new DateTime(2003, 11, 28),
    BloodType.ONegative,
    "0504445566",
    "m.shevchenko@gmail.com"));

clinic.Patients.Add(new Patient(
    "Іван",
    "Бондаренко",
    new DateTime(1978, 3, 2),
    BloodType.BPositive,
    "0637778899",
    "i.bond@gmail.com"));

clinic.Doctors.Add(new Doctor(
    "Андрій",
    "Мельник",
    Speciality.Cardiology,
    "LIC-1001",
    "0671110001",
    new WorkSchedule(8, 16)));

clinic.Doctors.Add(new Doctor(
    "Олена",
    "Ткаченко",
    Speciality.General,
    "LIC-1002",
    "0671110002",
    new WorkSchedule(9, 17)));

clinic.Doctors.Add(new Doctor(
    "Сергій",
    "Кравченко",
    Speciality.Surgery,
    "LIC-1003",
    "0671110003",
    new WorkSchedule(10, 18)));

clinic.Appointments.Book(1, 1, DateTime.Now.AddDays(1).AddHours(2), 30);
clinic.Appointments.Book(2, 2, DateTime.Now.AddDays(2).AddHours(4), 45);
clinic.Appointments.Book(3, 3, DateTime.Now.AddDays(3).AddHours(1), 60);

// ===== Медичні записи =====
clinic.MedicalRecords.Add(new Diagnosis(
    1, 1, DateTime.Today.AddDays(-20),
    "I10", "Гіпертонічна хвороба", true));

clinic.MedicalRecords.Add(new LabResult(
    1, 1, DateTime.Today.AddDays(-18),
    "Гемоглобін", 145, "г/л", "120–160", true));

clinic.MedicalRecords.Add(new Prescription(
    1, 1, DateTime.Today.AddDays(-5),
    "Лізиноприл", "10 мг", 30,
    "1 раз на добу вранці"));

clinic.MedicalRecords.Add(new Prescription(
    1, 2, DateTime.Today.AddDays(-60),
    "Ібупрофен", "200 мг", 7,
    "після їжі"));

clinic.MedicalRecords.Add(new Diagnosis(
    2, 2, DateTime.Today.AddDays(-10),
    "J06.9", "Гостра інфекція дихальних шляхів"));

clinic.MedicalRecords.Add(new LabResult(
    2, 2, DateTime.Today.AddDays(-9),
    "Глюкоза", 6.9, "ммоль/л", "3.9–5.6", false));

clinic.MedicalRecords.Add(new Prescription(
    2, 2, DateTime.Today.AddDays(-3),
    "Амоксицилін", "500 мг", 7,
    "3 рази на добу"));

clinic.MedicalRecords.Add(new Diagnosis(
    3, 3, DateTime.Today.AddDays(-30),
    "E11", "Цукровий діабет 2 типу", true));

clinic.MedicalRecords.Add(new Prescription(
    3, 3, DateTime.Today.AddDays(-20),
    "Метформін", "850 мг", 90,
    "2 рази на добу під час їжі"));

ShowMainMenu(clinic);

static void ShowMainMenu(Clinic clinic)
{
    while (true)
    {
        Console.WriteLine();
        Console.WriteLine("┌──────────────────────────────────────────────┐");
        Console.WriteLine("│               МЕДИЧНА КЛІНІКА                │");
        Console.WriteLine("├──────────────────────────────────────────────┤");
        Console.WriteLine("│ 1. Пацієнти       — реєстрація, пошук        │");
        Console.WriteLine("│ 2. Лікарі         — персонал, розклад        │");
        Console.WriteLine("│ 3. Записи         — прийоми, скасування      │");
        Console.WriteLine("│ 4. Медична картка — діагнози, рецепти        │");
        Console.WriteLine("│ 5. Рахунки        — оплата, борги            │");
        Console.WriteLine("│ 6. Звіт           — загальна статистика      │");
        Console.WriteLine("│ 0. Вийти                                     │");
        Console.WriteLine("└──────────────────────────────────────────────┘");
        Console.Write("Оберіть розділ: ");

        string choice = Console.ReadLine()!;

        switch (choice)
        {
            case "1":
                ShowPatientMenu(clinic);
                break;

            case "2":
                ShowDoctorMenu(clinic);
                break;

            case "3":
                ShowAppointmentMenu(clinic);
                break;

            case "4":
                ShowMedicalMenu(clinic);
                break;

            case "5":
                ShowBillingMenu(clinic);
                break;

            case "6":
                clinic.GenerateReport();
                break;

            case "0":
                Console.WriteLine("Завершення роботи програми...");
                return;

            default:
                Console.WriteLine("Невірний вибір! Спробуйте ще раз.");
                break;
        }
    }
}

static void ShowBillingMenu(Clinic clinic)
{
    while (true)
    {
        Console.WriteLine("\n──────────────── Рахунки ──────────────────");
        Console.WriteLine("1. Борги пацієнта");
        Console.WriteLine("2. Всі неоплачені записи");
        Console.WriteLine("3. Оплатити запис");
        Console.WriteLine("4. Загальний борг клініки");
        Console.WriteLine("0. Назад");
        Console.Write("Оберіть дію: ");

        string choice = Console.ReadLine()!;

        switch (choice)
        {
            case "1":
                Console.Write("Введіть ID пацієнта: ");

                if (int.TryParse(Console.ReadLine(), out int patientId))
                {
                    IPayable[] unpaid =
                        clinic.Billing.GetUnpaidByPatient(patientId);

                    Console.WriteLine(
                        $"\nНеоплачені записи пацієнта #{patientId}:");

                    clinic.Billing.DisplayUnpaid(unpaid);

                    Console.WriteLine(
                        $"Борг: {clinic.Billing.GetPatientDebt(patientId):F2} грн");
                }
                else
                {
                    Console.WriteLine(
                        "[Помилка формату]: ID має бути числом!");
                }
                break;

            case "2":
                clinic.Billing.DisplayUnpaid(
                    clinic.Billing.GetAllUnpaid());
                break;

            case "3":
                Console.Write("Введіть ID запису для оплати: ");

                if (int.TryParse(Console.ReadLine(), out int appId))
                {
                    if (clinic.Billing.PayAppointment(appId))
                    {
                        Console.WriteLine($"Запис [{appId}] оплачено.");
                    }
                    else
                    {
                        Console.WriteLine(
                            "Не вдалося оплатити: запис не знайдено, вже оплачений або скасований.");
                    }
                }
                else
                {
                    Console.WriteLine(
                        "[Помилка формату]: ID має бути числом!");
                }
                break;

            case "4":
                Console.WriteLine(
                    $"Загальний борг клініки: {clinic.Billing.GetTotalDebt():F2} грн");
                break;

            case "0":
                return;

            default:
                Console.WriteLine("Невірний вибір! Спробуйте ще раз.");
                break;
        }
    }
}

static void ShowPatientMenu(Clinic clinic)
{
    while (true)
    {
        Console.WriteLine("\n--- Управління пацієнтами ---");
        Console.WriteLine("1. Показати всіх пацієнтів");
        Console.WriteLine("2. Додати нового пацієнта");
        Console.WriteLine("3. Знайти пацієнта за ID");
        Console.WriteLine("4. Знайти пацієнта за ім'ям / прізвищем");
        Console.WriteLine("5. Видалити пацієнта за ID");
        Console.WriteLine("6. Переглянути статистику пацієнтів");
        Console.WriteLine("0. Повернутися в головне меню");
        Console.Write("Оберіть дію: ");

        string choice = Console.ReadLine()!;

        switch (choice)
        {
            case "1":
                clinic.Patients.DisplayAll();
                break;

            case "2":
                try
                {
                    Console.Write("Введіть ім'я: ");
                    string firstName = Console.ReadLine()!;

                    Console.Write("Введіть прізвище: ");
                    string lastName = Console.ReadLine()!;

                    Console.Write("Введіть дату народження (РРРР-ММ-ДД): ");
                    DateTime birthDate =
                        DateTime.Parse(Console.ReadLine()!);

                    Console.WriteLine("Оберіть групу крові:");
                    Console.WriteLine("0 - Unknown, 1 - APositive, 2 - ANegative, 3 - BPositive, 4 - BNegative, 5 - ABPositive, 6 - ABNegative, 7 - OPositive, 8 - ONegative");
                    Console.Write("Введіть номер групи крові (0-8): ");

                    int bloodChoice =
                        int.Parse(Console.ReadLine()!);

                    BloodType bloodType =
                        (BloodType)bloodChoice;

                    Console.Write("Введіть номер телефону: ");
                    string phone = Console.ReadLine()!;

                    Console.Write("Введіть email: ");
                    string email = Console.ReadLine()!;

                    Patient newPatient = new Patient(
                        firstName,
                        lastName,
                        birthDate,
                        bloodType,
                        phone,
                        email);

                    clinic.Patients.Add(newPatient);
                }
                catch (ArgumentException ex)
                {
                    Console.WriteLine(ex.Message);
                }
                catch (FormatException)
                {
                    Console.WriteLine(
                        "[Помилка формату]: Введено некоректне число або дату!");
                }
                break;

            case "3":
                try
                {
                    Console.Write("Введіть ID пацієнта: ");
                    int id = int.Parse(Console.ReadLine()!);

                    Patient? p = clinic.Patients.FindById(id);

                    if (p != null)
                    {
                        Console.WriteLine($"Знайдено: {p}");
                    }
                    else
                    {
                        Console.WriteLine(
                            $"Пацієнта з ID {id} не знайдено.");
                    }
                }
                catch (FormatException)
                {
                    Console.WriteLine(
                        "[Помилка формату]: ID має бути числом!");
                }
                break;

            case "4":
                Console.Write("Введіть ім'я або прізвище для пошуку: ");
                string name = Console.ReadLine()!;

                Patient[] found =
                    clinic.Patients.FindByName(name);

                if (found.Length == 0)
                {
                    Console.WriteLine("Пацієнтів не знайдено.");
                }
                else
                {
                    Console.WriteLine(
                        $"\nЗнайдено пацієнтів: {found.Length}");

                    for (int i = 0; i < found.Length; i++)
                    {
                        Console.WriteLine(found[i]);
                    }
                }
                break;

            case "5":
                try
                {
                    Console.Write("Введіть ID пацієнта для видалення: ");
                    int removeId =
                        int.Parse(Console.ReadLine()!);

                    if (clinic.Patients.RemoveById(removeId))
                    {
                        Console.WriteLine(
                            $"Пацієнта з ID {removeId} успішно видалено.");
                    }
                    else
                    {
                        Console.WriteLine(
                            $"Не вдалося видалити: пацієнта з ID {removeId} не знайдено.");
                    }
                }
                catch (FormatException)
                {
                    Console.WriteLine(
                        "[Помилка формату]: ID має бути числом!");
                }
                break;

            case "6":
                clinic.Patients.DisplayStats();
                break;

            case "0":
                return;

            default:
                Console.WriteLine(
                    "Невірний вибір! Спробуйте ще раз.");
                break;
        }
    }
}

static void ShowDoctorMenu(Clinic clinic)
{
    while (true)
    {
        Console.WriteLine("\n--- Управління лікарями ---");
        Console.WriteLine("1. Показати всіх лікарів");
        Console.WriteLine("2. Додати нового лікаря");
        Console.WriteLine("3. Знайти лікаря за ID");
        Console.WriteLine("4. Знайти лікарів за спеціальністю");
        Console.WriteLine("5. Вільні години лікаря");
        Console.WriteLine("6. Видалити лікаря за ID");
        Console.WriteLine("7. Переглянути статистику лікарів");
        Console.WriteLine("0. Повернутися в головне меню");
        Console.Write("Оберіть дію: ");

        string choice = Console.ReadLine()!;

        switch (choice)
        {
            case "1":
                clinic.Doctors.DisplayAll();
                break;

            case "2":
                try
                {
                    Console.Write("Введіть ім'я: ");
                    string firstName = Console.ReadLine()!;

                    Console.Write("Введіть прізвище: ");
                    string lastName = Console.ReadLine()!;

                    Console.WriteLine("Оберіть спеціальність:");
                    Console.WriteLine("0 - General, 1 - Cardiology, 2 - Neurology, 3 - Pediatrics, 4 - Surgery, 5 - Orthopedics, 6 - Dermatology, 7 - Emergency");
                    Console.Write("Введіть номер спеціальності (0-7): ");

                    int specChoice =
                        int.Parse(Console.ReadLine()!);

                    Speciality speciality =
                        (Speciality)specChoice;

                    Console.Write("Введіть номер ліцензії: ");
                    string licenseNumber =
                        Console.ReadLine()!;

                    Console.Write("Введіть номер телефону: ");
                    string phone = Console.ReadLine()!;

                    Console.Write(
                        "Початок робочого дня (година, за замовчуванням 8): ");

                    string startInput =
                        Console.ReadLine()!;

                    int startHour =
                        string.IsNullOrWhiteSpace(startInput)
                            ? 8
                            : int.Parse(startInput);

                    Console.Write(
                        "Кінець робочого дня (година, за замовчуванням 17): ");

                    string endInput =
                        Console.ReadLine()!;

                    int endHour =
                        string.IsNullOrWhiteSpace(endInput)
                            ? 17
                            : int.Parse(endInput);

                    WorkSchedule schedule =
                        new WorkSchedule(startHour, endHour);

                    Doctor newDoctor = new Doctor(
                        firstName,
                        lastName,
                        speciality,
                        licenseNumber,
                        phone,
                        schedule);

                    clinic.Doctors.Add(newDoctor);
                }
                catch (ArgumentException ex)
                {
                    Console.WriteLine(ex.Message);
                }
                catch (FormatException)
                {
                    Console.WriteLine(
                        "[Помилка формату]: Введено некоректне число!");
                }
                break;

            case "3":
                try
                {
                    Console.Write("Введіть ID лікаря: ");
                    int id = int.Parse(Console.ReadLine()!);

                    Doctor? doc = clinic.Doctors.FindById(id);

                    if (doc != null)
                    {
                        Console.WriteLine($"Знайдено: {doc}");
                    }
                    else
                    {
                        Console.WriteLine(
                            $"Лікаря з ID {id} не знайдено.");
                    }
                }
                catch (FormatException)
                {
                    Console.WriteLine(
                        "[Помилка формату]: ID має бути числом!");
                }
                break;

            case "4":
                Console.Write(
                    "Введіть спеціальність (General, Cardiology, Surgery...): ");

                string inputSpec = Console.ReadLine()!;

                if (Enum.TryParse<Speciality>(
                    inputSpec,
                    true,
                    out Speciality searchSpec))
                {
                    Doctor[] found =
                        clinic.Doctors.FindBySpeciality(searchSpec);

                    if (found.Length == 0)
                    {
                        Console.WriteLine(
                            "Лікарів за цією спеціальністю не знайдено.");
                    }
                    else
                    {
                        Console.WriteLine(
                            $"\nЗнайдено лікарів: {found.Length}");

                        for (int i = 0; i < found.Length; i++)
                        {
                            Console.WriteLine(found[i]);
                        }
                    }
                }
                else
                {
                    Console.WriteLine(
                        "Такої спеціальності не існує.");
                }
                break;

            case "5":
                try
                {
                    Console.Write("Введіть ID лікаря: ");
                    int freeDocId = int.Parse(Console.ReadLine()!);

                    Doctor? freeDoc =
                        clinic.Doctors.FindById(freeDocId);

                    if (freeDoc == null)
                    {
                        Console.WriteLine(
                            $"Лікаря з ID {freeDocId} не знайдено.");
                        break;
                    }

                    ISchedulable schedulable = freeDoc;

                    Console.Write("Введіть дату (РРРР-ММ-ДД): ");
                    DateTime slotDate =
                        DateTime.Parse(Console.ReadLine()!);

                    Console.Write("Скільки слотів показати: ");
                    int slotCount =
                        int.Parse(Console.ReadLine()!);

                    DateTime[] slots =
                        schedulable.GetAvailableSlots(
                            slotDate,
                            slotCount);

                    Console.WriteLine(
                        $"\nВільні години лікаря {freeDoc.FullName}:");

                    for (int i = 0; i < slots.Length; i++)
                    {
                        Console.WriteLine(slots[i].ToString("HH:mm"));
                    }
                }
                catch (ArgumentException ex)
                {
                    Console.WriteLine($"[Помилка]: {ex.Message}");
                }
                catch (FormatException)
                {
                    Console.WriteLine(
                        "[Помилка формату]: Введено некоректне число або дату!");
                }
                break;

            case "6":
                try
                {
                    Console.Write("Введіть ID лікаря для видалення: ");
                    int removeId =
                        int.Parse(Console.ReadLine()!);

                    if (clinic.Doctors.Remove(removeId))
                    {
                        Console.WriteLine(
                            $"Лікаря з ID {removeId} успішно видалено.");
                    }
                    else
                    {
                        Console.WriteLine(
                            $"Не вдалося видалити: лікаря з ID {removeId} не знайдено.");
                    }