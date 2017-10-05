using System.IO;
using System.Threading.Tasks;

namespace LazySetup.HelpersAndExtensions
{
    public class StreamHelper
    {
        public Task<string> StreamToJson(Stream stream)
        {
            var reader = new StreamReader(stream);
            return reader.ReadToEndAsync();
        }
    }
}
