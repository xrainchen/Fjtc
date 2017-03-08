using System;
using System.Web;

namespace Fjtc
{
    public sealed class HttpCookieHelper
    {
        public static void SetCookie(string name, string value)
        {
            HttpContext.Current.Response.Cookies.Add(new HttpCookie(name) { Value = value });
        }

        public static string GetCookieValue(string name)
        {
            return GetCookie(name)?.Value;
        }

        public static HttpCookie GetCookie(string name)
        {
            return HttpContext.Current.Request.Cookies[name];
        }

        public static void SetCookie(string name, string value, int expireSeconds)
        {
            HttpContext.Current.Response.Cookies.Add(new HttpCookie(name) { Value = value, Expires = DateTime.Now.AddSeconds(expireSeconds) });
        }
    }
}
