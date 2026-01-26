using System;

namespace ShopTARgv24.Core.Domain
{
    public class FileToDatabase
    {
        public Guid Id { get; set; }
        public string? ImageTitle { get; set; }
        public byte[]? ImageData { get; set; }

        // Связь с RealEstate
        public Guid? RealEstateId { get; set; }
        public RealEstate RealEstate { get; set; } = default!;

        // НОВОЕ: Связь с Kindergarten
        public Guid? KindergartenId { get; set; }
        public Kindergarten Kindergarten { get; set; } = default!;
    }
}