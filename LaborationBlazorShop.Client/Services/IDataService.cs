using LaborationBlazorShop.Client.DTOs;

namespace LaborationBlazorShop.Client.Services
{
    public interface IDataService
    {
        Task<string> GetNameAsync();
        Task<string> GetAddressAsync();
        Task<List<ProductDTO>> GetOrderedProductsAsync();
    }
}
