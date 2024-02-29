using LaborationBlazorShop.Client.DTOs;
using LaborationBlazorShop.Data;
using LaborationBlazorShop.Models;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query.Internal;
using System.Security.Claims;

namespace LaborationBlazorShop.Services
{
    public class UserService
    {
        private readonly ApplicationDbContext _context;
        private readonly AuthenticationStateProvider _authenticationStateProvider;
        private readonly CurrencyService _currencyService;

        public UserService(ApplicationDbContext context, AuthenticationStateProvider authenticationStateProvider, CurrencyService currencyService)
        {
            _context = context;
            _authenticationStateProvider = authenticationStateProvider;
            _currencyService = currencyService;
        }

        public string Name = string.Empty;

        public string Address = string.Empty;

        public List<Product> ProductsToCheckout = new();

        public async Task<string>? IsUserAuthenticated()
        {
            var authState = await _authenticationStateProvider.GetAuthenticationStateAsync();

            if (authState.User.Identity.IsAuthenticated)
            {
                return authState.User.Claims.Where(c => c.Type == ClaimTypes.NameIdentifier).FirstOrDefault().Value;
            }
            return null;
        }

        public async Task<ApplicationUser> GetUserProducts(ApplicationUser user)
        {
            var userProducts = _context.Users.Include(u => u.Cart).First(u => u.Id == user.Id);
            ProductsToCheckout = userProducts.Cart.Products.ToList();
            return userProducts;
        }

        public async Task<List<ProductDTO>> ConfirmedProducts()
        {
            List<ProductDTO> confirmedProducts = new(0);

            foreach (var product in ProductsToCheckout)
            {
                confirmedProducts.Add(
                   new ProductDTO
                   {
                       Title = product.Title,
                       Image = product.Image,
                       Price = product.Price,
                       OriginalPrice = product.OriginalPrice,
                       ExchangeRate = _currencyService.ExchangeRate,
                       SelectedCurrency = _currencyService.SelectedCurrency
                   });
            }
            return confirmedProducts;
        }
    }
}
