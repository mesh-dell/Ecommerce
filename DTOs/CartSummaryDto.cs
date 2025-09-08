using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Ecommerce.DTOs;

public class CartSummaryDto
{
  public int ItemCount { get; set; }
  public decimal Subtotal { get; set; }
  public decimal Total { get; set; }
}
