using System;
using System.Web.Mvc;
using admin.fjtc.com.Common;
using Fjtc.BLL;
using Fjtc.Model.SearchModel;

namespace admin.fjtc.com.Controllers
{
    /// <summary>
    /// 权限控制器
    /// </summary>
    public class PowerController : BaseController
    {
        // GET: Power
        [HttpGet]
        public ActionResult List(PowerSearchParameter searchObj)
        {
            searchObj.ReturnList = new PowerBll().GetPowerTree();
            return View(searchObj);
        }
        [HttpPost]
        public ActionResult List(PowerSearchParameter searchObj, FormCollection collection)
        {
            try
            {
                searchObj.ReturnList = new PowerBll().GetAllPower();
                searchObj.Count = 1000;
                return Json(searchObj);
            }
            catch (Exception ex)
            {
                return DwzHelper.Warn(ex.Message);
            }
        }
    }
}