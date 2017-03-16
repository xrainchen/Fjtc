using System;
using System.Web.Mvc;
using fjtc.com.Common;
using fjtc.com.Models;
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
            var checkCode = collection["CheckCode"].ToUpper();
            if (Session["CheckCode"] == null || !checkCode.Equals(Session["CheckCode"].ToString(), StringComparison.CurrentCultureIgnoreCase))
            {
                return Json(JsonResultHelper.Warn("对不起,您填写的验证码不正确！"));
            }
            Session["CheckCode"] = null;
            var userBll = new UserBLL();
            if (userBll.IsExistLoginName(model.LoginName))
                return Json(JsonResultHelper.Warn("对不起,该用户名已被注册！"));
            if (userBll.IsExistMobilePhone(model.MobilePhone))
                return Json(JsonResultHelper.Warn("对不起,该手机号已被注册！"));
            var user = new User()
            {
                LoginName = model.LoginName,
                MobilePhone = model.MobilePhone,
                Name = model.Name,
                BindHost = $"{model.LoginName}.weixin.rponey.cc",
                HeadPhoto = "http://b.hiphotos.baidu.com/zhidao/wh%3D450%2C600/sign=f0c5c08030d3d539c16807c70fb7c566/8ad4b31c8701a18bbef9f231982f07082838feba.jpg"
            };
            user.Password = user.EncryPassword(model.Password);
            var result = userBll.AddUser(user);
            if (result)
                return RedirectToAction("Index", "Account", new { area = "Admin" });
            return Json(JsonResultHelper.Warn("注册失败"));
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