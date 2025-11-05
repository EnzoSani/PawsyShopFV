using Pawsy.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pawsy.Application.Services.Interface
{
    public interface ITokenService
    {
        Task<string> GenerateAccessTokenAsync(ApplicationUser user, IList<string> roles);
        RefreshToken GenerateRefreshToken(string userId);
    }
}
