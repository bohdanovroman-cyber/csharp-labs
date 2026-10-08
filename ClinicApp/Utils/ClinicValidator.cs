namespace ClinicApp.Utils;

public static class ClinicValidator
{
    public static void ValidatePhone(string phone)
    {
        if (string.IsNullOrWhiteSpace(phone) || phone.Length != 10)
            throw new ArgumentException(
                "Номер телефону має містити 10 цифр.",
                nameof(phone));

        foreach (char c in phone)
        {
            if (!char.IsDigit(c))
                throw new ArgumentException(
                    "Номер телефону має містити тільки цифри.",
                    nameof(phone));
        }
    }

    public static void ValidateName(string value, string fieldName)
    {
        if (string.IsNullOrWhiteSpace(value) || value.Length > 50)
            throw new ArgumentException(
                "Некоректне ім'я/прізвище, максимум 50 символів.",
                fieldName);
    }

    public static void ValidateDate(DateTime value, string fieldName)
    {
        if (value > DateTime.Today)
            throw new ArgumentOutOfRangeException(
                fieldName,
                "Дата не може бути в майбутньому.");

        if (value.Year < 1900)
            throw new ArgumentOutOfRangeException(
                fieldName,
                "Дата не може бути раніше 1900 року.");
    }

    public static void ValidatePositive(int value, string fieldName)
    {
        if (value <= 0)
            throw new ArgumentOutOfRangeException(
                fieldName,
                "Значення має бути більше 0.");
    }
}