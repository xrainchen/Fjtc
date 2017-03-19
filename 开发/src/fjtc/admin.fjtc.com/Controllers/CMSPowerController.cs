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
    public class CMSPowerController : BaseController
    {
        // GET: CMSPower
        [HttpGet]
        public ActionResult List(CMSPowerSearchParameter searchObj)
        {
            searchObj.ReturnList = new CMSPowerBll().GetPowerTree();
            return View(searchObj);
        }
        [HttpPost]
        public ActionResult List(CMSPowerSearchParameter searchObj, FormCollection collection)
        {
            try
            {
                searchObj.ReturnList = new CMSPowerBll().GetAllPower();
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