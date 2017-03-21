using System;
using System.Web.Mvc;
using System.Web.Routing;

namespace fjtc.com
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            RouteConfig.RegisterRoutes(RouteTable.Routes);
        }

        protected void Application_Error(object sender, EventArgs e)
        {
            try
            {
                var ex = Server.GetLastError().GetBaseException(); //获取异常源  
                RPoney.Log.LoggerManager.Error(GetType().Name, "全局异常", ex);
                Response.Write("抱歉,系统出错了！");
                //清空前一个异常  
                Server.ClearError();
            }
            catch (Exception ex)
            {
                RPoney.Log.LoggerManager.Error(GetType().Name, "全局异常", ex);
            }
        }
    }
}
