using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ImageSeeds.Codes.Entities;

namespace ImageSeeds.Codes.Repositories
{
    public interface IAlbumRepository
    {
        bool AlbumExists(Guid id);
        Album GetAlbumById(Guid id);
        IQueryable<Album> GetAlbumsByImageId(Guid id);
        IQueryable<Album> GetAlbumsByCreator(User Creator);
        IQueryable<Album> GetAlbumsByName(string name);
        Album CreateAlbum(string name, ISet<Image> images, ISet<Tag> tags);
        Album SaveAlbum(Album album);
    }
}