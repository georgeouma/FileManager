using Microsoft.WindowsAzure.Storage.Blob;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace FileManager.Service
{
    public class PaymentFileManager : FileManager
    {
        CloudBlobContainer container;
        public PaymentFileManager(CloudBlobContainer container)
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
