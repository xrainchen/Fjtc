using System.Web.Mvc;
using admin.fjtc.com.Controllers;
using Fjtc.BLL;
using Fjtc.Model.SearchModel;

namespace admin.fjtc.com.Areas.User.Controllers
{
    public class ProductUserController : BaseController
    {
        // GET: ProductUser/ProductUser

        [HttpGet]
        public ActionResult List(ProductUserSearchParameter searchObj)
        {
            return View(searchObj);
        }

        [HttpPost]
        public ActionResult List(ProductUserSearchParameter searchParameter, FormCollection collection)
        {
            BindParameter(searchParameter);
            searchParameter.ReturnList = new ProductUserBLL().GetList(searchParameter);
            return Json(searchParameter);
        }
        /// <summary>
        /// 重置密码
        /// </summary>
        /// <returns></returns>
        public ActionResult ResetPassword()
        {
            return null;
        }
    }
}