namespace ImageSeeds.Codes.Repositories
{
    public class RepositoryBase
    {
        public ImageDbContext GetDbContext()
        {
            return new ImageDbContext();
        }
    }
}