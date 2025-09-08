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

namespace Ecommerce.Controllers;

[ApiController]
[Route("api/[controller]")]
public class CartController(
  ICartService cartService,
  UserManager<AppUser> userManager
) : ControllerBase
{
  private readonly ICartService _cartService = cartService;
  private readonly UserManager<AppUser> _userManager = userManager;

  [HttpGet]
  [Authorize]
  public async Task<IActionResult> GetCart()
  {
    var user = await _userManager.GetUserAsync(User);
    if (user == null) return Unauthorized();

    var cart = await _cartService.GetCartAsync(user.Id);
    if (cart == null)
    {
      return NotFound();
    }
    return Ok(cart);
  }

  [HttpGet]
  [Route("items/{id:int}")]
  public async Task<IActionResult> GetCartItem([FromRoute] int id)
  {
    var item = await _cartService.GetCartItemById(cartItemId: id);
    if (item == null)
    {
      return NotFound();
    }
    return Ok(item);
  }

  [HttpPost("items")]
  [Authorize]
  public async Task<IActionResult> AddItem(AddToCartDto cartDto)
  {
    var user = await _userManager.GetUserAsync(User);
    if (user == null) return Unauthorized();

    var cartItem = new CartItem
    {
      ProductId = cartDto.ProductId,
      Quantity = cartDto.Quantity
    };

    var createdItem = await _cartService.AddToCartAsync(cartItem, user.Id);
    return CreatedAtAction(nameof(GetCartItem), new { id = createdItem.Id }, createdItem);
  }

  [HttpPut]
  [Route("items/{id:int}")]
  [Authorize]
  public async Task<IActionResult> UpdateItemQuantity([FromRoute] int id, [FromBody] UpdateItemQuantityDto updateItemQuantityDto)
  {
    var item = await _cartService.UpdateItemQuantity(id, updateItemQuantityDto);
    if (item == null)
    {
      return NotFound();
    }
    return Ok(item);
  }

  [HttpDelete]
  [Route("items/{id:int}")]
  [Authorize]
  public async Task<IActionResult> RemoveItem([FromRoute] int id)
  {
    var item = await _cartService.RemoveFromCart(id);
    if (item == null)
    {
      return NotFound();
    }
    return NoContent();
  }

  [HttpDelete]
  [Authorize]
  public async Task<IActionResult> ClearCart()
  {
    var user = await _userManager.GetUserAsync(User);
    if (user == null) return Unauthorized();

    var cart = await _cartService.ClearCartAsync(user.Id);
    if (cart == null) return NotFound();
    return Ok(cart);
  }

  [HttpGet("summary")]
  [Authorize]
  public async Task<IActionResult> GetSummary()
  {
    var user = await _userManager.GetUserAsync(User);
    if (user == null) return Unauthorized();

    var summary = await _cartService.GetCartSummary(user.Id);
    return Ok(summary);
  }
}
