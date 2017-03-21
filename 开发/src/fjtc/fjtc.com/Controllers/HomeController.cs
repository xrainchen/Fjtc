using System.Web.Mvc;
using Fjtc.Model;

namespace fjtc.com.Controllers
{
    public class HomeController : Controller
    {
        // GET: Home
        public ActionResult Index()
        {
            return View();
        }

        public void Error()
        {
            RPoney.Log.LoggerManager.Error(GetType().Name, "Error");
            RPoney.Log.LoggerManager.Info(GetType().Name, "Info");
            RPoney.Log.LoggerManager.Debug(GetType().Name, "Debug");
            RPoney.Log.LoggerManager.Fatal(GetType().Name, "Fatal");
            RPoney.Log.LoggerManager.Warn(GetType().Name, "Warn");
        }
    }
}