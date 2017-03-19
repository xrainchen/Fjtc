using System.Web.Mvc;
using Fjtc.Model.SearchModel;

namespace admin.fjtc.com.Controllers
{
    public class CMSDictionaryController : BaseController
    {

        // GET: CMSDictionary
        [HttpGet]
        public ActionResult List(CMSDictionarySearchParameter searchParameter)
        {
            return View(searchParameter);
        }

        [HttpPost]
        public ActionResult List(CMSDictionarySearchParameter searchParameter, FormCollection collection)
        {
            return Json(searchParameter);
        }
    }
}