using Microsoft.WindowsAzure.Storage.Blob;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileManager.Service
{
    public class BlobFileHandle : IStoredFileHandle, IFileDescriptor
    {
        CloudBlockBlob blob;

        public BlobFileHandle(CloudBlockBlob blob)
        {
            if (blob == null)
                throw new ArgumentNullException("blob");
            this.blob = blob;
        }
        public IFileDescriptor Descriptor
        {
            get { return this; }
        }

        public async Task<Stream> OpenAsync()
        {
            //back the stream with local file
            //could use MemoryStream but I think this will be more
            //efficient esp for large files
            var path = Path.GetTempFileName();
            using (var writer = new FileStream(path, FileMode.Create))
            {
                await CopyAsyncImpl(writer);
            }
            return new FileStream(path, FileMode.Open, FileAccess.Read, FileShare.Read);
        }

        public Task CopyAsync(System.IO.Stream destination)
        {
            if (destination == null)
                throw new ArgumentNullException("destination");
            return CopyAsyncImpl(destination);
        }

        public string OriginalName
        {
            get
            {
                return blob.Metadata[Metadata.OriginalName];
            }
        }
        public string FileId
        {
            get
            {
                return blob.Metadata[Metadata.FileId];
            }
        }
        public string ContentType
        {
            get
            {
                return blob.Metadata[Metadata.ContentType];
            }
        }

        public DateTime DateCreatedUtc
        {
            get { return DateTime.Parse(blob.Metadata[Metadata.DateCreatedUtc], CultureInfo.InvariantCulture); }
        }

        public string CreatedBy
        {
            get { return blob.Metadata[Metadata.CreatedBy]; }
        }

        async Task CopyAsyncImpl(Stream destination)
        {
            await blob.DownloadToStreamAsync(destination);
        }
    }
}
