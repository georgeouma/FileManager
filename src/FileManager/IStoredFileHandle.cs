using System.IO;
using System.Threading.Tasks;

namespace FileManager
{
    public interface IStoredFileHandle
    {
        /// <summary>
        /// Properties of the stream
        /// </summary>
        IFileDescriptor Descriptor { get; }
        /// <summary>
        /// Gets a stream that can read contents from the target file
        /// </summary>
        /// <returns>Task<Stream></returns>
        Task<Stream> OpenAsync();
        /// <summary>
        /// Copy the contents of the file into the stream
        /// </summary>
        /// <param name="destination"></param>
        /// <returns>Task</returns>
        Task CopyAsync(Stream destination);
    }
}
