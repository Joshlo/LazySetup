using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using LazySetup.Token;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;

namespace LazySetup.Web
{
    public class TokenHandler : ITokenHandler
    {
        public async Task<TokenClaims> ValidateAsync(string identifier, string password)
        {
            if (identifier == password) return new MyClaims {Username = identifier};

            return null;
        }

        public Task<TokenClaims> ValidateAsync(string refreshToken)
        {
            throw new NotImplementedException();
        }

        public Task StoreRefreshTokenAsync(string identifier, string refreshToken)
        {
            return Task.CompletedTask;
        }
    }
}
