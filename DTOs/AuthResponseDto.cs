using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Ecommerce.DTOs;

public class AuthResponseDto
{
  public string UserName { get; set; } = string.Empty;
  public string Email { get; set; } = string.Empty;
  public string Token { get; set; } = string.Empty;
}
