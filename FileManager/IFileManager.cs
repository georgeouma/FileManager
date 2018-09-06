using System.Threading;
using System.Threading.Tasks;

namespace FileManager
{
    public interface IFileManager
    {
        /// <summary>
        /// File manager default root directory
        /// </summary>
        IDirectory Root { get; }
        /// <summary>
        /// Get file directory by directory name
        /// </summary>
        /// <param name="directoryName"></param>
        /// <param name="cancellationToken"></param>
        /// <returns>Directory</returns>
        Task<IDirectory> GetDirectoryAsync(string directoryName, CancellationToken cancellationToken);
    }
}
