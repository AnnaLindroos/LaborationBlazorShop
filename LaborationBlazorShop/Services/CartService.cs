using LaborationBlazorShop.Data;
using LaborationBlazorShop.Models;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Cors.Infrastructure;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace LaborationBlazorShop.Services;

public class CartService
{
    public readonly ApplicationDbContext _db;
    public readonly UserManager<ApplicationUser> _userManager;
    public AuthenticationStateProvider _authenticationStateProvider;

    public CartService(ApplicationDbContext db, UserManager<ApplicationUser> userManager, AuthenticationStateProvider authenticationStateProvider)
    {
        _db = db;
        _userManager = userManager;
        _authenticationStateProvider = authenticationStateProvider;
    }

    public List<Product> ShoppingCart = new();

    public async Task AddToCart(Product product, string UserId)
    {
        var user = await _userManager.FindByIdAsync(UserId);
        if (user.CartId is null)
        {
            var newCart = new Cart
            {
                ApplicationUser = user,
                ApplicationUserId = UserId,
                Products = new List<Product>()
            };
            await _db.Carts.AddAsync(newCart);
            user.Cart = newCart;
            await _db.SaveChangesAsync();
        }

        Cart cart = await _db.Carts.FirstOrDefaultAsync(x => x.ApplicationUserId.Equals(UserId));

        var productToAdd = new CartProduct
        {
            Cart = cart,
            Product = product
        };

        var decreaseProductStock = await _db.Products.FirstOrDefaultAsync(x => x.Id.Equals(product.Id));
        
        cart.Products.Add(product);
        decreaseProductStock.Stock--;
        await _db.ProductsInCarts.AddAsync(productToAdd);
        await _db.SaveChangesAsync();
        ShoppingCart = await ViewCart(UserId);
    }
    public async Task<List<Product>> ViewCart(string UserId)
    {
        var user = await _userManager.FindByIdAsync(UserId);

        if (user.CartId is not null)
        {
            var cart = await _db.Carts.FirstAsync(x => x.ApplicationUserId.Equals(UserId));
            return cart.Products.ToList();
        }
        else
        {
            return new List<Product>();
        }
    }
}
