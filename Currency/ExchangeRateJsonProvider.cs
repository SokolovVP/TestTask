using System.Text.Json;

namespace CurrencyApplication;

public class ExchangeRateJsonProvider : IExchangeRateProvider
{
    public async Task<IEnumerable<ExchangeRate>> GetExchangeRatesAsync()
    {
        string ExchangeRatesFilePath = "ExchangeRatesFile.json";

        if (!File.Exists(ExchangeRatesFilePath))
        {
            throw new Exception($"Exchange rates file ({ExchangeRatesFilePath}) doesn't exist");
        }

        IEnumerable<ExchangeRate>? DeserializedRates;

        using (FileStream ExchangeRateFileStream  = new FileStream(ExchangeRatesFilePath, FileMode.Open))
        {
            DeserializedRates = await JsonSerializer.DeserializeAsync<IEnumerable<ExchangeRate>>(ExchangeRateFileStream);
        }

        return DeserializedRates ?? throw new Exception($"Can't deserialize to {typeof(List<ExchangeRate>)}");
    }

    
}