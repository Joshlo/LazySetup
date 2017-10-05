using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;

namespace LazySetup.HelpersAndExtensions
{
    public static class StringExtensions
    {
        public static T ToObject<T>(this string s)
        {
            return JsonConvert.DeserializeObject<T>(s);
        }
    }
}
