using System.Web.Mvc;

namespace fjtc.com.Areas.Admin.Controllers
{
    public class MpUserController : BaseAdminControl
    {
        // GET: Admin/MpUser
        public ActionResult List()
        {
            return View();
        }
    }
}