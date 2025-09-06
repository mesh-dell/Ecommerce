using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Ecommerce.Models;

public class Product
{
  public int Id { get; set; }
  public string Name { get; set; } = string.Empty;
  public string Description { get; set; } = string.Empty;
  public decimal Price { get; set; }
  public int StockQuantity { get; set; }
  public int CategoryId { get; set; }
  public virtual Category Category { get; set; } = null!;
}
