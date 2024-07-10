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

        List<ExchangeRate>? DeserializedList;

        using (FileStream ExchangeRateFileStream  = new FileStream(ExchangeRatesFilePath, FileMode.Open))
        {
            DeserializedList = await JsonSerializer.DeserializeAsync<List<ExchangeRate>>(ExchangeRateFileStream);
        }

        return DeserializedList ?? throw new Exception($"Can't deserialize to {typeof(List<ExchangeRate>)}");
    }

    
}