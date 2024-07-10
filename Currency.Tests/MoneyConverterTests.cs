namespace Currency.Tests;

public class MoneyConverterTests
{
    [Fact]
    public void ConvertsDirectlyToNewCurrency()
    {
        var IExchangeRateProviderMock = new Mock<IExchangeRateProvider>();
        IExchangeRateProviderMock
            .Setup(_exchangeRateProvider => _exchangeRateProvider.GetExchangeRatesAsync().Result)
            .Returns(GetAllRates());

        var _exchangeRateRepository = new ExchangeRateRepository(IExchangeRateProviderMock.Object);

        var _moneyConverter = new MoneyConverter(_exchangeRateRepository);

        Money _moneyObject = new Money(CurrencyList.RUB, 9);

        var result = _moneyConverter.ConvertToNewCurrency(_moneyObject, CurrencyList.RSD);

        Assert.Equal(_moneyObject.Amount * GetAllRates()[2].ExchangeRateValue, result.Amount);
    }

    [Fact]
    public void ConvertsThroughTransitCurrencyRate()
    {
        var IExchangeRateProviderMock = new Mock<IExchangeRateProvider>();
        IExchangeRateProviderMock
            .Setup(_exchangeRateProvider => _exchangeRateProvider.GetExchangeRatesAsync().Result)
            .Returns(GetAllRates());

        var _exchangeRateRepository = new ExchangeRateRepository(IExchangeRateProviderMock.Object);

        var _moneyConverter = new MoneyConverter(_exchangeRateRepository);

        Money _moneyObject = new Money(CurrencyList.RUB, 9);

        var result = _moneyConverter.ConvertToNewCurrency(_moneyObject, CurrencyList.GBP);

        Assert.Equal(_moneyObject.Amount * GetAllRates()[5].ExchangeRateValue / GetAllRates()[6].ExchangeRateValue, result.Amount);
    }

    private List<ExchangeRate> GetAllRates()
    {
        return new List<ExchangeRate>
        {
            new ExchangeRate(CurrencyList.EUR, CurrencyList.RUB, 95.34m),
            new ExchangeRate(CurrencyList.RUB, CurrencyList.USD, 0.011m),
            new ExchangeRate(CurrencyList.RUB, CurrencyList.RSD, 1.23m),
            new ExchangeRate(CurrencyList.EUR, CurrencyList.GBP, 0.84m),
            new ExchangeRate(CurrencyList.RUB, CurrencyList.AED, 0.04m),
            new ExchangeRate(CurrencyList.RUB, CurrencyList.CHF, 0.01m),
            new ExchangeRate(CurrencyList.GBP, CurrencyList.CHF, 1.15m),
        };
    }
}