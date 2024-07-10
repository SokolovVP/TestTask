using CurrencyApplication;
using Microsoft.Extensions.DependencyInjection;

var Services = new ServiceCollection().AddCurrencyServices();
var ServiceProvider = Services.BuildServiceProvider();

var _moneyConverter = ServiceProvider.GetService<IMoneyConverter>();

Money money1 = new Money(CurrencyList.EUR, 5);
Money? moneyRub = _moneyConverter?.ConvertToNewCurrency(money1, CurrencyList.RUB);


Console.WriteLine($"5 eur = {moneyRub?.Amount} rub");

