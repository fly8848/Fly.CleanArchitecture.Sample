namespace Fly.CleanArchitecture.Sample.Domain.Orders.Aggregates;

public record Money : ValueObject
{
    public Money(decimal amount, Currency currency)
    {
        if (amount < 0) throw new DomainException("金额不能为负数");

        Amount = amount;
        Currency = currency;
    }

    public decimal Amount { get; }
    public Currency Currency { get; }
}