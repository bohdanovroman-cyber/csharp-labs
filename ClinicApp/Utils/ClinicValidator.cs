using System.Text.RegularExpressions;

namespace ClinicApp.Utils;

public static class ClinicValidator
{
    private static readonly Regex PhoneRegex =
        new(@"^(?:\+38)?[0-9]{10}$", RegexOptions.Compiled);

    private static readonly Regex EmailRegex =
        new(@"^[^\s@]+@[^\s@]+\.[^\s@]+$", RegexOptions.Compiled);

    public static void ValidatePhone(string phone)
    {
        if (string.IsNullOrWhiteSpace(phone) ||
            !PhoneRegex.IsMatch(phone))
        {
            throw new ArgumentException(
                "Некоректний номер телефону. Використовуйте 10 цифр або +38 та 10 цифр.",
                nameof(phone));
        }
    }

    public static void ValidateEmail(string email)
    {
        if (string.IsNullOrWhiteSpace(email))
            return;

        if (!EmailRegex.IsMatch(email))
        {
            throw new ArgumentException(
                "Некоректний формат email.",
                nameof(email));
        }
    }

    public static void ValidateName(string value, string fieldName)
    {
        if (string.IsNullOrWhiteSpace(value) || value.Length > 50)
        {
            throw new ArgumentException(
                "Некоректне ім'я/прізвище, максимум 50 символів.",
                fieldName);
        }
    }

    public static void ValidateDate(DateTime value, string fieldName)
    {
        if (value > DateTime.Today)
        {
            throw new ArgumentOutOfRangeException(
                fieldName,
                "Дата не може бути в майбутньому.");
        }

        if (value.Year < 1900)
        {
            throw new ArgumentOutOfRangeException(
                fieldName,
                "Дата не може бути раніше 1900 року.");
        }
    }

    public static void ValidatePositive(int value, string fieldName)
    {
        if (value <= 0)
        {
            throw new ArgumentOutOfRangeException(
                fieldName,
                "Значення має бути більше 0.");
        }
    }
}