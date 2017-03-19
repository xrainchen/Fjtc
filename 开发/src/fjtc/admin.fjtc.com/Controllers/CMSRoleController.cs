using System.Web.Mvc;
using Fjtc.Model.SearchModel;

namespace admin.fjtc.com.Controllers
{
    public class CMSRoleController : BaseController
    {
        // GET: CMSRole

        [HttpGet]
        public ActionResult List(CMSRoleSearchParameter searchParameter)
        {
            return View(searchParameter);
        }

        [HttpPost]
        public ActionResult List(CMSRoleSearchParameter searchParameter, FormCollection collection)
        {
            return Json(searchParameter);
        }
    }
}