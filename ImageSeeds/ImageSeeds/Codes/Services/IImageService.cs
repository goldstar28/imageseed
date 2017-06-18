using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using ImageSeeds.Codes.Entities;
using System.Drawing;
using ImageSeeds.Codes.ViewModels;

namespace ImageSeeds.Codes.Services
{
    public interface IImageService
    {

        Image SaveNewImage(string tags, string name, string description, string originalFileName, string mimeType, User creator, byte[] data);

        void SaveImageFile(string FileName, int maxWidth, int maxHeight, string filePath);

        Image GetImage(Guid id);

        bool ImageExists(Guid id);

        Image UpdateImage(Guid id, string tags, string name, string description, User modifiedBy);

    }
}