using System.Web.Mvc;
using admin.fjtc.com.Common;
using Fjtc.BLL;
using Fjtc.Model.Entity;
using Fjtc.Model.SearchModel;
using Fjtc.Model.ViewModel;

namespace admin.fjtc.com.Controllers
{
    public class CMSMenuController : BaseController
    {
        // GET: CMSMenu
        [HttpGet]
        public ActionResult List(CMSMenuSearchParameter searchParameter)
        {
            searchParameter.ReturnList = new CMSMenuBll().GetTreeList();
            return View(searchParameter);
        }

        [HttpPost]
        public ActionResult List(CMSMenuSearchParameter searchParameter, FormCollection collection)
        {
            return Json(new CMSMenuBll().GetList(searchParameter));
        }
        [HttpGet]
        public ActionResult Add(CMSMenuViewModel menu)
        {
            var model = new CMSMenuViewModel();
            if (menu.ParentId > 0)
            {
                var parent = new CMSMenuBll().Get(menu.Id);
                model.ParentId = parent.Id;
                model.ParentOrganPath = parent.OrganPath;
            }
            else
            {
                var virtualRoot = new CMSMenuBll().GetRootMenu();
                model.ParentId = virtualRoot.Id;
                model.ParentOrganPath = virtualRoot.OrganPath;
            }
            return View(model);
        }
        [HttpGet]
        public ActionResult AddSub(CMSMenuViewModel menu)
        {
            var virtualRoot = new CMSMenuBll().GetRootMenu();
            menu.ParentMenus = new CMSMenuBll().GetList(new CMSMenuSearchParameter() { ParentId = virtualRoot.Id });
            return View(menu);
        }
        

        [HttpPost]
        public ActionResult Add(CMSMenu menu, FormCollection collection)
        {
            menu.CreatedBy = CurrentUser.LoginName;
            var result = new CMSMenuBll().Add(menu);
            if (result)
            {
                return DwzHelper.SuccessAndClose("", "添加成功");
            }
            return DwzHelper.Warn("添加失败");
        }



        [HttpGet]
        public ActionResult Edit(CMSMenuViewModel menu)
        {
            return View(new CMSMenuBll().Get(menu.Id));
        }
        [HttpPost]
        public ActionResult Edit(CMSMenu menu, FormCollection collection)
        {
            var entity = new CMSMenuBll().Get(menu.Id);
            entity.Url = menu.Url;
            entity.Name = menu.Name;
            entity.PowerValue = menu.PowerValue;
            entity.Sort = menu.Sort;
            entity.OrganPath = menu.OrganPath;
            entity.MenuType = menu.MenuType;
            var result = new CMSMenuBll().Update(entity);
            if (result)
            {
                return DwzHelper.Success("更新成功");
            }
            return DwzHelper.Warn("更新失败");
        }
    }
}