﻿namespace CurrencyApplication;

public class Money
{
    public CurrencyList CurrentCurrency { get; set; }

    public decimal Amount;

    public Money(CurrencyList CurrentCurrency, decimal Amount)
    {
        this.CurrentCurrency = CurrentCurrency;
        this.Amount = Amount;
    }
}