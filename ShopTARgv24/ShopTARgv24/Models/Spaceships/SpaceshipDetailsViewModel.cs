using System.Collections.Generic;

namespace ShopTARgv24.Models.Spaceships
{
    public class SpaceshipDetailsViewModel
    {
        public Guid? Id { get; set; }          // ← чтобы не ругался на Guid? → Guid
        public string? Name { get; set; }
        public string? TypeName { get; set; }  // во вью мы используем это имя
        public DateTime? BuiltDate { get; set; }

        public int? Crew { get; set; }
        public int? Passengers { get; set; }
        public decimal? InnerVolume { get; set; }

        public int? EnginePower { get; set; }  // ← ДОБАВИЛИ

        public DateTime? CreatedAt { get; set; }
        public DateTime? ModifiedAt { get; set; }

        public List<string> Images { get; set; } = new();
    }
}
