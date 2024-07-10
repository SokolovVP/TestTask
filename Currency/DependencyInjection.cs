using Microsoft.Extensions.DependencyInjection;

namespace CurrencyApplication;

public static class DependencyInjection
{
    public static IServiceCollection AddCurrencyServices(this IServiceCollection services)
    {
        return services
            .AddScoped<IExchangeRateProvider, ExchangeRateJsonProvider>()
            .AddScoped<IExchangeRateRepository, ExchangeRateRepository>()
            .AddSingleton<IMoneyConverter, MoneyConverter>()
            .AddSingleton<IMoneyOperations, MoneyOperations>();
    }
}