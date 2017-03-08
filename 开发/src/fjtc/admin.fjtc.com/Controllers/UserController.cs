using System.Web.Mvc;
using Fjtc.BLL;
using Fjtc.Model.Entity;
using Fjtc.Model.ViewModel;

namespace admin.fjtc.com.Controllers
{
    public class UserController : BaseController
    {
        /// <summary>
        /// 用户列表页
        /// </summary>
        /// <returns></returns>
        public ActionResult List()
        {
            return View();
        }

        public ActionResult Add(UserViewModel user)
        {
            if (Request.IsAjaxRequest())
            {
                var msg = string.Empty;
                var result = new UserBll().AddUser(new User()
                {
                    Name = user.Name,
                    CreatedBy = "当前登录用户",
                    Password = user.Password,
                    Status = user.Status,
                    Number = user.Number,
                    LoginName = user.LoginName
                }, ref msg);
                return Json(new { IsOk = result, Msg = msg });
            }
            return View(user);
        }
    }
}