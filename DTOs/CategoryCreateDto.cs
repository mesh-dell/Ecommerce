using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Ecommerce.DTOs;

public class CategoryCreateDto
{

  [Required]
  public string Name { get; set; } = string.Empty;
  [Required]
  public string Description { get; set; } = string.Empty;
}
