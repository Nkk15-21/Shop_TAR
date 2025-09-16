namespace ShopTARgv24.Models.Spaceships
{
    public class SpaceshipDetailsViewModel
    {
        public Guid? Id { get; set; }
        public string Name { get; set; }
        public string TypeName { get; set; }
        public DateTime? BuiltDate { get; set; }
        public int? Crew { get; set; }
        public double? EnginePower { get; set; }
        public int? Passengers { get; set; }
        public double? InnerVolume { get; set; }
        public DateTime? CreatedAt { get; set; }
        public DateTime? ModifiedAt { get; set; }

    }
}
