using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Ecommerce.DTOs;

public class RegisterRequestDto
{
  [Required]
  public string UserName { get; set; } = string.Empty;
  [EmailAddress]
  [Required]
  public string Email { get; set; } = string.Empty;
  [Required]
  public string Password { get; set; } = string.Empty;
}
