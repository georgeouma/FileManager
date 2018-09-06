using System;

namespace FileManager
{
    public struct FileId : IEquatable<FileId>
    {
        public string Id { get; set; }
        public FileId(string id)
            : this()
        {
            Id = id;
        }
        public bool Equals(FileId other)
        {
            return string.Equals(Id, other.Id, StringComparison.InvariantCultureIgnoreCase);
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(obj, null))
                return false;
            if (obj.GetType() != typeof(FileId))
                return false;
            return Equals((FileId)obj);
        }

        public override int GetHashCode()
        {
            return Id.GetHashCode();
        }

        public override string ToString()
        {
            return Id;
        }

        public static FileId NewId()
        {
            return new FileId(Guid.NewGuid().ToString());
        }

        public static explicit operator FileId(Guid id)
        {
            return new FileId(id.ToString());
        }
    }
}
