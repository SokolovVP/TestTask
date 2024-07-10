namespace CurrencyApplication;

public record ExchangeRate(CurrencyList CurrentCurrency, CurrencyList TargetCurrency, decimal ExchangeRateValue);