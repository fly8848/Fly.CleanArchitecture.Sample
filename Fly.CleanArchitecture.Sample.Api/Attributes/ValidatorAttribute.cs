namespace Fly.CleanArchitecture.Sample.Api.Attributes;

[AttributeUsage(AttributeTargets.Method | AttributeTargets.Class)]
public class ValidatorAttribute : Attribute
{
    public ValidatorAttribute(bool isEnabled = true)
    {
        IsEnabled = isEnabled;
    }

    public bool IsEnabled { get; init; }
}