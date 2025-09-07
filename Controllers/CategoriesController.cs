using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Ecommerce.DTOs;
using Ecommerce.Models;
using Ecommerce.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Ecommerce.Controllers;

[ApiController]
[Route("api/[controller]")]
public class CategoriesController(
  ICategoryService categoryService
) : ControllerBase
{
  private readonly ICategoryService _categoryService = categoryService;
  [HttpGet]
  public async Task<IActionResult> GetCategories()
  {
    var categories = await _categoryService.GetCategoriesAsync();
    return Ok(categories);
  }

  [HttpGet]
  [Route("{id}")]
  public async Task<IActionResult> GetByCategoryId([FromRoute] int id)
  {
    var category = await _categoryService.GetByIdAsync(id);

    if (category == null)
      return NotFound();

    return Ok(category);
  }

  [HttpGet]
  [Route("{id}/products")]
  public async Task<IActionResult> GetCategoryProducts([FromRoute] int id)
  {
    var products = await _categoryService.GetCategoryProductsAsync(categoryId: id);
    return Ok(products);
  }

  [Authorize(Roles = "Admin")]
  [HttpPost]
  public async Task<IActionResult> CreateCategory([FromBody] CategoryCreateDto categoryCreateDto)
  {
    if (!ModelState.IsValid)
    {
      return BadRequest(ModelState);
    }

    var category = new Category
    {
      Name = categoryCreateDto.Name,
      Description = categoryCreateDto.Description
    };

    await _categoryService.CreateCategoryAsync(category);
    return CreatedAtAction(nameof(GetByCategoryId), new { id = category.Id }, category);
  }

}
