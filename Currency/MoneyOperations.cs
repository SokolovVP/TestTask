﻿namespace CurrencyApplication;

public class MoneyOperations : IMoneyOperations
{
    private readonly IMoneyConverter _moneyConverter;

    public MoneyOperations(IMoneyConverter moneyConverter)
    {
        _moneyConverter = moneyConverter;
    }

    public Money Add (Money MoneyObject1, Money MoneyObject2, CurrencyList TargetCurrency)
    {
        Money op1 = _moneyConverter.ConvertToNewCurrency(MoneyObject1, TargetCurrency);
        Money op2 = _moneyConverter.ConvertToNewCurrency(MoneyObject2, TargetCurrency);

        op1.Amount += op2.Amount;

        return op1;
    }

    public Money Sub (Money MoneyObject1, Money MoneyObject2, CurrencyList TargetCurrency)
    {
        Money op1 = _moneyConverter.ConvertToNewCurrency(MoneyObject1, TargetCurrency);
        Money op2 = _moneyConverter.ConvertToNewCurrency(MoneyObject2, TargetCurrency);

        op1.Amount -= op2.Amount;

        return op1;
    }
}