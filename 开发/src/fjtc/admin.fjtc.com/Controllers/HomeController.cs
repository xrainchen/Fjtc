using System.Web.Mvc;
using Fjtc.BLL;

namespace admin.fjtc.com.Controllers
{
    public class HomeController : BaseController
    {
        /// <summary>
        /// 首页
        /// </summary>
        /// <returns></returns>
        public ActionResult Index()
        {
            ViewBag.UserMenu = new CMSMenuBll().GetTreeList();
            return View(CurrentUser);
        }
    }
}