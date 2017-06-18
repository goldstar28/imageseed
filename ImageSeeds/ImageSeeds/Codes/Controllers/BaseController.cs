using System;
using System.Configuration;
using System.Web.Mvc;

namespace ImageSeeds.Codes.Controllers
{
    public class BaseController : Controller
    {
        public string OriginalImageDirectory => ConfigurationManager.AppSettings["OriginalImageDirectory"];
        public string ThumbnailImageDirectory => ConfigurationManager.AppSettings["ThumbnailImageDirectory"];
        public string DefaultThumbnail => ConfigurationManager.AppSettings["DefaultThumbnail"];
        public long UserId => Convert.ToInt64(User.Identity.Name.Split('|')[0]);
    }
}