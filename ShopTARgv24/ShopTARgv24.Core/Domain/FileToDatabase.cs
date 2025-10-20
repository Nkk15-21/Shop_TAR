using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopTARgv24.Core.Domain
{
    public class FileToDatabase
    {
        public int Id { get; set; }

        public string? Name { get; set; }
        public string? ContentType { get; set; }

        public byte[] ImageData { get; set; } = Array.Empty<byte>();

        public DateTime UploadedAtUtc { get; set; } = DateTime.UtcNow;
    }
}
