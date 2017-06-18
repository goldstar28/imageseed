using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ImageSeeds.Codes.Entities
{
    public class ImageType
    {
        public static readonly ImageType Jpeg = new ImageType("image/jpeg");
        public static readonly ImageType Png = new ImageType("image/png");
        public static readonly ImageType Gif = new ImageType("image/gif");
        public static readonly ImageType Unknown = new ImageType("");

        private readonly string _mime;
        private ImageType(string mime)
        {
            _mime = mime;
        }

        public override string ToString()
        {
            return _mime;
        }

        public static ImageType ParseImageTypeFromMime(string mimeType)
        {
            switch (mimeType.ToLower())
            {
                case "image/jpeg":
                    return Jpeg;
                case "image/gif":
                    return Gif;
                case "image/png":
                    return Png;
                default:
                    return Unknown;
            }
        }
    }
}
