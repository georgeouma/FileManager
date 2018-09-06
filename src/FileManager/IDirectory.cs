using System.Threading;
using System.Threading.Tasks;

namespace FileManager
{
    public interface IDirectory
    {
        string Name { get; }
        /// <summary>
        /// Add file to a directory
        /// </summary>
        /// <param name="uploadData"></param>
        /// <returns>FileId</returns>
        FileId Write(FileUploadData uploadData);
        /// <summary>
        /// Add file to directory asynchronously
        /// </summary>
        /// <param name="uploadData"></param>
        /// <param name="cancellationToken"></param>
        /// <returns>Task<FileId></returns>
        Task<FileId> WriteAsync(FileUploadData uploadData, CancellationToken cancellationToken);
        /// <summary>
        /// Get stored file handle by file id
        /// </summary>
        /// <param name="fileId"></param>
        /// <returns>IStoredFileHandle</returns>
        Task<IStoredFileHandle> GetAsync(FileId fileId);
    }
}
