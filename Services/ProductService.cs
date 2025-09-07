using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Ecommerce.Data;
using Ecommerce.DTOs;
using Ecommerce.Helpers;
using Ecommerce.Models;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;

namespace Ecommerce.Services;

public class ProductService(
  AppDbContext context
) : IProductService
{
  private readonly AppDbContext _context = context;
  public async Task<Product> CreateProductAsync(Product product)
  {
    await _context.Products.AddAsync(product);
    await _context.SaveChangesAsync();
    return product;
  }

  public async Task<Product?> DeleteProductAsync(int id)
  {
    var product = await _context.Products.FindAsync(id);
    if (product == null)
    {
      return null;
    }
    _context.Products.Remove(product);
    await _context.SaveChangesAsync();
    return product;
  }

  public async Task<Product?> GetProductByIdAsync(int id)
  {
    var product = await _context.Products.FindAsync(id);
    if (product == null)
    {
      return null;
    }
    return product;
  }

  public async Task<List<Product>> GetProductsAsync(QueryObject query)
  {
    var products = _context.Products.AsQueryable();

    // optional name filter
    if (!string.IsNullOrWhiteSpace(query.Name))
    {
      products = products.Where(p => p.Name.Contains(query.Name));
    }

    // optional sort
    if (!string.IsNullOrEmpty(query.SortBy))
    {
      if (query.SortBy.Equals("Price", StringComparison.OrdinalIgnoreCase))
      {
        products = query.IsDescending
         ? products.OrderByDescending(s => s.Price)
         : products.OrderBy(s => s.Price);
      }
    }

    // Pagination
    var skipNumber = (query.PageNumber - 1) * query.PageSize;
    return await products.Skip(skipNumber).Take(query.PageSize).ToListAsync();
  }

  public async Task<Product?> UpdateProductAsync(int id, UpdateProductDto productDto)
  {
    var product = await _context.Products.FindAsync(id);

    if (product == null)
      return null;

    product.Name = productDto.Name;
    product.Description = productDto.Description;
    product.Price = productDto.Price;
    product.StockQuantity = productDto.StockQuantity;
    product.CategoryId = productDto.CategoryId;

    await _context.SaveChangesAsync();
    return product;
  }
}
