namespace CurrencyApplication;

public interface IExchangeRateProvider
{
    public Task<List<Currency.ExchangeRate>> GetExchangeRatesAsync();
}