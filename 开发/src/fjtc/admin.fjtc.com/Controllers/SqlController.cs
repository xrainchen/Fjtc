using System.Web.Mvc;

namespace admin.fjtc.com.Controllers
{
    public class SqlController : BaseController
    {
        // GET: Sql
        public ActionResult List()
        {
            if (!CurrentUser.IsAdministrator())
            {
                return Content("对不起,您无权访问");
            }
            return View();
        }
    }
}