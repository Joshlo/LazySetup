using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;

namespace LazySetup.HelpersAndExtensions
{
    public static class ObjectExtensions
    {
        public static string ToJson(this object obj)
        {
            return JsonConvert.SerializeObject(obj);
        }
    }
}
