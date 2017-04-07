using System.Web.Mvc;
using fjtc.com.Common;
using Fjtc.BLL;
using Fjtc.Model.Entity;

namespace fjtc.com.Areas.Admin.Controllers
{
    public class ProductUserAdminController : BaseAdminControl
    {
        // GET: Admin/ProductUserAdmin
        [HttpGet]
        public ActionResult UpdatePassword(ProductUser model)
        {
            return View(model);
        }

        [HttpPost]
        public ActionResult UpdatePassword(ProductUser model, FormCollection collection)
        {
            var result = new ProductUserBLL().UpdatePassword(model.EncryPassword(model.Password), CurrentUser.Id);
            if (result)
            {
                return DwzHelper.SuccessAndClose("", "密码修改成功");
            }
            return DwzHelper.Warn("密码修改失败");
        }

        [HttpGet]
        public ActionResult Index()
        {
            var productUserBll = new ProductUserBLL();
            var user = productUserBll.Get(CurrentUser.Id);
            return View(user);
        }

        [HttpPost]
        public ActionResult Save(ProductUser model)
        {
            var productUserBll = new ProductUserBLL();
            var user = productUserBll.Get(CurrentUser.Id);
            user.SiteName = model.SiteName;
            user.Name = model.Name;
            user.Company = model.Company;
            var result = productUserBll.Update(user);
            if (result)
            {
                return DwzHelper.Success("修改成功");
            }
            return DwzHelper.Warn("修改失败");
        }
    }
}