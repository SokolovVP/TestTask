using System.Text.Json;

namespace CurrencyApplication;

public class ExchangeRateJsonProvider : IExchangeRateProvider
{
    public async Task<List<Currency.ExchangeRate>> GetExchangeRatesAsync()
    {
        string ExchangeRatesFilePath = "ExchangeRatesFile.json";

        if (!File.Exists(ExchangeRatesFilePath))
        {
            throw new Exception($"Exchange rates file ({ExchangeRatesFilePath}) doesn't exist");
        }

        List<Currency.ExchangeRate>? DeserializedList;

        using (FileStream ExchangeRateFileStream  = new FileStream(ExchangeRatesFilePath, FileMode.Open))
        {
            DeserializedList = await JsonSerializer.DeserializeAsync<List<Currency.ExchangeRate>>(ExchangeRateFileStream);
        }

        return DeserializedList ?? throw new Exception($"Can't deserialize to {typeof(List<Currency.ExchangeRate>)}");
    }

    
}