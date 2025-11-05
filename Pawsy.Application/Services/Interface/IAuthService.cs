using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pawsy.Application.Services.Interface
{
    public interface IAuthService
    {
        Task<(string accessToken, string refreshToken)> LoginAsync(string email, string password);
        Task<bool> RegisterAsync(string email, string password, string fullName);
        Task<(string accessToken, string refreshToken)> RefreshAsync(string refreshToken);
    }
}
