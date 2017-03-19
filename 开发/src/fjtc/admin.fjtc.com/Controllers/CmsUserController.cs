using System.Web.Mvc;
using admin.fjtc.com.Common;
using Fjtc.BLL;
using Fjtc.Model.Entity;
using Fjtc.Model.SearchModel;
using Fjtc.Model.ViewModel;

namespace admin.fjtc.com.Controllers
{
    public class CMSUserController : BaseController
    {
        /// <summary>
        /// 用户列表页
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult List(CMSUserSearchParameter searchParameter)
        {
            BindParameter(searchParameter);
            return View(searchParameter);
        }

        [HttpPost]
        public ActionResult List(CMSUserSearchParameter searchParameter, FormCollection collection)
        {
            BindParameter(searchParameter);
            searchParameter.ReturnList = new CMSUserBll().GetList(searchParameter);
            return Json(searchParameter);
        }

        [HttpGet]
        public ActionResult Add(CMSUserViewModel user)
        {
            return View(user);
        }

        [HttpPost]
        public ActionResult Add(CMSUserViewModel user, FormCollection collection)
        {
            var result = new CMSUserBll().AddUser(new CMSUser()
            {
                Name = user.Name,
                CreatedBy = User.Identity.Name,
                Password = user.Password,
                Status = user.Status,
                Number = user.Number,
                LoginName = user.LoginName
            });
            if (result)
            {
                return DwzHelper.SuccessAndClose(user.NavTab, "添加成功");
            }
            return DwzHelper.Warn("添加失败");
        }
        [HttpGet]
        public ActionResult Edit(CMSUserViewModel user)
        {
            var userModel = new CMSUserBll().GetModel(user.Id);
            userModel.Password = string.Empty;
            return View(userModel);
        }
        [HttpPost]
        public ActionResult Edit(CMSUserViewModel user, FormCollection collection)
        {
            var result = new CMSUserBll().UpdateUser(user);
            if (result)
            {
                return DwzHelper.SuccessAndClose(user.NavTab, "密码修改成功");
            }
            return DwzHelper.Warn("密码修改失败");
        }

        /// <summary>
        /// 修改密码
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult UpdatePassword(CMSUserViewModel model)
        {
            return View(model);
        }
        [HttpPost]
        public ActionResult UpdatePassword(FormCollection collection)
        {
            var password = collection.Get("Password");
            var result = new CMSUserBll().UpdatePassword(
                new CMSUserViewModel()
                {
                    Id = CurrentUser.Id,
                    Password = password
                });
            if (result)
            {
                return DwzHelper.SuccessAndClose("", "密码修改成功");
            }
            return DwzHelper.Warn("密码修改失败");
        }
    }
}