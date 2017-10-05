using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace LazySetup.Token
{
    public interface ITokenValidation
    {
        Task<TokenClaims> ValidateAsync(string identifier, string password);
        Task<TokenClaims> ValidateAsync(string refreshToken);
        Task StoreRefreshTokenAsync(string refreshToken);
    }
}
