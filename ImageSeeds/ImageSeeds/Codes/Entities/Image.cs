using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace ImageSeeds.Codes.Entities
{
    public class Image
    {
        public Image(string mimeType)
        {
            if (!IsImageType(mimeType))
                throw new ArgumentException("Supplied MimeType is not an accepted Image format.");
            MimeType = mimeType;
        }

        public string MimeType { get; private set; }
        public IList<Tag> Tags { get; set; }
        public Guid Id { get; set; }
        public string FileName { get; set; }
        public string ImagePath { get; set; }
        public string ThumbnailPath { get; set; }
        public string OriginalFileName { get; set; }
        public string Description { get; set; }
        public DateTime CreateDate { get; set; }
        public DateTime LastModified { get; set; }
        public User Creator { get; set; }
        public User ModifiedBy { get; set; }

        public static bool IsImageType(string mimeType)
        {
            var type = mimeType.ToLower();
            return ((type == "image/jpeg") || (type == "image/png") || (type == "image/gif"));
        }

        public static bool FileExtensionIsImageType(string fileExt)
        {
            var type = fileExt.ToLower();
            return ((type == ".jpg") || (type == ".png") || (type == ".gif") || (type == ".jpeg"));
        }

        public static string GetMimeTypeFromFileExtension(string fileExt)
        {
            switch (fileExt.ToLower())
            {
                case ".jpg":
                case ".jpeg":
                    return "image/jpeg";
                case ".png":
                    return "image/png";
                case ".gif":
                    return "image/gif";
            }
            throw new ArgumentException("Extension provided is not a valid image file");
        }

        public static string GetMimeTypeFromFileName(string filename)
        {
            return GetMimeTypeFromFileExtension(Path.GetExtension(filename));
        }
    }
}