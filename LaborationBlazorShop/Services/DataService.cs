using LaborationBlazorShop.Client.DTOs;
using LaborationBlazorShop.Client.Services;
using LaborationBlazorShop.Models;

namespace LaborationBlazorShop.Services
{
    public class DataService : IDataService
    {
        private readonly UserService _userService;

        public DataService(UserService userService)
        {
            _userService = userService;
        }
        public async Task<string> GetNameAsync()
        {
            string name = _userService.Name;
            return name;

        }

        public async Task<string> GetAddressAsync()
        {
            string address = _userService.Address;
            return address;
        }

        public async Task<List<ProductDTO>> GetOrderedProductsAsync()
        {
            return await _userService.ConfirmedProducts();
        }

    }
}
