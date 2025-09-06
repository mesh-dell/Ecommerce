using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Ecommerce.Models;

public enum Status
{
  Pending,
  Shipped,
  Delivered,
}
public class Order
{
  public int Id { get; set; }
  public string UserId { get; set; } = string.Empty;
  public DateTime OrderDate { get; set; }
  public decimal TotalAmount { get; set; }
  public Status Status { get; set; }
}
