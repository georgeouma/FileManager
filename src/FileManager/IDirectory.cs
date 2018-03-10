using System.Threading;
using System.Threading.Tasks;

namespace FileManager
{
    public interface IDirectory
    {
        string Name { get; }
        FileId Write(FileUploadData uploadData);
        Task<FileId> WriteAsync(FileUploadData uploadData, CancellationToken cancellationToken);
        Task<IStoredFileHandle> GetAsync(FileId fileId);
    }
}
