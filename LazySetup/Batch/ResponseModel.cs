using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Http;

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
