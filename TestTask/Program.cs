using Microsoft.Extensions.DependencyInjection;
using CurrencyApplication;

var ServiceCollection = new ServiceCollection().AddScoped<IExchangeRateProvider, ExchangeRateJsonProvider>();
var ServiceProvider = ServiceCollection.BuildServiceProvider();
IExchangeRateProvider? _rateProvider = ServiceProvider.GetService<IExchangeRateProvider>();

var cur = new CurrencyRepository(_rateProvider);


MoneyConverter moneyConverter = new MoneyConverter(cur);
MoneyOperations moneyOperations = new MoneyOperations(moneyConverter);

Money GBPSum = new Money(CurrencyList.GBP, 4);
Money RubSum = moneyConverter.ConvertToNewCurrency(GBPSum, CurrencyList.RUB);
Console.WriteLine($"SUM IN GBP: {GBPSum.Amount} = {RubSum.Amount} {(CurrencyList)RubSum.CurrentCurrency}");


Money CNYSum = new Money(CurrencyList.CNY, 5);
RubSum = moneyConverter.ConvertToNewCurrency(CNYSum, CurrencyList.RUB);
Console.WriteLine($"SUM IN CNY: {CNYSum.Amount} = {RubSum.Amount} {(CurrencyList)RubSum.CurrentCurrency}");


Money CHFSum = new Money(CurrencyList.CHF, 6);
RubSum = moneyConverter.ConvertToNewCurrency(CHFSum, CurrencyList.RUB);
Console.WriteLine($"SUM IN CHF: {CHFSum.Amount} = {RubSum.Amount} {(CurrencyList)RubSum.CurrentCurrency}");


Money AEDSum = new Money(CurrencyList.AED, 8);
RubSum = moneyConverter.ConvertToNewCurrency(AEDSum, CurrencyList.RUB);
Console.WriteLine($"SUM IN AED: {AEDSum.Amount} = {RubSum.Amount} {(CurrencyList)RubSum.CurrentCurrency}");


Console.WriteLine($"{CNYSum.Amount} {(CurrencyList)CNYSum.CurrentCurrency} + " +
    $"{CHFSum.Amount} {(CurrencyList)CHFSum.CurrentCurrency} = " +
    $"{moneyOperations.Add(CNYSum, CHFSum, CurrencyList.RUB).Amount} ");

Console.WriteLine($"{GBPSum.Amount} {(CurrencyList)GBPSum.CurrentCurrency} - " +
    $"{AEDSum.Amount} {(CurrencyList)AEDSum.CurrentCurrency} = " +
    $"{moneyOperations.Sub(GBPSum, AEDSum, CurrencyList.RUB).Amount} ");