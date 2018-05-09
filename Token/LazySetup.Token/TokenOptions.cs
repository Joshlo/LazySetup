using System;
using System.Text;
using System.Threading.Tasks;
using Microsoft.IdentityModel.Tokens;

namespace LazySetup.Token
{
    public class TokenOptions
    {
        /// <summary>
        /// Defaults to "/token"
        /// </summary>
        public string Path { get; set; } = "/token";
        
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
        public TimeSpan Expiration { get; set; } = TimeSpan.FromDays(1);

        /// <summary>
        /// Defaults to key: "NotSoSecure1234" and algorithm : HmacSha512
        /// </summary>
        public SigningCredentials SigningCredentials { get; set; } = new SigningCredentials(new SymmetricSecurityKey(Encoding.ASCII.GetBytes("NotSoSecure12345")), SecurityAlgorithms.HmacSha256);

        /// <summary>
        /// Default is false.
        /// </summary>
        public bool UseCookie { get; set; } = false;
    }
}