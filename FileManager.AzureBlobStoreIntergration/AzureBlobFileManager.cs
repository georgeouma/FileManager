using Microsoft.WindowsAzure.Storage.Blob;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace FileManager.AzureBlobStoreIntergration
{
    public class AzureBlobFileManager
    {
        readonly CloudBlobContainer container;
        public AzureBlobFileManager(CloudBlobContainer container)
        {
            if (container == null)
                throw new ArgumentNullException("container");
            this.container = container;
            Root = new BlobVirtualDirectory(null, container);
        }
        public IDirectory Root { get; set; }
        public Task<IDirectory> GetDirectoryAsync(string directoryName, CancellationToken cancellationToken)
        {
            if (string.IsNullOrWhiteSpace(directoryName))
                throw new ArgumentNullException("directoryName");

            IDirectory directory = new BlobVirtualDirectory(directoryName, container);
            return Task.FromResult(directory);
        }
    }
}
