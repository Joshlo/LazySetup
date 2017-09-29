using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Http;

namespace LazySetup.Helpers_and_Extensions
{
    internal static class HttpResponseExtensions
    {
        public static bool IsSuccessStatusCode(this HttpResponse response) =>
            response.StatusCode >= 200 && response.StatusCode <= 299;
    }
}