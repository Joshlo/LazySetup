using System;
using Microsoft.AspNetCore.Http;

namespace LazySetup.Tracking
{
    public class TrackingModel
    {
        public HttpResponse Response { get; internal set; }
        public HttpRequest Request { get; internal set; }
        public TimeSpan ExecutionTime { get; internal set; }
        
    }
}