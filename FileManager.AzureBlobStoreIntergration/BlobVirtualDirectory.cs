using Microsoft.WindowsAzure.Storage.Blob;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace FileManager.AzureBlobStoreIntergration
{
    public class BlobVirtualDirectory : IDirectory
    {
        private CloudBlobContainer container;
        public BlobVirtualDirectory(string name, CloudBlobContainer container)
        {
            Name = string.IsNullOrEmpty(name)
                ? null
                : name.ToLowerInvariant();
            this.container = container;
        }
        public string Name { get; set; }

        public FileId Write(FileUploadData uploadData)
        {
            var id = FileId.NewId();
            var path = GetFilePath(id);
            var block = container.GetBlockBlobReference(path);
            block.Metadata[Metadata.FileId] = id.ToString();
            block.Metadata[Metadata.ContentType] = uploadData.ContentType;
            block.Metadata[Metadata.OriginalName] = uploadData.OriginalName;
            block.Metadata[Metadata.DateCreatedUtc] = DateTime.UtcNow.ToString(CultureInfo.InvariantCulture);

            block.UploadFromStreamAsync(uploadData.FileStream).ConfigureAwait(false).GetAwaiter().GetResult();
            return id;
        }

        public async Task<FileId> WriteAsync(FileUploadData uploadData, CancellationToken cancellationToken)
        {
            var id = FileId.NewId();
            var path = GetFilePath(id);
            var block = container.GetBlockBlobReference(path);
            block.Metadata[Metadata.FileId] = id.ToString();
            block.Metadata[Metadata.ContentType] = uploadData.ContentType;
            block.Metadata[Metadata.OriginalName] = uploadData.OriginalName;
            block.Metadata[Metadata.DateCreatedUtc] = DateTime.UtcNow.ToString(CultureInfo.InvariantCulture);

            await block.UploadFromStreamAsync(uploadData.FileStream);
            return id;
        }

        public async Task<IStoredFileHandle> GetAsync(FileId fileId)
        {
            var path = GetFilePath(fileId);
            var block = container.GetBlockBlobReference(path);
            await block.FetchAttributesAsync();
            var handle = new BlobFileHandle(block);
            return handle;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        string GetFilePath(FileId fileId)
        {
            return string.IsNullOrWhiteSpace(Name)
                ? fileId.ToString()
                : string.Format("{0}/{1}", Name, fileId.ToString());
        }
    }
    class Metadata
    {
        public const string
            FileId = "filemanager_fileId",
            ContentType = "filemanager_contenttype",
            OriginalName = "filemanager_originalname",
            DateCreatedUtc = "filemanager_datecreatedutc",
            CreatedBy = "filemanager_createdby";
    }
}
