using System.Web.Mvc;

namespace admin.fjtc.com.Areas.User
{
    public class ProductUserAreaRegistration : AreaRegistration 
    {
        public override string AreaName => "ProductUser";

        public override void RegisterArea(AreaRegistrationContext context) 
        {
            context.MapRoute(
                "ProductUser_default",
                "ProductUser/{controller}/{action}/{id}",
                new { action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}