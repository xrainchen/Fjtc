using System;
using System.Web.Mvc;
using System.Web.Security;
using admin.fjtc.com.Auth;
using Fjtc.Common;
using admin.fjtc.com.Models;
using Fjtc.Model;
using Fjtc.Model.Entity;

namespace admin.fjtc.com.Controllers
{
    /// <summary>
    /// 帐号控制器
    /// </summary>
    public class AccountController : BaseController
    {
        // GET: Account

        /// <summary>
        /// 登录页面
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [AllowAnonymous]
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        [AllowAnonymous]
        public ActionResult Login()
        {
            return View("Index");
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult Login(LoginModel loginModel)
        {
            ModelState.Clear();
            if (ModelState.IsValid)
            {
                if (loginModel == null)
                {
                    ModelState.AddModelError("UserName", "非法访问!!!");
                    return View("Index");
                }
                if (loginModel.UserName == null || loginModel.UserName.Trim() == "")
                {
                    ModelState.AddModelError("UserName", "用户名不能为空!");
                }
                if (loginModel.Password == null || loginModel.Password.Trim() == "")
                {
                    ModelState.AddModelError("Password", "密码不能为空!");
                }
                if (loginModel.VerifyCode != $"{Session["CheckCode"]}")
                {
                    ModelState.AddModelError("VerifyCode", "验证码无效，请重新输入！");
                }
                if (ModelState.Count == 0)
                {
                    var userBll = new Fjtc.BLL.CMSUserBll();
                    var user = userBll.GetModel(loginModel.UserName);
                    if (user != null && user.EncryPassword(loginModel.Password) == user.Password)
                    {
                        if (user.Status == UserStatusEnum.Cancel)
                        {
                            ModelState.AddModelError("UserName", "对不起，您的帐号已冻结");
                        }
                        else
                        {
                            TicketStorageFactory.InstanceTicketStorage<CMSUser>().SetTicket(user);
                            return RedirectToAction("Index", "Home");
                        }
                    }
                    else
                    {
                        ModelState.AddModelError("UserName", "提供的用户名或密码不正确!");
                    }
                }
            }
            return View("Index");
        }

        /// <summary>
        /// 登出
        /// </summary>
        /// <returns></returns>
        [AllowAnonymous]
        public ActionResult LogOut()
        {
            Session.Abandon();
            TicketStorageFactory.InstanceTicketStorage<CMSUser>().Cancellation();
            return RedirectToAction("Index");
        }
        /// <summary>
        /// 验证码
        /// </summary>
        /// <returns></returns>
        [AllowAnonymous]
        public ActionResult VerifyCode()
        {
            string code = Operator.GetValidateCode(4);                //取随机码      // 输出图片
            Session["CheckCode"] = code.ToUpper();          // 使用Session取验证码的值
            Operator.CreateImage(code);
            return Content("");
        }
    }
}