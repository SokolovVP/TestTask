namespace CurrencyApplication;

public class Money
{
    public Currency.CurrencyList CurrentCurrency { get; set; }

    public decimal Amount;

    public Money(Currency.CurrencyList CurrentCurrency, decimal Amount)
    {
        this.CurrentCurrency = CurrentCurrency;
        this.Amount = Amount;
    }
}