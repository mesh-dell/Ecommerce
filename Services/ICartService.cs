using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Ecommerce.DTOs;
using Ecommerce.Models;

namespace Ecommerce.Services;

public interface ICartService
{
  public Task<Cart?> GetCartAsync(string userId);
  public Task<CartItem?> GetCartItemById(int cartItemId);
  public Task<CartSummaryDto> GetCartSummary(string userId);
  public Task<CartItem> AddToCartAsync(CartItem cartItem, string userId);
  public Task<CartItem?> UpdateItemQuantity(int itemId, UpdateItemQuantityDto updateItemQuantityDto);
  public Task<CartItem?> RemoveFromCart(int itemId);
  public Task<Cart?> ClearCartAsync(string userId);
}
