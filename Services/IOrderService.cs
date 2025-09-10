using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Ecommerce.DTOs;
using Ecommerce.Models;

namespace Ecommerce.Services;

public interface IOrderService
{
  public Task<Order?> CreateOrderAsync(string userId);
  public Task<List<Order>?> GetOrdersAsync(string userId);
  public Task<Order?> GetOrderByIdAsync(int orderId);
  public Task<Order?> CancelOrderAsync(int orderId);
  public Task<Order?> UpdateOrderStatus(int orderId, UpdateOrderStatusDto updateOrderStatusDto);
  public Task<OrderSummaryDto> GetOrdersSummary();
}
