namespace CurrencyApplication;

public interface IExchangeRateProvider
{
    public Task<IEnumerable<ExchangeRate>> GetExchangeRatesAsync();
}