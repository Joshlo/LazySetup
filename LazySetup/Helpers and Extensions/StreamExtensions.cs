using System.IO;

namespace LazySetup.Helpers_and_Extensions
{
    public static class StreamExtensions
    {
        public static void ResetPosition(this Stream stream, int position = 0)
        {
            stream.Position = position;
        }
    }
}
