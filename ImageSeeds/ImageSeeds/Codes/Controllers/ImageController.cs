using System;
using System.Web;
using System.Web.Caching;
using System.Web.Mvc;
using ImageSeeds.Codes.Services;
using ImageSeeds.Codes.ViewModels;
using System.Drawing;
using System.Linq;
using ImageSeeds.Codes.Exceptions;
using System.Web.UI.WebControls;
using ImageSeeds.Codes.Entities;
using System.Collections.Generic;


namespace ImageSeeds.Codes.Controllers
{
    public class ImageController : Controller
    {
        private readonly IImageService _imageService;
        private AlbumService AlbumService { get; set; }

        public IImageService ImageService => _imageService;

        public const int MaxFileSizeAllowed = 471859200; // 450MB

        public ImageController(IImageService imageService, AlbumService albumService)
        {
            _imageService = imageService;
            AlbumService = albumService;
        }

        public Size NewImageSize(Size imageSize, Size newSize)
        {
            Size finalSize;
            double tempval;
            if (imageSize.Height > newSize.Height || imageSize.Width > newSize.Width)
            {
                if (imageSize.Height > imageSize.Width)
                    tempval = newSize.Height / (imageSize.Height * 1.0);
                else
                {
                    tempval = newSize.Width / (imageSize.Width * 1.0);
                }

                finalSize = new Size((int)(tempval * imageSize.Width), (int)(tempval * imageSize.Height));
            }
            else
                finalSize = imageSize; // image is already small size

            return finalSize;
        }
        


        [HttpGet]
        public ActionResult Upload()
        {

            var batchId = (Guid?)Session["BatchUploadId"];
            return View(new NewImageViewModel());
        }

        [HttpPost]
        public ActionResult Upload(NewImageViewModel newImage, HttpPostedFileBase imageFile)
        {
            if (ModelState.IsValid)
            {
                if (imageFile != null)
                {
                    // workaround.  IE report jpeg as pjpeg
                    var contentType = imageFile.ContentType == "image/pjpeg" ? "image/jpeg" : imageFile.ContentType;
                    if (Image.IsImageType(contentType))

                    {

                        var data = new byte[imageFile.ContentLength];
                        imageFile.InputStream.Read(data, 0, imageFile.ContentLength);
                        var OriginalFileName = imageFile.FileName;
                        var newId = Guid.NewGuid();
                        var extension = System.IO.Path.GetExtension(imageFile.FileName).ToLower();

                        var fullDirectory = Path.Combine(Server.MapPath(OriginalImageDirectory), newId.ToString());

                        var fullPath = Path.Combine(fullDirectory, imageFile.FileName);


                        if (!Directory.Exists(fullDirectory))
                            Directory.CreateDirectory(fullDirectory);

                        //SaveImageFile( 800 , 800 , fullPath);
                        SaveImageFile(600, 600, fullPath);

                        /***************** create thumbnail resize **********************/

                        var fullDirectoryThumbnail = Path.Combine(Server.MapPath(ThumbnailImageDirectory),
                            UserId.ToString(), album.Id.ToString());

                        var fullPathThumbnail = Path.Combine(fullDirectoryThumbnail, file.FileName);

                        if (!Directory.Exists(fullDirectoryThumbnail))
                            Directory.CreateDirectory(fullDirectoryThumbnail);

                        //SaveImageFile(fullPath, 400, 300, fullPathThumbnail);
                        SaveImageFile(fullPath, 200, 150, fullPathThumbnail);

                        var newImg = ImageService.SaveNewImage
                                                  (newImage.Tags, newImage.Name, String.Empty, imageFile.FileName, contentType, null, data); ;
                        return View(newImg);
                    }
                    else
                    {
                        ModelState.AddModelError("imageFile", "File supplied is not an image file");
                    }
                }
                else
                {
                    ModelState.AddModelError("imageFile", "File is required");
                }
            }
            return View(newImage);

        }




[HttpGet]
        [OutputCache(Duration = 2000, VaryByParam = "*")]
        public ActionResult View(Guid id)
        {
            Entities.Image image;
            if (id.Equals(Guid.Empty) || ((image = _imageService.GetImage(id)) == null))
                throw new ResourceNotFoundException("Image with id: " + id.ToString() + " is not exists.");

            return View(ImageViewModel.CreateFromImage(image));
        }

    }
}
