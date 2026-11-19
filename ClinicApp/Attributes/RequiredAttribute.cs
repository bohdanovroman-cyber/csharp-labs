namespace ClinicApp.Attributes;

[AttributeUsage(AttributeTargets.Property, AllowMultiple = false)]
public sealed class RequiredAttribute : Attribute
{
    public string ErrorMessage { get; }

    public RequiredAttribute(string errorMessage = "є обов'язковим")
    {
        ErrorMessage = errorMessage;
    }
}