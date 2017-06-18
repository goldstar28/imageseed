using System.Data.Entity;
using ImageSeeds.Codes.Entities;

namespace ImageSeeds.Codes.Repositories
{
    public class ImageDbContext : DbContext
    {
        static ImageDbContext()
        {
            Database.SetInitializer<ImageDbContext>(null);
        }

        public ImageDbContext() : this("name=Default")
        {
        }

        public ImageDbContext(string connectionString)
            : base(connectionString)
        {
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Album> Albums { get; set; }
        public DbSet<Image> Image { get; set; }
        public DbSet<Image> ImageType { get; set; }
        public DbSet<Image> Tag { get; set; }
    }
}