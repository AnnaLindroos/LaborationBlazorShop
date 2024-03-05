using Humanizer;
using LaborationBlazorShop.Models;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;
using System.Security.Cryptography.Xml;
using System;
using LaborationBlazorShop.Data;
using Microsoft.EntityFrameworkCore;

namespace LaborationBlazorShop.Services;

public class ProductService
{
    private readonly ApplicationDbContext _applicationDbContext;

    public ProductService(ApplicationDbContext applicationDbContext)
    {
        _applicationDbContext = applicationDbContext;
    }

    public async Task<List<Product>> GetAllProducts()
    {
        await Task.Delay(1000);
        return await _applicationDbContext.Products.ToListAsync();
    }

    public async Task<Product?> GetProductById(int id)
    {
        return await _applicationDbContext.Products.FirstOrDefaultAsync(x => x.Id == id);
    }
}
