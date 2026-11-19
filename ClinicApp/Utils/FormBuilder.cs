using System.Globalization;
using System.Reflection;
using ClinicApp.Attributes;

namespace ClinicApp.Utils;

public static class FormBuilder
{
    public static T Build<T>() where T : new()
    {
        T obj = new T();

        var props = typeof(T)
            .GetProperties(BindingFlags.Public | BindingFlags.Instance)
            .Where(p => p.GetSetMethod() != null && p.GetIndexParameters().Length == 0);

        foreach (var prop in props)
        {
            while (true)
            {
                Console.Write($"{prop.Name}{Hint(prop)}: ");
                string? input = Console.ReadLine();

                if (TryConvert(input, prop.PropertyType, out object? value))
                {
                    prop.SetValue(obj, value);
                    break;
                }

                Console.WriteLine($"  Невірний формат. Очікується {prop.PropertyType.Name}.");
            }
        }

        return obj;
    }

    private static string Hint(PropertyInfo prop)
    {
        var parts = new List<string>();
        if (prop.GetCustomAttribute<RequiredAttribute>() != null) parts.Add("обов'язкове");
        var max = prop.GetCustomAttribute<MaxLengthAttribute>();
        if (max != null) parts.Add($"макс. {max.Length} символів");
        var min = prop.GetCustomAttribute<MinValueAttribute>();
        if (min != null) parts.Add($"мін. {min.Min}");
        return parts.Count > 0 ? $" ({string.Join(", ", parts)})" : "";
    }

    private static bool TryConvert(string? input, Type type, out object? value)
    {
        value = null;
        Type target = Nullable.GetUnderlyingType(type) ?? type;

        if (string.IsNullOrWhiteSpace(input))
        {
            if (target == typeof(string)) { value = string.Empty; return true; }
            if (Nullable.GetUnderlyingType(type) != null) return true;
            return false;
        }

        try
        {
            if (target == typeof(string)) { value = input; return true; }
            if (target.IsEnum)
            {
                if (Enum.TryParse(target, input, true, out object? e)) { value = e; return true; }
                return false;
            }
            value = Convert.ChangeType(input, target, CultureInfo.InvariantCulture);
            return true;
        }
        catch (Exception ex) when (ex is FormatException or InvalidCastException or OverflowException)
        {
            return false;
        }
    }
}