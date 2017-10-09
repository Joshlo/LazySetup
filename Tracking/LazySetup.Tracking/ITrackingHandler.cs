using System.Threading.Tasks;

namespace LazySetup.Tracking
{
    public interface ITrackingHandler
    {
        Task SaveTracking(TrackingModel model);
    }
}