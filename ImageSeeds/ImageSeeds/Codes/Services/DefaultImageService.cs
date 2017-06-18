using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using ImageSeeds.Codes.Entities;
using ImageSeeds.Codes.Repositories;
using System.Drawing;

namespace ImageSeeds.Codes.Services
{
    public class DefaultImageService : IImageService
    {
        private readonly IImageRepository _imageRepo;
        private readonly ITagRepository _tagRepo;

        public IImageRepository ImageRepo => _imageRepo;

        public ITagRepository TagRepo => _tagRepo;

        public DefaultImageService(IImageRepository imageRepo, ITagRepository tagRepo)
        {
            _imageRepo = imageRepo;
            _tagRepo = tagRepo;
        }

        public Image SaveNewImage(string tags, string name, string description, string originalFileName,
            string mimeType, User creator, byte[] data)
        {
            if (!Image.IsImageType(mimeType))
            {
                throw new ArgumentException("Supplied mimeType is not supported");
            }

            var newId = Guid.NewGuid();
            if (!_imageRepo.SaveImage(newId, data,false))
            {
                throw new Exception("Unknown error occurred while saving new image");
            }

            var tagList = tags == null ? _tagRepo.CreateOrReturnTags(new string[0]) : _tagRepo.CreateOrReturnTags(tags.Split(','));

            var newImage = new Image(mimeType)
            {
                Id = newId,
                FileName = name,
                Description = description,
                CreateDate = DateTime.Now,
                Creator = creator,
                Tags = tagList,
                OriginalFileName = originalFileName,
                LastModified = DateTime.Now
            };

            // Change the count in the related tag
            foreach (var t in tagList)
            {
                t.ItemCount++;
                _tagRepo.SaveTag(t);
            }

            return _imageRepo.SaveImage(newImage, false);
        }

        public Image GetImage(Guid id)
        {
            return _imageRepo.GetImage(id);
        }


        public bool ImageExists(Guid id)
        {
            return _imageRepo.ImageExists(id);
        }

        /// <summary>
        ///     Method to resize, convert and save the image.
        /// </summary>
        /// <param name="fileName">File Name</param>
        /// <param name="maxWidth">resize width.</param>
        /// <param name="maxHeight">resize height.</param>
        /// <param name="pathToSave">pathToSave.</param>
        public void SaveImageFile(string FileName, int maxWidth, int maxHeight, string filePath)
        {
            Bitmap image;
            using (Stream bmpStream = File.Open(FileName, FileMode.Open))
            {
                var imageOrgi = System.Drawing.Image.FromStream(bmpStream);

                image = new Bitmap(imageOrgi);
            }

            // Get the image's original width and height
            var originalWidth = image.Width;
            var originalHeight = image.Height;

            // To preserve the aspect ratio
            var ratioX = maxWidth / (float)originalWidth;
            var ratioY = maxHeight / (float)originalHeight;
            var ratio = Math.Min(ratioX, ratioY);

            if (maxWidth / (float)originalWidth < 1 || maxHeight / (float)originalHeight < 1 )
                {
                // New width and height based on aspect ratio
                var newWidth = (int)(originalWidth * ratio);
                var newHeight = (int)(originalHeight * ratio);
            }
            else
            {
                var newWidth = originalWidth;
                var newHeight = originalHeight;
            }

            return SaveImageFile(maxWidth, maxHeight, filePath);
        }

        private static void SaveNewImage(string filePath)
        {
            throw new NotImplementedException();
        }

        public Image UpdateImage(Guid id, string tags, string name, string description, User modifiedBy)
        {
            if (!ImageExists(id))
            {
                throw new ArgumentException("Supplied Image is not existed in db");
            }
            var image = GetImage(id);
            foreach (var t in image.Tags)
            {
                t.ItemCount--;
                _tagRepo.SaveTag(t);
            }

            image.FileName = name;
            image.Description = description;
            image.ModifiedBy = modifiedBy;

            var tagList = tags == null ?
                _tagRepo.CreateOrReturnTags(new string[0]) :
                _tagRepo.CreateOrReturnTags(tags.Split(','));

            foreach (var t in tagList)
            {
                t.ItemCount++;
                _tagRepo.SaveTag(t);
            }

            image.Tags = tagList;
            image.LastModified = DateTime.Now;
            return _imageRepo.SaveImage(image, true);
        }

    }
}