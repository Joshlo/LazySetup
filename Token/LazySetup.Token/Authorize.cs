using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.Authorization;

namespace LazySetup.Token
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
    public class AuthorizeAttribute : Microsoft.AspNetCore.Authorization.AuthorizeAttribute
    {
        public AuthorizeAttribute()
        {
            AuthenticationSchemes = "Cookies,Bearer";
        }

        public AuthorizeAttribute(string policy) : base(policy)
        {
            AuthenticationSchemes = "Cookies,Bearer";
        }
    }
}
