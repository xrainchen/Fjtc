using System.Web.Mvc;
using fjtc.com.Auth;
using fjtc.com.Models;
using Fjtc.Common;
using Fjtc.Model.Entity;

namespace fjtc.com.Areas.Admin.Controllers
{
    public class AccountController : BaseAdminControl
    {
        // GET: Admin/Account

        [HttpGet]
        [AllowAnonymous]
        public ActionResult Index(LoginModel model)
        {
            return View(model);
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult Index(LoginModel model, FormCollection collection)
        {
            ModelState.Clear();
            if (ModelState.IsValid)
            {
                if (model == null)
                {
                    ModelState.AddModelError("UserName", "非法访问!!!");
                    return View("Index");
                }
                if (model.UserName == null || model.UserName.Trim() == "")
                {
                    ModelState.AddModelError("UserName", "用户名不能为空!");
                }
                if (model.Password == null || model.Password.Trim() == "")
                {
                    ModelState.AddModelError("Password", "密码不能为空!");
                }
                if (model.VerifyCode != $"{Session["CheckCode"]}")
                {
                    ModelState.AddModelError("VerifyCode", "验证码无效，请重新输入！");
                }
                if (ModelState.Count == 0)
                {
                    var userBll = new Fjtc.BLL.ProductUserBLL();
                    var user = userBll.GetModel(model.UserName);
                    if (user != null && user.EncryPassword(model.Password) == user.Password)
                    {
                        TicketStorageFactory.InstanceTicketStorage<ProductUser>().SetTicket(user);
                        return RedirectToAction("Index", "Home", new { area = "Admin" });
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
            TicketStorageFactory.InstanceTicketStorage<ProductUser>().Cancellation();
            return RedirectToAction("Index");
        }

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