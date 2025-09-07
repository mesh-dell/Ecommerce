using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Ecommerce.DTOs;
using Ecommerce.Helpers;
using Ecommerce.Models;

namespace Ecommerce.Services;

public interface IProductService
{
  public Task<List<Product>> GetProductsAsync(QueryObject queryObject);
  public Task<Product?> GetProductByIdAsync(int id);
  public Task<Product> CreateProductAsync(Product product);
  public Task<Product?> DeleteProductAsync(int id);
  public Task<Product?> UpdateProductAsync(int id, UpdateProductDto updateProductDto);
}
