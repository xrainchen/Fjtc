using System;
using System.Web.Mvc;
using fjtc.com.Common;
using Fjtc.BLL;
using Fjtc.Common;
using Fjtc.Model.Entity;
using Fjtc.Model.ViewModel;

namespace fjtc.com.Controllers
{
    public class ProductUserController : Controller
    {
        // GET: ProductUser

        private readonly Lazy<ProductUserBLL> _productUserBll = new Lazy<ProductUserBLL>();

        [HttpGet]
        public ActionResult Register()
        {
            return View(new ProductUserViewModel());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Register(ProductUserViewModel model, FormCollection collection)
        {
            var checkCode = collection["CheckCode"].ToUpper();
            if (Session["RegisterCheckCode"] == null || !checkCode.Equals(Session["CheckCode"].ToString(), StringComparison.CurrentCultureIgnoreCase))
            {
                return Json(JsonResultHelper.Warn("对不起,您填写的验证码不正确！"));
            }
            var userBll = new ProductUserBLL();
            if (userBll.IsExistLoginName(model.LoginName))
                return Json(JsonResultHelper.Warn("对不起,该用户名已被注册！"));
            if (userBll.IsExistMobilePhone(model.MobilePhone))
                return Json(JsonResultHelper.Warn("对不起,该手机号已被注册！"));
            var bindHost = $"{model.BindHost}.weixin.rponey.cc";
            if (userBll.IsExistMobilePhone(bindHost))
                return Json(JsonResultHelper.Warn("对不起,该域名已被绑定！"));
            Session["RegisterCheckCode"] = null;
            var user = new ProductUser()
            {
                LoginName = model.LoginName,
                MobilePhone = model.MobilePhone,
                Name = model.Name,
                BindHost = bindHost,
                HeadPhoto = "http://b.hiphotos.baidu.com/zhidao/wh%3D450%2C600/sign=f0c5c08030d3d539c16807c70fb7c566/8ad4b31c8701a18bbef9f231982f07082838feba.jpg"
            };
            user.Password = user.EncryPassword(model.Password);
            var result = userBll.AddUser(user);
            if (result)
            {
                return Json(JsonResultHelper.Redirect(Url.Action("Index", "Account", new { area = "Admin" })));
            }
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
            Session["RegisterCheckCode"] = code.ToUpper();          // 使用Session取验证码的值
            Operator.CreateImage(code);
            return new EmptyResult();
        }

        [HttpGet]
        public ActionResult RetSetSendRedPackPassword(ProductUserViewModel model)
        {
            return View(model);
        }

        [HttpPost]
        public ActionResult RetSetSendRedPackPassword(ProductUserViewModel model, FormCollection collection)
        {
            var productUser = _productUserBll.Value.GetModel(model.LoginName);
            if (null != productUser)
            {
                if (productUser.Password == productUser.EncryPassword(model.Password))
                {
                    var result = _productUserBll.Value.UpdateSendRedPackPassword(productUser.EncryPassword(model.SendRedPackPassword), productUser.Id);
                    if (result)
                    {
                        return Json(JsonResultHelper.Success("密码更新成功"));
                    }
                    else
                    {
                        return Json(JsonResultHelper.Warn("密码更新失败"));
                    }
                }
            }
            return Json(JsonResultHelper.Warn("非法操作"));
        }
    }
}