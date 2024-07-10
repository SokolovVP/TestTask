namespace CurrencyApplication;

public interface IMoneyOperations
{
    public Money Add(Money MoneyObject1, Money MoneyObject2, CurrencyList TargetCurrency);
    public Money Sub(Money MoneyObject1, Money MoneyObject2, CurrencyList TargetCurrency);
}