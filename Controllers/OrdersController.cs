using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Ecommerce.DTOs;
using Ecommerce.Models;
using Ecommerce.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Abstractions;

namespace Ecommerce.Controllers;

[ApiController]
[Route("api/[controller]")]
public class OrdersController(
  IOrderService orderService,
  UserManager<AppUser> userManager
) : ControllerBase
{
  private readonly IOrderService _orderService = orderService;
  private readonly UserManager<AppUser> _userManager = userManager;

  [HttpGet]
  [Authorize(Roles = "Admin")]
  [Route("{id:int}")]
  public async Task<IActionResult> GetOrderById([FromRoute] int id)
  {
    var order = await _orderService.GetOrderByIdAsync(id);
    if (order == null) return NotFound();
    return Ok(order);
  }

  [HttpPost]
  [Authorize]
  public async Task<IActionResult> CreateOrder()
  {
    var user = await _userManager.GetUserAsync(User);
    if (user == null) return Unauthorized();
    var order = await _orderService.CreateOrderAsync(user.Id);

    if (order == null)
      return NotFound("Cart empty or not found");

    return CreatedAtAction(nameof(GetOrderById), new { id = order.Id }, order);
  }

  [HttpGet]
  [Authorize]
  public async Task<IActionResult> GetOrders()
  {
    var user = await _userManager.GetUserAsync(User);
    if (user == null) return Unauthorized();

    var orders = await _orderService.GetOrdersAsync(user.Id);
    if (orders == null) return NotFound();

    return Ok(orders);
  }

  [HttpDelete("{orderId:int}/cancel")]
  [Authorize]
  public async Task<IActionResult> CancelOrder([FromRoute] int orderId)
  {
    var order = await _orderService.CancelOrderAsync(orderId);

    if (order == null)
    {
      return NotFound();
    }
    return NoContent();
  }

  [HttpPut]
  [Route("{orderId:int}/status")]
  [Authorize(Roles = "Admin")]
  public async Task<IActionResult> UpdateOrderStatus(
    [FromBody] UpdateOrderStatusDto updateOrderStatusDto,
    [FromRoute] int orderId
  )
  {
    if (!ModelState.IsValid)
    {
      return BadRequest(ModelState);
    }

    var updatedOrder = await _orderService.UpdateOrderStatus(orderId, updateOrderStatusDto);
    if (updatedOrder == null) return NotFound();

    return Ok(updatedOrder);
  }

  [HttpGet("summary")]
  [Authorize(Roles = "Admin")]
  public async Task<IActionResult> GetOrderSummary()
  {
    var OrderSummary = await _orderService.GetOrdersSummary();
    return Ok(OrderSummary);
  }

}
