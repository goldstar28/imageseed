using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ImageSeeds.Codes.Entities;

namespace ImageSeeds.Codes.Repositories
{
    public interface IImageRepository
    {
        Image GetImage(Guid id);
        bool ImageExists(Guid id);
        IQueryable<Image> GetImages();
        Image SaveImage(Image image, bool overwrite);
        bool SaveImage(Guid newId, byte[] data, bool v);
    }
}