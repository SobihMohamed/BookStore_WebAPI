using Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Abstraction.Authentication
{
    public interface IJwtProvider
    {
        string GenerateToken(ApplicationUser user, IList<string> roles);
    }
}
