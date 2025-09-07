using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Ecommerce.DTOs;
using Ecommerce.Models;

namespace Ecommerce.Services;

public interface ICategoryService
{
  public Task<Category> CreateCategoryAsync(Category category);
  public Task<List<Category>> GetCategoriesAsync();
  public Task<List<Product>> GetCategoryProductsAsync(int categoryId);
  public Task<Category?> GetByIdAsync(int categoryId);
}
