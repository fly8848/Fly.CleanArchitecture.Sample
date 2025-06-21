using MediatR;

namespace Fly.CleanArchitecture.Sample.Domain.Common;

public abstract record DomainEvent : INotification
{
}