using System.IO;
using System.Threading.Tasks;

namespace FileManager
{
    public interface IStoredFileHandle
    {
        //properties of the stream
        IFileDescriptor Descriptor { get; }
        //gets a stream that can read contents from
        //the target file
        Task<Stream> OpenAsync();
        //copy the contents of the file into the stream
        Task CopyAsync(Stream destination);
    }
}
