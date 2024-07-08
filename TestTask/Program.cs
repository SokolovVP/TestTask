using Microsoft.Extensions.DependencyInjection;
using CurrencyApplication;

var ServiceCollection = new ServiceCollection().AddScoped<IExchangeRateProvider, ExchangeRateJsonProvider>();
var ServiceProvider = ServiceCollection.BuildServiceProvider();
IExchangeRateProvider? _rateProvider = ServiceProvider.GetService<IExchangeRateProvider>();

var cur = new Currency(_rateProvider);


MoneyConverter moneyConverter = new MoneyConverter(cur);
MoneyOperations moneyOperations = new MoneyOperations(moneyConverter);

Money GBPSum = new Money(Currency.CurrencyList.GBP, 4);
Money RubSum = moneyConverter.ConvertToNewCurrency(GBPSum, Currency.CurrencyList.RUB);
Console.WriteLine($"SUM IN GBP: {GBPSum.Amount} = {RubSum.Amount} {(Currency.CurrencyList)RubSum.CurrentCurrency}");


Money CNYSum = new Money(Currency.CurrencyList.CNY, 5);
RubSum = moneyConverter.ConvertToNewCurrency(CNYSum, Currency.CurrencyList.RUB);
Console.WriteLine($"SUM IN CNY: {CNYSum.Amount} = {RubSum.Amount} {(Currency.CurrencyList)RubSum.CurrentCurrency}");


Money CHFSum = new Money(Currency.CurrencyList.CHF, 6);
RubSum = moneyConverter.ConvertToNewCurrency(CHFSum, Currency.CurrencyList.RUB);
Console.WriteLine($"SUM IN CHF: {CHFSum.Amount} = {RubSum.Amount} {(Currency.CurrencyList)RubSum.CurrentCurrency}");


Money AEDSum = new Money(Currency.CurrencyList.AED, 8);
RubSum = moneyConverter.ConvertToNewCurrency(AEDSum, Currency.CurrencyList.RUB);
Console.WriteLine($"SUM IN AED: {AEDSum.Amount} = {RubSum.Amount} {(Currency.CurrencyList)RubSum.CurrentCurrency}");


Console.WriteLine($"{CNYSum.Amount} {(Currency.CurrencyList)CNYSum.CurrentCurrency} + " +
    $"{CHFSum.Amount} {(Currency.CurrencyList)CHFSum.CurrentCurrency} = " +
    $"{moneyOperations.Add(CNYSum, CHFSum, Currency.CurrencyList.RUB).Amount} ");

Console.WriteLine($"{GBPSum.Amount} {(Currency.CurrencyList)GBPSum.CurrentCurrency} - " +
    $"{AEDSum.Amount} {(Currency.CurrencyList)AEDSum.CurrentCurrency} = " +
    $"{moneyOperations.Sub(GBPSum, AEDSum, Currency.CurrencyList.RUB).Amount} ");