using System.Threading;
using System.Threading.Tasks;

namespace FileManager
{
    public interface FileManager
    {
        IDirectory Root { get; }
        Task<IDirectory> GetDirectoryAsync(string directoryName, CancellationToken cancellationToken);
    }
}
