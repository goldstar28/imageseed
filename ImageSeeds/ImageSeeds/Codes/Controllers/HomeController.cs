using System.Web.Mvc;

namespace ImageSeeds.Codes.Controllers
{
    [Authorize]
    public class HomeController : BaseController
    {
        public ActionResult Index()
        {
            return View();
        }
    }
}