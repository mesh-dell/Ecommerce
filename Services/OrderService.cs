using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Ecommerce.Data;
using Ecommerce.DTOs;
using Ecommerce.Models;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.EntityFrameworkCore;

namespace Ecommerce.Services;

public class OrderService(
  AppDbContext context
) : IOrderService
{
  private readonly AppDbContext _context = context;

  public async Task<Order?> CancelOrderAsync(int orderId)
  {
    var order = await _context.Orders
      .Include(c => c.OrderItems)
      .FirstOrDefaultAsync(c => c.Id == orderId);

    if (order == null)
    {
      return null;
    }

    _context.Orders.Remove(order);
    await _context.SaveChangesAsync();
    return order;
  }

  public async Task<Order?> CreateOrderAsync(string userId)
  {
    var cart = await _context.Carts
      .Include(c => c.CartItems)
      .FirstOrDefaultAsync(c => c.UserId == userId);

    if (cart == null || cart.CartItems.Count == 0)
      return null;

    var order = new Order
    {
      UserId = userId,
      OrderDate = DateTime.UtcNow,
      TotalAmount = cart.CartItems.Sum(c => c.Price * c.Quantity),
      Status = Status.Pending,
      OrderItems = []
    };

    await _context.Orders.AddAsync(order);
    await _context.SaveChangesAsync();

    foreach (CartItem item in cart.CartItems)
    {
      var orderItem = new OrderItem
      {
        OrderId = order.Id,
        ProductId = item.ProductId,
        Quantity = item.Quantity,
        Price = item.Price
      };
      await _context.OrderItems.AddAsync(orderItem);
    }

    // clear cart
    _context.CartItems.RemoveRange(cart.CartItems);
    await _context.SaveChangesAsync();

    return order;
  }

  public async Task<Order?> GetOrderByIdAsync(int orderId)
  {
    var order = await _context.Orders
      .Include(c => c.OrderItems)
      .FirstOrDefaultAsync(c => c.Id == orderId);

    if (order == null)
    {
      return null;
    }

    return order;
  }

  public async Task<List<Order>?> GetOrdersAsync(string userId)
  {
    var orders = await _context.Orders
      .Include(c => c.OrderItems)
      .Where(c => c.UserId == userId)
      .ToListAsync();

    if (orders == null)
      return null;

    return orders;
  }

  public async Task<OrderSummaryDto> GetOrdersSummary()
  {
    var orders = await _context.Orders
      .Include(c => c.OrderItems)
      .ToListAsync();

    if (orders.Count == 0)
    {
      return new OrderSummaryDto
      {
        TotalOrders = 0,
        TotalRevenue = 0.0m,
        AverageOrderValue = 0.0m,
        PendingOrders = 0,
        ShippedOrders = 0,
        DeliveredOrders = 0
      };
    }
    var totalOrders = orders.Count;
    var totalRevenue = orders.Sum(c => c.TotalAmount);
    var averageOrderValue = totalRevenue / totalOrders;
    var pendingOrders = orders
        .Where(c => c.Status == Status.Pending).Count();
    var shippedOrders = orders
        .Where(c => c.Status == Status.Shipped).Count();
    var deliveredOrders = orders
        .Where(c => c.Status == Status.Delivered).Count();

    var OrderSummary = new OrderSummaryDto
    {
      TotalOrders = totalOrders,
      TotalRevenue = totalRevenue,
      AverageOrderValue = averageOrderValue,
      PendingOrders = pendingOrders,
      ShippedOrders = shippedOrders,
      DeliveredOrders = deliveredOrders
    };

    return OrderSummary;
  }

  public async Task<Order?> UpdateOrderStatus(int orderId, UpdateOrderStatusDto updateOrderStatusDto)
  {
    var order = await _context.Orders
      .Include(c => c.OrderItems)
      .FirstOrDefaultAsync(c => c.Id == orderId);

    if (order == null)
      return null;

    order.Status = updateOrderStatusDto.status;
    await _context.SaveChangesAsync();

    return order;
  }
}
