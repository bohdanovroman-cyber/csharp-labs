namespace ClinicApp.Attributes;

[AttributeUsage(AttributeTargets.Property, AllowMultiple = false)]
public sealed class MinValueAttribute : Attribute
{
    public double Min { get; }

    public MinValueAttribute(double min)
    {
        Min = min;
    }
}