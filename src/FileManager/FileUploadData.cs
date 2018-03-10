using System;
using System.IO;

namespace FileManager
{
    public class FileUploadData
    {
        public string OriginalName { get; set; }
        public string ContentType { get; set; }
        public string UploadedBy { get; set; }
        public Stream FileStream { get; set; }

        public FileUploadData(string originalName, string contentType, string uploadedBy,
            Stream fileStream)
        {
            if (fileStream == null)
                throw new ArgumentNullException("fileStream");
            if (string.IsNullOrEmpty(contentType))
                throw new ArgumentNullException("contentType");
            this.OriginalName = originalName;
            ContentType = contentType;
            UploadedBy = uploadedBy;
            FileStream = fileStream;
        }
        public FileUploadData(string originalName, string contentType, string uploadedBy,
            string pathSource)
        {
            if (pathSource == null)
                throw new ArgumentNullException("pathSource");
            if (string.IsNullOrEmpty(contentType))
                throw new ArgumentNullException("contentType");
            FileStream fileStream = new FileStream(pathSource, FileMode.Open, FileAccess.Read);
            this.OriginalName = originalName;
            ContentType = contentType;
            UploadedBy = uploadedBy;
            FileStream = fileStream;
        }
    }
}
