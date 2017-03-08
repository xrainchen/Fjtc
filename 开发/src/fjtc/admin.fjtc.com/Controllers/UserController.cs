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

        [HttpGet]
        public ActionResult Add(UserViewModel user)
        {
            return View(user);
        }

        [HttpPost]
        public ActionResult Add(UserViewModel user, FormCollection collection)
        {
            var msg = string.Empty;
            var result = new UserBll().AddUser(new User()
            {
                Name = user.Name,
                CreatedBy = User.Identity.Name,
                Password = user.Password,
                Status = user.Status,
                Number = user.Number,
                LoginName = user.LoginName
            }, ref msg);
            return Json(new { IsOk = result, Msg = msg });
        }
    }
}