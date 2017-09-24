using System;
using System.Collections.Generic;
using System.Text;

namespace LazySetup.Batch
{
    public class BatchRequestOptions
    {
        public string Path { get; set; } = "/batch";
        public Uri Host { get; set; }
    }
}
