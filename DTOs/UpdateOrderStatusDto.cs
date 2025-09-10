using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Ecommerce.Models;

namespace Ecommerce.DTOs;

public class UpdateOrderStatusDto
{
  public Status status { get; set; }
}
