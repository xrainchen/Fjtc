using System.Web.Mvc;
using admin.fjtc.com.Common;
using Fjtc.BLL;
using Fjtc.Model.Entity;
using Fjtc.Model.SearchModel;
using ServiceStack.Common.Utils;

namespace admin.fjtc.com.Controllers
{
    public class CMSRoleController : BaseController
    {
        // GET: CMSRole

        [HttpGet]
        public ActionResult List(CMSRoleSearchParameter searchParameter)
        {
            BindParameter(searchParameter);
            return View(searchParameter);
        }

        [HttpPost]
        public ActionResult List(CMSRoleSearchParameter searchParameter, FormCollection collection)
        {
            BindParameter(searchParameter);
            searchParameter.ReturnList = new CMSRoleBll().GetList(searchParameter);
            return Json(searchParameter);
        }

        [HttpGet]
        public ActionResult Add(CMSRole model)
        {
            var entity = new CMSRoleBll().Get(model.Id);
            return View(entity ?? model);
        }

        [HttpPost]
        public ActionResult Add(CMSRole model, FormCollection collection)
        {
            var result = false;
            if (model.Id > 0)
            {
                var entity = new CMSRoleBll().Get(model.Id);
                entity.Name = model.Name;
                result = new CMSRoleBll().Update(entity);
            }
            else
            {
                result = new CMSRoleBll().Add(new CMSRole()
                {
                    Name = model.Name,
                    CreatedBy = User.Identity.Name
                });
            }
            if (result)
            {
                return DwzHelper.SuccessAndClose("", "添加成功");
            }
            return DwzHelper.Warn("添加失败");
        }

        [HttpPost]
        public ActionResult Delete(CMSRole model)
        {
            var result = new CMSRoleBll().Delete(model);
            if (result)
            {
                return DwzHelper.SuccessAndClose("", "删除成功");
            }
            return DwzHelper.Warn("删除失败");
        }
    }
}