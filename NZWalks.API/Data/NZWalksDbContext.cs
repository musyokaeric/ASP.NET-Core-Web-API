using Microsoft.EntityFrameworkCore;
using NZWalks.API.Models.Domain;

namespace NZWalks.API.Data
{
    public class NZWalksDbContext : DbContext
    {
        public NZWalksDbContext(DbContextOptions<NZWalksDbContext> dbContextOptions) : base(dbContextOptions)
        {
            
        }

        public DbSet<Difficulty> Difficulties { get; set; }
        public DbSet<Region> Regions { get; set; }
        public DbSet<Walk> Walks { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Seed data for Difficulties (Easy, Medium, Hard) to the database
            var difficulties = new List<Difficulty>
            {
                new Difficulty
                {
                    Id = Guid.Parse("1e4a18b3-a6c5-44fd-a441-114c5b022007"),
                    Name = "Easy"
                },
                new Difficulty
                {
                    Id = Guid.Parse("8cdc4fcb-78ae-4942-8429-ead23d6b2209"),
                    Name = "Medium"
                },
                new Difficulty
                {
                    Id = Guid.Parse("29d31fc7-1b3a-414a-9a4a-06d5c86f9b5b"),
                    Name = "Hard"
                }
            };
            modelBuilder.Entity<Difficulty>().HasData(difficulties);

            // Seed data for Regions to the database
            var regions = new List<Region>
            {
                new Region
                {
                    Id = Guid.Parse("e1059975-93dc-4fb3-9eb3-7ed147cb2e3d"),
                    Name = "Auckland",
                    Code = "AKL",
                    RegionImageUrl = "auckland-img.png"
                },
                new Region
                {
                    Id = Guid.Parse("a0ddd808-2e24-4ab6-a502-f2b737f67135"),
                    Name = "Northland",
                    Code = "NTL",
                    RegionImageUrl = "northland-img.png"
                },
                new Region
                {
                    Id = Guid.Parse("acdb83cb-3576-40b2-b0a6-594bb5a25302"),
                    Name = "Bay of Plenty",
                    Code = "BOP",
                    RegionImageUrl = "bayOfPlenty-img.png"
                }
                ,new Region
                {
                    Id = Guid.Parse("68996614-3843-4f4a-867e-d011f1fa2fb4"),
                    Name = "Wellington",
                    Code = "WGN",
                    RegionImageUrl = "wellington-img.png"
                },
                new Region
                {
                    Id = Guid.Parse("60257737-16a2-4354-a33e-d8ff95009cf3"),
                    Name = "Nelson",
                    Code = "NSN",
                    RegionImageUrl = "nelson-img.png"
                },
                new Region
                {
                    Id = Guid.Parse("e665ca3b-bed8-403b-905c-f20a241e38a8"),
                    Name = "Southland",
                    Code = "STL",
                    RegionImageUrl = "southland-img.png"
                }
            };
            modelBuilder.Entity<Region>().HasData(regions);
        }
    }
}
