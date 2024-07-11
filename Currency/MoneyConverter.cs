namespace CurrencyApplication;

public class MoneyConverter : IMoneyConverter
{
    private readonly IExchangeRateRepository _exchangeRateRepository;

    public MoneyConverter(IExchangeRateRepository exchangeRateRepository)
    {
        _exchangeRateRepository = exchangeRateRepository;
    }

    public Money ConvertToNewCurrency(Money SourceMoney, CurrencyList TargetCurrency)
    {
        if (SourceMoney.CurrentCurrency == TargetCurrency)
        {
            return new Money(TargetCurrency, SourceMoney.Amount);
        }

        var ExchangeRates = _exchangeRateRepository.GetAllExchangeRates();

        if (ExchangeRates is not null)
        {
            var ExchangeRatePair = ExchangeRates
                .Where(x => x.CurrentCurrency == SourceMoney.CurrentCurrency && x.TargetCurrency == TargetCurrency)
                .FirstOrDefault();

            if (ExchangeRatePair is not null)
            {
                decimal NewValueInTargetCurrency = SourceMoney.Amount * ExchangeRatePair.ExchangeRateValue;
                return new Money(TargetCurrency, NewValueInTargetCurrency);
            }
            else
            {
                var AllExchangeSourceRates = _exchangeRateRepository.GetExchangeRatesForCurrentCurrency(SourceMoney.CurrentCurrency);
                List<CurrencyList> availableCurrenciesForSource =
                    AllExchangeSourceRates.Select(x => x.TargetCurrency).ToList();

                var AllExchangeTargetRates = _exchangeRateRepository.GetExchangeRatesForCurrentCurrency(TargetCurrency);
                List<CurrencyList> availableCurrenciesForTarget =
                    AllExchangeTargetRates.Select(x => x.TargetCurrency).ToList();

                var TransitCurrency = ExchangeRates
                    .FirstOrDefault(x => x.CurrentCurrency == SourceMoney.CurrentCurrency
                    && availableCurrenciesForTarget.Contains(x.TargetCurrency));

                //source can be converted to X and target can be converted to X

                if (TransitCurrency is not null)
                {
                    var AmountInTransitCurrency = SourceMoney.Amount * TransitCurrency.ExchangeRateValue;

                    var Transit2TargetCurrencyExchangeRate = ExchangeRates
                        .FirstOrDefault(x => x.CurrentCurrency == TargetCurrency
                        && x.TargetCurrency == TransitCurrency.TargetCurrency);

                    var NewValueInTargetCurrency = AmountInTransitCurrency / Transit2TargetCurrencyExchangeRate.ExchangeRateValue;

                    return new Money(TargetCurrency, NewValueInTargetCurrency);
                }

                var AllAvailableCurrenciesToConvertToTarget = ExchangeRates
                    .Where(x => x.TargetCurrency == TargetCurrency).Select(c => c.CurrentCurrency).ToList();

                TransitCurrency = ExchangeRates
                    .FirstOrDefault(x => x.CurrentCurrency == SourceMoney.CurrentCurrency
                    && AllAvailableCurrenciesToConvertToTarget.Contains(x.TargetCurrency) 
                    && availableCurrenciesForSource.Contains(x.TargetCurrency));

                //source can be converted to X and X can be converted to target

                if (TransitCurrency is not null)
                {

                    var AmountInTransitCurrency = SourceMoney.Amount * TransitCurrency.ExchangeRateValue;

                    var Transit2TargetCurrencyExchangeRate = ExchangeRates
                        .FirstOrDefault(x => x.CurrentCurrency == TransitCurrency.TargetCurrency
                        && x.TargetCurrency == TargetCurrency);

                    var NewValueInTargetCurrency = AmountInTransitCurrency * Transit2TargetCurrencyExchangeRate.ExchangeRateValue;

                    return new Money(TargetCurrency, NewValueInTargetCurrency);
                }

                var AllAvailableCurrenciesToConvertToSource = ExchangeRates
                    .Where(x => x.TargetCurrency == SourceMoney.CurrentCurrency).Select(c => c.CurrentCurrency).ToList();

                TransitCurrency = ExchangeRates
                    .FirstOrDefault(x => AllAvailableCurrenciesToConvertToSource.Contains(x.CurrentCurrency)
                    && AllAvailableCurrenciesToConvertToTarget.Contains(x.CurrentCurrency)
                    && x.TargetCurrency == SourceMoney.CurrentCurrency);

                //X can be converted to both source and target

                if (TransitCurrency is not null)
                {
                    var AmountInTransitCurrency = SourceMoney.Amount / TransitCurrency.ExchangeRateValue;

                    var Transit2TargetCurrencyExchangeRate = ExchangeRates
                        .FirstOrDefault(x => x.CurrentCurrency == TransitCurrency.CurrentCurrency
                        && x.TargetCurrency == TargetCurrency);

                    var NewValueInTargetCurrency = AmountInTransitCurrency * Transit2TargetCurrencyExchangeRate.ExchangeRateValue;

                    return new Money(TargetCurrency, NewValueInTargetCurrency);
                }

                //X can be converted to source and target can be converted to X
                TransitCurrency = ExchangeRates
                    .FirstOrDefault(x => AllAvailableCurrenciesToConvertToSource.Contains(x.CurrentCurrency)
                    && availableCurrenciesForTarget.Contains(x.CurrentCurrency)
                    && x.TargetCurrency == SourceMoney.CurrentCurrency);

                if (TransitCurrency is not null)
                {
                    var AmountInTransitCurrency = SourceMoney.Amount / TransitCurrency.ExchangeRateValue;

                    var Transit2TargetCurrencyExchangeRate = ExchangeRates
                        .FirstOrDefault(x => x.TargetCurrency == TransitCurrency.CurrentCurrency
                        && x.CurrentCurrency == TargetCurrency);

                    var NewValueInTargetCurrency = AmountInTransitCurrency / Transit2TargetCurrencyExchangeRate.ExchangeRateValue;

                    return new Money(TargetCurrency, NewValueInTargetCurrency);
                }
            }
        }
        throw new Exception($"No available exchange rates for this operation (source = {SourceMoney.CurrentCurrency}), " +
            $"(target = {TargetCurrency})");
    }
}