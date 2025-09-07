using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Ecommerce.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;

namespace Ecommerce.Services;

public class TokenService(UserManager<AppUser> userManager, IConfiguration config) : ITokenService
{
  private readonly UserManager<AppUser> _userManager = userManager;
  private readonly IConfiguration _config = config;
  public async Task<string> CreateTokenAsync(AppUser user)
  {
    var roles = await _userManager.GetRolesAsync(user);
    var claims = new List<Claim>
    {
      new (JwtRegisteredClaimNames.GivenName, user.UserName!),
      new(JwtRegisteredClaimNames.Email, user.Email!),
    };
    
    claims.AddRange(roles.Select(role => new Claim(ClaimTypes.Role, role)));

    var key = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes(_config["Jwt:Key"]!));
    var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
    var token = new JwtSecurityToken(
      issuer: _config["Jwt:Issuer"],
      audience: _config["Jwt:Audience"],
      claims: claims,
      expires: DateTime.Now.AddHours(1),
      signingCredentials: creds
    );
    return new JwtSecurityTokenHandler().WriteToken(token);
  }
}
