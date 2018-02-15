using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
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

        public object StreamToObject(Stream stream)
        {
            var formatter = new BinaryFormatter();
            stream.Seek(0, SeekOrigin.Begin);
            return formatter.Deserialize(stream);
        }
    }
}
