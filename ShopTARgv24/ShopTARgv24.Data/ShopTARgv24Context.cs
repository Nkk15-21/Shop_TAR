using Microsoft.EntityFrameworkCore;
using ShopTARgv24.Core.Domain;



namespace ShopTARgv24.Data
{
    public partial class ShopTARgv24Context : DbContext
    {
        public ShopTARgv24Context(DbContextOptions<ShopTARgv24Context> options)
            : base(options) { }


        public DbSet<FileToApi> FileToApis { get; set; }
        public DbSet<FileToDatabase> FileToDatabases { get; set; }
        public DbSet<RealEstate> RealEstates { get; set; }
        public DbSet<Spaceship> Spaceships { get; set; } = default!;



        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<FileToDatabase>(e =>
            {
                e.HasKey(x => x.Id);
                e.Property(x => x.Name).HasMaxLength(255);
                e.Property(x => x.ContentType).HasMaxLength(127);
            });

            modelBuilder.Entity<RealEstate>(e =>
            {
                e.HasKey(x => x.Id);
            });
        }
    }
}
