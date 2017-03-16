using System.Web.Mvc;

namespace fjtc.com.Areas.Admin.Controllers
{
    public class HomeController : BaseAdminControl
    {
        // GET: Admin/Home
        public ActionResult Index()
        {
            return View(CurrentUser);
        }
    }
}