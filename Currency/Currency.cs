namespace CurrencyApplication;

public class Currency
{
    private readonly IExchangeRateProvider _exchangeRateProvider;

    public Currency(IExchangeRateProvider exchangeRateProvider)
    {
        _exchangeRateProvider = exchangeRateProvider;
    }

    public List<ExchangeRate> GetAllExchangeRates()
    {
        return _exchangeRateProvider.GetExchangeRatesAsync().Result ?? throw new Exception("No exchange rates found");
    }

    public List<ExchangeRate> GetExchangeRatesForCurrentCurrency(CurrencyList currency)
    {
        var exchangeRates = _exchangeRateProvider.GetExchangeRatesAsync().Result;

        if (exchangeRates is null)
        {
            throw new Exception($"No exchange rates found for {(CurrencyList)currency}");
        }

        return exchangeRates.Where(r => r.CurrentCurrency == currency).ToList();
    }

    public enum CurrencyList
    {
        EUR, RUB, USD, RSD, JPY, GBP, CNY, CHF, AED, MXN
    }

    public record ExchangeRate(CurrencyList CurrentCurrency, CurrencyList TargetCurrency, decimal ExchangeRateValue);
}