using System;

namespace LazySetup.Tracking
{
    public class TrackingOptions
    {
        public TrackingTypes TrackingType { get; set; }
        public Action<TrackingModel> Execute { get; set; }
    }
}