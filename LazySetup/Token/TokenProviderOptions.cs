using System;
using System.Text;
using System.Threading.Tasks;
using Microsoft.IdentityModel.Tokens;

namespace LazySetup.Token
{
    public class TokenProviderOptions
    {
        /// <summary>
        /// Defaults to "/token"
        /// </summary>
        public string Path { get; set; } = "/token";

        public string Identifier { get; }

        public Func<string, string, Task<TokenClaims>> ValidateAsync { get; set; } = null;
        public Func<string, string, TokenClaims> Validate { get; set; } = null;
        
        /// <summary>
        /// Defaults to "LazySetup"
        /// </summary>
        public string Issuer { get; set; } = "LazySetup";

        /// <summary>
        /// Defaults to "http://localhost"
        /// </summary>
        public string Audience { get; set; } = "http://localhost";

        /// <summary>
        /// Defaults to 30 days
        /// </summary>
        public TimeSpan Expiration { get; set; } = TimeSpan.FromDays(30);

        /// <summary>
        /// Defaults to key: "NotSoSecure1234" and algorithm : HmacSha512
        /// </summary>
        public SigningCredentials SigningCredentials { get; set; } = new SigningCredentials(new SymmetricSecurityKey(Encoding.ASCII.GetBytes("NotSoSecure12345")), SecurityAlgorithms.HmacSha256);

        public TokenProviderOptions(string identifier)
        {
            Identifier = identifier;
        }
    }
}