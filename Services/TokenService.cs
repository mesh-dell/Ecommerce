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

public class TokenService(UserManager<AppUser> userManager) : ITokenService
{
  private readonly UserManager<AppUser> _userManager = userManager;
  public async Task<string> CreateTokenAsync(AppUser user)
  {
    var roles = await _userManager.GetRolesAsync(user);
    var claims = new List<Claim>
    {
      new(JwtRegisteredClaimNames.GivenName, user.UserName!),
      new(JwtRegisteredClaimNames.Email, user.Email!),
      new(ClaimTypes.NameIdentifier, user.Id)
    };

    claims.AddRange(roles.Select(role => new Claim(ClaimTypes.Role, role)));

    var key = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes(Environment.GetEnvironmentVariable("JWT_KEY") ??
     throw new InvalidOperationException("JWT_KEY environment variable is missing or empty.")));
    var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
    var token = new JwtSecurityToken(
      issuer: Environment.GetEnvironmentVariable("JWT_ISSUER"),
      audience: Environment.GetEnvironmentVariable("JWT_AUDIENCE"),
      claims: claims,
      expires: DateTime.Now.AddHours(1),
      signingCredentials: creds
    );
    return new JwtSecurityTokenHandler().WriteToken(token);
  }
}
