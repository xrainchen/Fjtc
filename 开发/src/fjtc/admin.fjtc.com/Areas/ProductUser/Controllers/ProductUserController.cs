using System.Web.Mvc;
using admin.fjtc.com.Common;
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
        [HttpPost]
        public ActionResult ResetPassword(long productUserId)
        {
            if (!CurrentUser.IsAdministrator())
            {
                return DwzHelper.Warn("对不起,您无权操作");
            }
            var userBll = new ProductUserBLL();
            var user = userBll.Get(productUserId);
            if (user != null)
            {
                userBll.UpdatePassword(user.EncryPassword(user.LoginName), user.Id);
            }
            return DwzHelper.Success("重置成功");
        }
    }
}