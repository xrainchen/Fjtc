using System.Web.Mvc;

namespace admin.fjtc.com.Controllers
{
    public class BuildController : Controller
    {
        // GET: Build
        /// <summary>
        /// 网站正在建设中
        /// </summary>
        /// <returns></returns>
        public ActionResult List()
        {
            return View();
        }
    }
}