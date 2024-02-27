using LaborationBlazorShop.Data;
using LaborationBlazorShop.Models;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace LaborationBlazorShop.Services
{
    public class UserService
    {
        private readonly ApplicationDbContext _context;
        private readonly AuthenticationStateProvider _authenticationStateProvider;

        public UserService(ApplicationDbContext context, AuthenticationStateProvider authenticationStateProvider)
        {
            _context = context;
            _authenticationStateProvider = authenticationStateProvider;
        }

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
    }
}
