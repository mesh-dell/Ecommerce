using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Ecommerce.Data;
using Ecommerce.DTOs;
using Ecommerce.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query.SqlExpressions;

namespace Ecommerce.Services;

public class CartService(AppDbContext context) : ICartService
{
  private readonly AppDbContext _context = context;
  public async Task<CartItem> AddToCartAsync(CartItem cartItem, string userId)
  {
    var cart = await _context.Carts
      .Include(c => c.CartItems)
      .FirstOrDefaultAsync(c => c.UserId == userId);

    if (cart == null)
    {
      cart = new Cart
      {
        UserId = userId,
        CartItems = []
      };
      await _context.Carts.AddAsync(cart);
      await _context.SaveChangesAsync();
    }

    var existingItem = cart.CartItems.FirstOrDefault(i => i.ProductId == cartItem.ProductId);

    if (existingItem != null)
    {
      existingItem.Quantity += cartItem.Quantity;
    }
    else
    {
      cartItem.CartId = cart.Id;
      var product = await _context.Products.FindAsync(cartItem.ProductId) ?? throw new InvalidOperationException("Product not found");
      cartItem.Price = product.Price;
      await _context.CartItems.AddAsync(cartItem);
    }

    await _context.SaveChangesAsync();
    return cartItem;
  }

  public async Task<Cart?> ClearCartAsync(string userId)
  {
    var cart = await _context.Carts
      .Include(c => c.CartItems)
      .FirstOrDefaultAsync(c => c.UserId == userId);

    if (cart == null)
      return null;

    _context.CartItems.RemoveRange(cart.CartItems);
    await _context.SaveChangesAsync();
    return cart;
  }
  public async Task<Cart?> GetCartAsync(string userId)
  {
    return await _context.Carts
    .Include(c => c.CartItems)
    .FirstOrDefaultAsync(c => c.UserId == userId);
  }

  public async Task<CartItem?> GetCartItemById(int cartItemId)
  {
    var cartItem = await _context.CartItems.FindAsync(cartItemId);
    return cartItem;
  }

  public async Task<CartSummaryDto> GetCartSummary(string userId)
  {
    var cart = await _context.Carts
    .Include(c => c.CartItems)
    .FirstOrDefaultAsync(c => c.UserId == userId);

    if (cart == null || cart.CartItems.Count == 0)
    {
      return new CartSummaryDto
      {
        ItemCount = 0,
        Subtotal = 0,
        Total = 0
      };
    }
    decimal subtotal = cart.CartItems.Sum(i => i.Price * i.Quantity);
    decimal total = subtotal;

    return new CartSummaryDto
    {
      Subtotal = subtotal,
      Total = total,
      ItemCount = cart.CartItems.Sum(c => c.Quantity)
    };
  }

  public async Task<CartItem?> RemoveFromCart(int itemId)
  {
    var item = await _context.CartItems.FindAsync(itemId);
    if (item == null)
    {
      return null;
    }
    _context.CartItems.Remove(item);
    await _context.SaveChangesAsync();
    return item;
  }

  public async Task<CartItem?> UpdateItemQuantity(int itemId, UpdateItemQuantityDto updateItemQuantityDto)
  {
    var item = await _context.CartItems.FindAsync(itemId);
    if (item == null)
    {
      return null;
    }
    item.Quantity = updateItemQuantityDto.Quantity;
    await _context.SaveChangesAsync();
    return item;
  }
}
