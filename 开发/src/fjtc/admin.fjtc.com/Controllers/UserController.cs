using System.Web.Mvc;
using admin.fjtc.com.Common;
using Fjtc.BLL;
using Fjtc.Model.Entity;
using Fjtc.Model.SearchModel;
using Fjtc.Model.ViewModel;

namespace admin.fjtc.com.Controllers
{
    public class UserController : BaseController
    {
        /// <summary>
        /// 用户列表页
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult List(UserSearchParameter searchParameter)
        {
            BindParameter(searchParameter);
            return View(searchParameter);
        }

        [HttpPost]
        public ActionResult List(UserSearchParameter searchParameter, FormCollection collection)
        {
            BindParameter(searchParameter);
            searchParameter.ReturnList = new UserBll().GetList(searchParameter);
            return Json(searchParameter);
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
            if (result)
            {
                return DwzHelper.SuccessAndClose(user.NavTab, msg);
            }
            return DwzHelper.Warn(msg);
        }
        [HttpGet]
        public ActionResult Edit(UserViewModel user)
        {
            var userModel = new UserBll().GetModel(user.Id);
            userModel.Password = string.Empty;
            return View(userModel);
        }
        [HttpPost]
        public ActionResult Edit(UserViewModel user, FormCollection collection)
        {
            var msg = string.Empty;
            var result = new UserBll().UpdateUser(user, ref msg);
            if (result)
            {
                return DwzHelper.SuccessAndClose(user.NavTab, msg);
            }
            return DwzHelper.Warn(msg);
        }

        /// <summary>
        /// 修改密码
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult UpdatePassword(UserViewModel model)
        {
            return View(model);
        }
        [HttpPost]
        public ActionResult UpdatePassword(FormCollection collection)
        {
            var password = collection.Get("Password");
            var msg = string.Empty;
            var result = new UserBll().UpdatePassword(
                new UserViewModel()
                {
                    Id = CurrentUser.Id,
                    Password = password
                }
                , ref msg);
            if (result)
            {
                return DwzHelper.SuccessAndClose("", "密码修改成功");
            }
            return DwzHelper.Warn("密码修改失败");
        }
    }
}