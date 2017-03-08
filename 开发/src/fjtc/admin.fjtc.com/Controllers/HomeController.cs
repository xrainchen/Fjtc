using System;
using System.Web.Mvc;
using admin.fjtc.com.Config;
using admin.fjtc.com.Models;
using Fjtc;
using Fjtc.Caching;

namespace admin.fjtc.com.Controllers
{
    public class HomeController : Controller
    {
        /// <summary>
        /// 首页
        /// </summary>
        /// <returns></returns>
        public ActionResult Index()
        {
            var cookie = HttpCookieHelper.GetCookie(SiteConfig.CookieLoginName);
            if (null == cookie) return RedirectToAction("Login", new LoginModel());
            var userId = CacheHelper.GetCache(CacheTypeEnum.Local).GetCacheOjbect<int>(cookie.Value);
            if (userId < 1)
            {
                return RedirectToAction("Login", new LoginModel());
            }
            //获取用户权限菜单列表
            return View();
        }

        public ActionResult Exit()
        {
            //退出处理
            var cookie = HttpCookieHelper.GetCookie(SiteConfig.CookieLoginName);
            if (null == cookie) RedirectToAction("Login", new LoginModel());
            CacheHelper.GetCache(CacheTypeEnum.Local).Add(0, cookie.Value, TimeSpan.FromSeconds(-20));
            HttpCookieHelper.SetCookie(SiteConfig.CookieLoginName, string.Empty, -1);
            //跳到登录页
            return RedirectToAction("Login", new LoginModel());
        }
        /// <summary>
        /// 登录
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult LoginValid(LoginModel model)
        {
            if (model.UserName != "rain" || model.Password != "123456")
                return Json(new { IsOk = false, Msg = "用户名或密码错误" });
            var guid = Guid.NewGuid().ToString();
            var userId = 1;
            HttpCookieHelper.SetCookie(SiteConfig.CookieLoginName, guid);
            //服务端存储用户信息
            CacheHelper.GetCache(CacheTypeEnum.Local).Add(userId, guid, TimeSpan.FromMinutes(20));
            return Json(new { IsOk = true, Msg = "登录成功" });
        }
    }
}