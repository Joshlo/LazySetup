using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json.Linq;

namespace LazySetup.Batch
{
    public class RequestModel
    {
        public string Method { get; set; }
        public string RelativeUrl { get; set; }
        public JObject Body { get; set; }

    }
}
