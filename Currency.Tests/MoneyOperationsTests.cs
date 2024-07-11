namespace Currency.Tests;

public class MoneyOperationsTests
{
    [Fact]
    public void AddReturnsCorrectResult()
    {
        var _exchangeRateProviderMock = new Mock<IExchangeRateProvider>();
        _exchangeRateProviderMock
            .Setup(exchangeRateProvider => exchangeRateProvider.GetExchangeRatesAsync().Result)
            .Returns(GetAllRates());

        IExchangeRateRepository _exchangeRateRepository = new ExchangeRateRepository(_exchangeRateProviderMock.Object);
        IMoneyConverter _moneyConverter = new MoneyConverter(_exchangeRateRepository);
        IMoneyOperations _moneyOperations = new MoneyOperations(_moneyConverter);

        Money obj1 = new Money(CurrencyList.RUB, 100);
        Money obj2 = new Money(CurrencyList.USD, 5);

        var result = _moneyOperations.Add(obj1, obj2, CurrencyList.RUB);

        Assert.NotNull(result);
        Assert.IsType<Money>(result);

        Assert.Equal(obj1.Amount + obj2.Amount * GetAllRates()[7].ExchangeRateValue * GetAllRates()[0].ExchangeRateValue,
            result.Amount);
    }

    [Fact]
    public void SubRetursCorrectResult()
    {
        var _exchangeRateProviderMock = new Mock<IExchangeRateProvider>();
        _exchangeRateProviderMock
            .Setup(exchangeRateProvider => exchangeRateProvider.GetExchangeRatesAsync().Result)
            .Returns(GetAllRates());

        IExchangeRateRepository _exchangeRateRepository = new ExchangeRateRepository(_exchangeRateProviderMock.Object);
        IMoneyConverter _moneyConverter = new MoneyConverter(_exchangeRateRepository);
        IMoneyOperations _moneyOperations = new MoneyOperations(_moneyConverter);

        Money obj1 = new Money(CurrencyList.USD, 5);
        Money obj2 = new Money(CurrencyList.RUB, 300);

        var result = _moneyOperations.Sub(obj1, obj2, CurrencyList.RUB);

        Assert.NotNull(result);
        Assert.IsType<Money>(result);

        Assert.Equal(obj1.Amount * GetAllRates()[7].ExchangeRateValue * GetAllRates()[0].ExchangeRateValue - obj2.Amount,
            result.Amount);
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
            new ExchangeRate(CurrencyList.USD, CurrencyList.EUR, 0.92m)
        };
    }
}