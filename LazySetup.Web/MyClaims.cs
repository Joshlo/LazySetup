using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LazySetup.Token;

namespace LazySetup.Web
{
    public class MyClaims : TokenClaims
    {
        public string Username { get; set; }
    }
}
