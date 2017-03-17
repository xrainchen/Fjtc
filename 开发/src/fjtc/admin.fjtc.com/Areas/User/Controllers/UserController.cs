using System.Web.Mvc;
using admin.fjtc.com.Controllers;
using Fjtc.BLL;
using Fjtc.Model.SearchModel;

namespace admin.fjtc.com.Areas.User.Controllers
{
    public class UserController : BaseController
    {
        // GET: User/User

        [HttpGet]
        public ActionResult List(UserSearchParameter searchObj)
        {
            return View(searchObj);
        }

        [HttpPost]
        public ActionResult List(UserSearchParameter searchParameter, FormCollection collection)
        {
            BindParameter(searchParameter);
            searchParameter.ReturnList = new UserBLL().GetList(searchParameter);
            return Json(searchParameter);
        }
    }
}