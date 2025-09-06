using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Ecommerce.Models;

public class Cart
{
  public int Id { get; set; }
  public string UserId { get; set; } = string.Empty;
}
