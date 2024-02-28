using LaborationBlazorShop.Client.DTOs;
using System.Net.Http.Json;

namespace LaborationBlazorShop.Client.Services;

public class DataServiceApiService : IDataService
{
    private readonly HttpClient _httpClient;

    public DataServiceApiService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<string> GetAddressAsync()
    {
        var address = await _httpClient.GetFromJsonAsync<string>("/api/confirmationpage-address");
        return address;
    }

    public async Task<string> GetNameAsync()
    {
        var name = await _httpClient.GetFromJsonAsync<string>("/api/confirmationpage-name");
        return name;
    }

    public Task<List<ProductDTO>> GetOrderedProductsAsync()
    {
        var orderedProducts = _httpClient.GetFromJsonAsync<List<ProductDTO>>("/api/confirmationpage-productinfo");
        return orderedProducts;
    }
}
