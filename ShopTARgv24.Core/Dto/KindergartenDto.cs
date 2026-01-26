using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;

namespace ShopTARgv24.Core.Dto
{
    public class KindergartenDto
    {
        public Guid? Id { get; set; }
        public string GroupName { get; set; }
        public int ChildrenCount { get; set; }
        public string KindergartenName { get; set; }
        public string TeacherName { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }

        // Список файлов для загрузки
        public List<IFormFile> Files { get; set; } = new List<IFormFile>();
        // Список уже существующих файлов (для отображения)
        public IEnumerable<FileToDatabaseDto> Image { get; set; } = new List<FileToDatabaseDto>();
    }
}