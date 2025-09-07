using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Ecommerce.Data;
using Ecommerce.DTOs;
using Ecommerce.Models;
using Microsoft.EntityFrameworkCore;

namespace Ecommerce.Services;

public class CategoryService(
  AppDbContext context
) : ICategoryService
{
  private readonly AppDbContext _context = context;
  public async Task<Category> CreateCategoryAsync(Category category)
  {
    await _context.Categories.AddAsync(category);
    await _context.SaveChangesAsync();
    return category;
  }
  public async Task<Category?> GetByIdAsync(int categoryId)
  {
    var category = await _context.Categories.FindAsync(categoryId);
    return category;
  }
  public async Task<List<Category>> GetCategoriesAsync()
  {
    var categories = await _context.Categories.ToListAsync();
    return categories;
  }
  public async Task<List<Product>> GetCategoryProductsAsync(int categoryId)
  {
    var products = await _context.Products.Where(p => p.CategoryId == categoryId).ToListAsync();
    return products;
  }
}
