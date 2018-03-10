using System;

namespace FileManager
{
    public interface IFileDescriptor
    {
        string OriginalName { get; }
        string ContentType { get; }
        DateTime DateCreatedUtc { get; }
        string CreatedBy { get; }
        string FileId { get; }
    }
}
