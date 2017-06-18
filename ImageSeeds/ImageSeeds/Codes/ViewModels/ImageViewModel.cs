using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web.Mvc;
using System.Text;
using ImageSeeds.Codes.Entities;

namespace ImageSeeds.Codes.ViewModels
{
    public class ImageViewModel
    {
        [Required]
        public string FileName { get; set; }
        public string ImagePath { get; set; }
        public string ThumbnailPath { get; set; }

        public Guid Id { get; set; }

        public string Description { get; set; }

        public DateTime CreateDate { get; set; }
        public string OriginalFileName { get; set; }
        public DateTime LastModified { get; set; }
        public virtual string[] Tags { get; set; }

        public static ImageViewModel CreateFromImage(Image image)
        {
            return image == null ?
                null :
                new ImageViewModel
                {
                    FileName = image.FileName,
                    Id = image.Id,
                    ImagePath = image.ImagePath,
                    ThumbnailPath = image.ThumbnailPath ,
                    Description = image.Description,
                    Tags = image.Tags.Select(t => t.Name).ToArray(),
                    CreateDate = image.CreateDate,
                    OriginalFileName = image.OriginalFileName,
                    LastModified = image.LastModified
                };
        }

    }
}