using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Ecommerce.DTOs;
using Ecommerce.Helpers;
using Ecommerce.Models;
using Ecommerce.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Ecommerce.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ProductsController
(
  IProductService productService
) : ControllerBase
{
  private readonly IProductService _productService = productService;

  [HttpGet]
  public async Task<IActionResult> GetProducts([FromQuery] QueryObject query)
  {
    var products = await _productService.GetProductsAsync(query);
    return Ok(products);
  }

  [HttpGet]
  [Route("{id:int}")]
  public async Task<IActionResult> GetProductById([FromRoute] int id)
  {
    var product = await _productService.GetProductByIdAsync(id);
    if (product == null)
    {
      return NotFound();
    }
    return Ok(product);
  }

  [HttpPost]
  [Authorize(Roles = "Admin")]
  public async Task<IActionResult> CreateProduct([FromBody] CreateProductDto createProductDto)
  {
    if (!ModelState.IsValid)
    {
      return BadRequest(ModelState);
    }

    var product = new Product
    {
      Name = createProductDto.Name,
      Description = createProductDto.Description,
      Price = createProductDto.Price,
      StockQuantity = createProductDto.StockQuantity,
      CategoryId = createProductDto.CategoryId
    };

    await _productService.CreateProductAsync(product);
    return CreatedAtAction(nameof(GetProductById), new { id = product.Id }, product);
  }

  [HttpPut]
  [Route("{id:int}")]
  [Authorize(Roles = "Admin")]
  public async Task<IActionResult> UpdateProduct([FromRoute] int id, [FromBody] UpdateProductDto updateProductDto)
  {
    if (!ModelState.IsValid)
    {
      return BadRequest(ModelState);
    }
    var product = await _productService.UpdateProductAsync(id, updateProductDto);
    if (product == null)
    {
      return NotFound();
    }
    return Ok(product);
  }

  [HttpDelete]
  [Route("{id:int}")]
  [Authorize(Roles = "Admin")]
  public async Task<IActionResult> DeleteProduct([FromRoute] int id)
  {
    var product = await _productService.DeleteProductAsync(id);
    if (product == null)
    {
      return NotFound();
    }
    return NoContent();
  }
}
