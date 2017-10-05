using System;
using System.Collections.Generic;
using System.Text;

namespace LazySetup.Token
{
    public class TokenRequest
    {
        public string Identifier { get; set; }
        public string Password { get; set; }
        public string Grant_type { get; set; }
        public string Refresh_token { get; set; }
    }
}
