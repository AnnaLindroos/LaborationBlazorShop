using LaborationBlazorShop.Models;
using Microsoft.EntityFrameworkCore.Storage.Json;
using Newtonsoft.Json;
using System.Linq.Expressions;

namespace LaborationBlazorShop.Services;

public class CurrencyService
{
    private readonly string apiKey;

    public CurrencyService(string apiKey)
    {
        this.apiKey = apiKey;
    }

    // Add a property to store the selected currency

    public decimal ExchangeRate { get; set; } = 1;
    public string SelectedCurrency { get; set; } = "USD";

    public event Action OnStateChange;

    public void NotifyStateChanged() => OnStateChange?.Invoke();

    public async Task<decimal> ConvertCurrencyAsync(HttpClient httpClient, string? chosenCurrency)
    {
        if (chosenCurrency == "USD" || chosenCurrency is null || chosenCurrency == "")
            return 1;

        var urlApi = ($"https://api.api-ninjas.com/v1/exchangerate?pair=USD_{chosenCurrency}");

        try
        {
            httpClient.DefaultRequestHeaders.Clear();
            httpClient.DefaultRequestHeaders.Add("X-Api-Key", apiKey);

            var response = await httpClient.GetAsync(urlApi);

            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                var exchangeRate = JsonConvert.DeserializeObject<Currency>(content);

                if (exchangeRate is not null && exchangeRate.exchange_rate is not 0)
                {
                    return exchangeRate.exchange_rate;
                }
            }
            else
            {
                Console.WriteLine($"Error: {response.StatusCode}, {await response.Content.ReadAsStringAsync()}");
            }
        }
        catch (Exception exception)
        {
            Console.WriteLine($"Exception: {exception.Message}");
        }
        return 1;
    }
}
