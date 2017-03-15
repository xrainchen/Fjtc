using Fjtc.Model.Entity;
using System;
using System.Web;
using System.Web.Security;
using RPoney;

namespace admin.fjtc.com.Auth
{
    /// <summary>
    /// Cookie票据存储
    /// </summary>
    public class CookieTickStorage : ITicketStorage<CMSUser>
    {
        public void Cancellation()
        {
            FormsAuthentication.SignOut();
            if (HttpContext.Current.Request.Cookies[FormsAuthentication.FormsCookieName] != null)
            {
                var cookie = HttpContext.Current.Request.Cookies[FormsAuthentication.FormsCookieName];
                cookie.Expires = DateTime.Now.AddYears(-10);
                HttpContext.Current.Response.Cookies.Add(cookie);
            }

            if (HttpContext.Current.Request.Cookies[FormsAuthentication.FormsCookieName + "Login"] != null)
            {
                var cookie = HttpContext.Current.Request.Cookies[FormsAuthentication.FormsCookieName + "Login"];
                cookie.Expires = DateTime.Now.AddYears(-10);
                HttpContext.Current.Response.Cookies.Add(cookie);
            }
        }

        public CMSUser GetTicket()
        {
            if (HttpContext.Current.Request.Cookies[FormsAuthentication.FormsCookieName] != null)
            {
                if (HttpContext.Current.User.Identity.IsAuthenticated)
                {
                    var cookie1 = HttpContext.Current.Request.Cookies[FormsAuthentication.FormsCookieName];
                    if (cookie1 != null)
                    {
                        var hash = cookie1.Value;
                        var ticket = FormsAuthentication.Decrypt(hash);
                        var user = ticket.UserData.DeserializeFromJSON<CMSUser>();
                        if (user != null && user.Id > 0)
                            return user;
                    }
                    else
                    {
                        var username = HttpContext.Current.User.Identity.Name;
                        var user = new Fjtc.BLL.CMSUserBll().GetModel(username);
                        SetTicket(user);
                        return user;
                    }
                }
            }
            HttpContext.Current.Response.Redirect(FormsAuthentication.LoginUrl);
            return null;
        }

        public void SetTicket(CMSUser model)
        {
            if (model == null) return;
            model.Password = string.Empty;
            var cookieTime = DateTime.Now;
            var cookieExpiration = DateTime.Now.AddDays(1);
            var ticket = new FormsAuthenticationTicket(1, model.LoginName, cookieTime, cookieExpiration, true, model.SerializeToJSON());
            var hash = FormsAuthentication.Encrypt(ticket);
            var cookie1 = new HttpCookie(FormsAuthentication.FormsCookieName, hash) { Expires = cookieExpiration };
            HttpContext.Current.Response.Cookies.Add(cookie1);
        }
    }
}