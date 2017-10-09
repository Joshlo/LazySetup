using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace LazySetup.Token
{
    public interface ITokenHandler
    {
        Task<TokenClaims> ValidateAsync(string identifier, string password);
        Task<TokenClaims> ValidateAsync(string refreshToken);
        Task StoreRefreshTokenAsync(string identifier, string refreshToken);
    }
}
