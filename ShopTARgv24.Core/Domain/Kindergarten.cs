using System.Collections.Generic;
using ShopTARgv24.Core.Domain;

public IEnumerable<FileToDatabase> FileToDatabase { get; set; } = new List<FileToDatabase>(); namespace ShopTARgv24.Core.Domain {
    public class Kindergarten
    {
        public Guid? Id { get; set; } // Обычно используем Guid
        public string GroupName { get; set; }
        public int ChildrenCount { get; set; }
        public string KindergartenName { get; set; }
        public string TeacherName { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
    }
}
