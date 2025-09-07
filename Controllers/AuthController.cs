using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Ecommerce.Data;
using Ecommerce.DTOs;
using Ecommerce.Models;
using Ecommerce.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authorization.Infrastructure;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.FileProviders;

namespace Ecommerce.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AuthController
(
  ITokenService tokenService,
  UserManager<AppUser> userManager,
  SignInManager<AppUser> signInManager
)
: ControllerBase
{
  private readonly ITokenService _tokenService = tokenService;
  private readonly UserManager<AppUser> _userManager = userManager;
  private readonly SignInManager<AppUser> _signInManager = signInManager;

  [HttpPost]
  [Route("login")]
  public async Task<IActionResult> Login([FromBody] LoginRequestDto loginRequestDto)
  {
    if (!ModelState.IsValid)
      return BadRequest(ModelState);

    var user = await _userManager.Users.FirstOrDefaultAsync(x => x.UserName!.ToLower() == loginRequestDto.UserName.ToLower());
    if (user == null)
      return Unauthorized("Invalid Username or Password");

    var result = await _signInManager.CheckPasswordSignInAsync(user, loginRequestDto.Password, false);
    if (!result.Succeeded)
    {
      return Unauthorized("Invalid Username or Password");
    }

    return Ok(new AuthResponseDto
    {
      UserName = user.UserName!,
      Email = user.Email!,
      Token = await _tokenService.CreateTokenAsync(user)
    });
  }

  [HttpPost]
  [Route("register")]
  public async Task<IActionResult> Register([FromBody] RegisterRequestDto registerRequestDto)
  {
    if (!ModelState.IsValid)
    {
      return BadRequest(ModelState);
    }

    var appUser = new AppUser
    {
      UserName = registerRequestDto.UserName,
      Email = registerRequestDto.Email
    };

    var createdUser = await _userManager.CreateAsync(appUser, registerRequestDto.Password);

    if (createdUser.Succeeded)
    {
      var roleResult = await _userManager.AddToRoleAsync(appUser, "User");
      if (roleResult.Succeeded)
      {
        return Ok(
          new AuthResponseDto
          {
            UserName = registerRequestDto.UserName,
            Email = registerRequestDto.Email,
            Token = await _tokenService.CreateTokenAsync(appUser)
          }
        );
      }
      else
      {
        return StatusCode(500, roleResult.Errors);
      }
    }
    else
    {
      return StatusCode(500, createdUser.Errors);
    }
  }

  [Authorize(Roles = "Admin")]
  [HttpPost]
  [Route("assign-admin/{username}")]
  public async Task<IActionResult> AssignAdmin(string username)
  {
    var user = await _userManager.Users.FirstOrDefaultAsync(x => x.UserName!.ToLower() == username);
    if (user == null)
    {
      return NotFound();
    }
    var roleResult = await _userManager.AddToRoleAsync(user, "Admin");
    if (!roleResult.Succeeded)
    {
      return StatusCode(500, roleResult.Errors);
    }
    return Ok("Assigned admin successfully");
  }
}
