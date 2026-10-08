using ClinicApp.Enums;

namespace ClinicApp.Utils;

public static class ClinicFormatter
{
    public static string FormatBloodType(BloodType bloodType)
    {
        return bloodType switch
        {
            BloodType.APositive => "A+",
            BloodType.ANegative => "A-",
            BloodType.BPositive => "B+",
            BloodType.BNegative => "B-",
            BloodType.ABPositive => "AB+",
            BloodType.ABNegative => "AB-",
            BloodType.OPositive => "O+",
            BloodType.ONegative => "O-",
            BloodType.Unknown => "Невідомо",
            _ => "Невідомо"
        };
    }

    public static string FormatSpeciality(
        Speciality speciality)
    {
        return speciality switch
        {
            Speciality.General => "Загальна",
            Speciality.Cardiology => "Кардіологія",
            Speciality.Neurology => "Неврологія",
            Speciality.Pediatrics => "Педіатрія",
            Speciality.Surgery => "Хірургія",
            Speciality.Orthopedics => "Ортопедія",
            Speciality.Dermatology => "Дерматологія",
            Speciality.Emergency => "Швидка допомога",
            _ => "Невідомо"
        };
    }

    public static string FormatAge(int age)
    {
        int twoDigits = age % 100;

        if (twoDigits is >= 11 and <= 19)
            return $"{age} років";

        return age % 10 switch
        {
            1 => $"{age} рік",
            >= 2 and <= 4 => $"{age} роки",
            _ => $"{age} років"
        };
    }

    public static string FormatPhone(string phone)
    {
        if (string.IsNullOrWhiteSpace(phone))
            return phone;

        string digits = string.Concat(
            phone.Where(char.IsDigit));

        if (digits.Length == 12 &&
            digits.StartsWith("380"))
        {
            digits = digits[2..];
        }

        if (digits.Length == 10)
        {
            return $"({digits[..3]}) " +
                   $"{digits.Substring(3, 3)}-" +
                   $"{digits[6..]}";
        }

        return phone;
    }
}