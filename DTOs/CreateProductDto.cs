using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace Ecommerce.DTOs;

public class CreateProductDto
{
  [Required]
  public string Name { get; set; } = string.Empty;
  [Required]
  public string Description { get; set; } = string.Empty;
  [Required]
  public decimal Price { get; set; }
  [Required]
  public int StockQuantity { get; set; }
  [Required]
  public int CategoryId { get; set; }
}
