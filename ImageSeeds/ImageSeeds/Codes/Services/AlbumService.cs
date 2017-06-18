using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ImageSeeds.Codes.Entities;
using ImageSeeds.Codes.Repositories;

namespace ImageSeeds.Codes.Services
{
    public class AlbumService
    {
        public IAlbumRepository AlbumRepo { get; private set; }

        public AlbumService(IAlbumRepository albumRepo)
        {
            AlbumRepo = albumRepo;
        }

        public Album CreateAlbum(string name, IList<Image> images, IList<Tag> tags)
        {
            Album newAlbum = AlbumRepo.CreateAlbum(name, new HashSet<Image>(images), new HashSet<Tag>(tags));
            AlbumRepo.SaveAlbum(newAlbum);
            return newAlbum;
        }
    }
}