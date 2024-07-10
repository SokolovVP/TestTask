namespace CurrencyApplication;

public interface IExchangeRateRepository
{
    public List<ExchangeRate> GetAllExchangeRates();
    public List<ExchangeRate> GetExchangeRatesForCurrentCurrency(CurrencyList currency);
}