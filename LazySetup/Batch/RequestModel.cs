using System;
using System.Collections.Generic;
using System.Text;

namespace LazySetup.Batch
{
    public class RequestModel
    {
        public string Method { get; set; }
        public string RelativeUrl { get; set; }
        public string Body { get; set; }

    }
}
