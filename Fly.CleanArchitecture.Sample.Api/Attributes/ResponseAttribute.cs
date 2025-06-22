namespace Fly.CleanArchitecture.Sample.Api.Attributes;

[AttributeUsage(AttributeTargets.Method | AttributeTargets.Class)]
public class ResponseAttribute : Attribute
{
    public ResponseAttribute(bool isEnabled = true)
    {
        IsEnabled = isEnabled;
    }

    public bool IsEnabled { get; init; }
}