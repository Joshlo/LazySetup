using System;
using System.Collections.Generic;
using System.IO;

namespace LazySetup.Batch
{
    [Serializable]
    public class ResponseModel
    {
        public int StatusCode { get; internal set; }
        public Dictionary<string, string> Headers { get; internal set; }
        public object Body { get; internal set; }
    }
}
