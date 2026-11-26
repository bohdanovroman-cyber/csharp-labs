using System.Reflection;
using ClinicApp.Attributes;

namespace ClinicApp.Utils;

public static class ModelValidator
{
    public static ValidationResult Validate(object obj)
    {
        ArgumentNullException.ThrowIfNull(obj);

        var result = new ValidationResult();

        foreach (var prop in obj.GetType().GetProperties(BindingFlags.Public | BindingFlags.Instance))
        {
            if (prop.GetIndexParameters().Length > 0) continue;

            object? value = prop.GetValue(obj);

            var required = prop.GetCustomAttribute<RequiredAttribute>();
            if (required != null)
            {
                bool empty = value == null || (value is string s && string.IsNullOrWhiteSpace(s));
                if (empty)
                {
                    result.AddError($"{prop.Name}: поле {required.ErrorMessage}");
                    continue;
                }
            }

            var maxLen = prop.GetCustomAttribute<MaxLengthAttribute>();
            if (maxLen != null && value is string str && str.Length > maxLen.Length)
            {
                result.AddError($"{prop.Name}: довжина {str.Length} перевищує максимум {maxLen.Length}");
            }

            var minVal = prop.GetCustomAttribute<MinValueAttribute>();
            if (minVal != null && value != null && value is IConvertible)
            {
                try
                {
                    double number = Convert.ToDouble(value);
                    if (number < minVal.Min)
                        result.AddError($"{prop.Name}: значення {number} менше за мінімум {minVal.Min}");
                }
                catch (Exception ex) when (ex is FormatException or InvalidCastException)
                {
                }
            }
        }

        return result;
    }

    public static void PrintInfo(Type type)
    {
        ArgumentNullException.ThrowIfNull(type);

        Console.WriteLine($"Тип: {type.Name}");
        Console.WriteLine("Властивості:");

        foreach (var prop in type.GetProperties(BindingFlags.Public | BindingFlags.Instance))
        {
            var attrs = prop.GetCustomAttributes(inherit: false)
                            .OfType<Attribute>()
                            .Select(Describe)
                            .ToList();

            string attrText = attrs.Count > 0 ? string.Join(", ", attrs) : "без атрибутів";
            Console.WriteLine($"  - {prop.PropertyType.Name} {prop.Name} [{attrText}]");
        }
    }

    private static string Describe(Attribute a) => a switch
    {
        RequiredAttribute => "Required",
        MaxLengthAttribute m => $"MaxLength({m.Length})",
        MinValueAttribute m => $"MinValue({m.Min})",
        _ => a.GetType().Name.Replace("Attribute", "")
    };
}