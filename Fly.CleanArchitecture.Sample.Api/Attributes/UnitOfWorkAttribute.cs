namespace Fly.CleanArchitecture.Sample.Api.Attributes;

[AttributeUsage(AttributeTargets.Method | AttributeTargets.Class)]
public class UnitOfWorkAttribute : Attribute
{
    public UnitOfWorkAttribute(bool isEnabled = true)
    {
        IsEnabled = isEnabled;
    }

    public bool IsEnabled { get; init; }
}