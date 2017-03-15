using System.Web.Mvc;
using fjtc.com.Common;
using Fjtc.BLL;
using Fjtc.Common;
using Fjtc.Model.Entity;
using Fjtc.Model.ViewModel;

namespace fjtc.com.Controllers
{
    public class UserController : Controller
    {
        // GET: User
        [HttpGet]
        public ActionResult Register()
        {
            return View(new UserViewModel());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Register(UserViewModel model, FormCollection collection)
        {
            var message = string.Empty;
            var userBll = new UserBLL();
            var user = new User();
            var result = userBll.AddUser(user);
            if (result)
                return Json(JsonResultHelper.Success("注册成功"));
            return Json(JsonResultHelper.Warn(message));
        }
        /// <summary>
        /// 验证码
        /// </summary>
        /// <returns></returns>
        [AllowAnonymous]
        public ActionResult VerifyCode()
        {
            var code = Operator.GetValidateCode(4);                //取随机码      // 输出图片
            Session["CheckCode"] = code.ToUpper();          // 使用Session取验证码的值
            Operator.CreateImage(code);
            return new EmptyResult();
        }
    }
}