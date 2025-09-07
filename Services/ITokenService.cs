using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Ecommerce.Models;

namespace Ecommerce.Services;

public interface ITokenService
{
  public Task<string> CreateTokenAsync(AppUser user);
}
