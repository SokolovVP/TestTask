namespace CurrencyApplication;

public class ExchangeRateRepository : IExchangeRateRepository
{
    private readonly IExchangeRateProvider _exchangeRateProvider;

    public ExchangeRateRepository(IExchangeRateProvider exchangeRateProvider)
    {
        _exchangeRateProvider = exchangeRateProvider;
    }

    public List<ExchangeRate> GetAllExchangeRates()
    {
        return _exchangeRateProvider.GetExchangeRatesAsync().Result.ToList() ?? throw new Exception("No exchange rates found");
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
}