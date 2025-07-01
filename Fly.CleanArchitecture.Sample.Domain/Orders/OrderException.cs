using Fly.Fast.Domain;

namespace Fly.CleanArchitecture.Sample.Domain.Orders;

public class OrderException : DomainException
{
    public OrderException()
    {
    }

    public OrderException(string message) : base(message)
    {
    }

    public OrderException(string message, Exception inner) : base(message, inner)
    {
    }
}