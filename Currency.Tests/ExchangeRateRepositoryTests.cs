namespace Currency.Tests;

public class ExchangeRateRepositoryTests
{
    [Fact]
    public void ExchangeRateRepositoryReturnsAllRates()
    {
        var IExchangeRateProviderMock = new Mock<IExchangeRateProvider>();

        IExchangeRateProviderMock
            .Setup(_exchangeRateProvider => _exchangeRateProvider
            .GetExchangeRatesAsync().Result)
            .Returns(GetAllRates());

        var _exchangeRateRepository = new ExchangeRateRepository(IExchangeRateProviderMock.Object);

        var result = _exchangeRateRepository.GetAllExchangeRates();

        Assert.NotNull(result);
        var exchangeRates = Assert.IsAssignableFrom<IEnumerable<ExchangeRate>>(result);
        Assert.Equal(GetAllRates().Count(), exchangeRates.Count());
    }

    [Fact]
    public void ExchangeRateRepositoryReturnsRatesForSpecifiedCurrency()
    {
        CurrencyList CurrentCurrencyForTest = CurrencyList.RUB;

        var IExchangeRateProviderMock = new Mock<IExchangeRateProvider>();

        IExchangeRateProviderMock
            .Setup(_exchangeRateProvider => _exchangeRateProvider
            .GetExchangeRatesAsync().Result)
            .Returns(() => GetAllRates());

        var _exchangeRateRepository = new ExchangeRateRepository(IExchangeRateProviderMock.Object);

        var result = _exchangeRateRepository.GetExchangeRatesForCurrentCurrency(CurrentCurrencyForTest);

        Assert.NotNull(result);
        var exchangeRates = Assert.IsAssignableFrom<IEnumerable<ExchangeRate>>(result);
        Assert.Equal(GetAllRates().Where(ER => ER.CurrentCurrency == CurrentCurrencyForTest), exchangeRates);
    }

    private List<ExchangeRate> GetAllRates()
    {
        return new List<ExchangeRate>
        {
            new ExchangeRate(CurrencyList.EUR, CurrencyList.RUB, 95.34m),
            new ExchangeRate(CurrencyList.RUB, CurrencyList.USD, 0.011m),
            new ExchangeRate(CurrencyList.RUB, CurrencyList.RSD, 1.23m),
            new ExchangeRate(CurrencyList.EUR, CurrencyList.GBP, 0.84m),
            new ExchangeRate(CurrencyList.RUB, CurrencyList.AED, 0.04m)
        };
    }
}
