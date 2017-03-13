using System.Web.Mvc;

namespace admin.fjtc.com.Areas.MpWeiXin
{
    public class MpWeiXinAreaRegistration : AreaRegistration
    {
        /// <summary>
        /// 微信公众平台
        /// </summary>
        public override string AreaName => "MpWeiXin";

        public override void RegisterArea(AreaRegistrationContext context)
        {
            context.MapRoute(
                "MpWeiXin_default",
                "MpWeiXin/{controller}/{action}/{id}",
                new { action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}