namespace CurrencyApplication;

public interface IMoneyConverter
{
    public Money ConvertToNewCurrency(Money SourceMoney, CurrencyList TargetCurrency);
}