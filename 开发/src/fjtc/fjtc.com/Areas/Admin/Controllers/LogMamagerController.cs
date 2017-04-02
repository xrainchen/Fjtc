using System;
using System.Web.Mvc;

namespace fjtc.com.Areas.Admin.Controllers
{
    public class LogMamagerController : Controller
    {
        // GET: Admin/LogMamager
        public ActionResult Index(string file)
        {

            try
            {
                return Content(System.IO.File.ReadAllText($@"{AppDomain.CurrentDomain.BaseDirectory}\Logs\{file}.txt"));
            }
            catch (Exception ex)
            {
                return Content(ex.ToString());
            }
        }
    }
}