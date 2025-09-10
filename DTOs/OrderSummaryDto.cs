using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Ecommerce.DTOs;

public class OrderSummaryDto
{
  public int TotalOrders { get; set; }
  public decimal TotalRevenue { get; set; }
  public decimal AverageOrderValue { get; set; }
  public int PendingOrders { get; set; }
  public int ShippedOrders { get; set; }
  public int DeliveredOrders { get; set; }
}
