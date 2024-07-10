namespace CurrencyApplication;

public interface ICurrencyRepository
{
    public List<ExchangeRate> GetAllExchangeRates();
    public List<ExchangeRate> GetExchangeRatesForCurrentCurrency(CurrencyList currency);
}