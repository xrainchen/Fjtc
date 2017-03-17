using System;
using System.ComponentModel;
using System.Web.Mvc;
using admin.fjtc.com.Auth;
using Fjtc.BLL;
using Fjtc.Model;
using Fjtc.Model.Entity;

namespace admin.fjtc.com.Controllers
{
    [Authorize]
    public class BaseController : Controller
    {
        private CMSUser _user;
        protected CMSUser CurrentUser
        {
            get
            {
                if (_user == null)
                {
                    var tick = TicketStorageFactory.InstanceTicketStorage<CMSUser>();
                    _user = tick.GetTicket();
                    if (_user == null)
                    {
                        if (User.Identity.IsAuthenticated)
                        {
                            var username = User.Identity.Name;
                            _user = new CMSUserBll().GetModel(username);
                            tick.SetTicket(_user);
                        }
                    }
                }
                //if (_user != null)
                //    _user.UserOperList = new UserBll().GetUserOperationList(_user.Id);
                return _user;
            }
            set
            {
                var tick = TicketStorageFactory.InstanceTicketStorage<CMSUser>();
                tick.SetTicket(value);
                _user = value;
                _user.Password = "";
            }
        }
        /// <summary>
        /// 权限过滤器
        /// </summary>
        /// <param name="filterContext"></param>
        protected override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            if (filterContext == null)
            {
                throw new ArgumentNullException(nameof(filterContext));
            }
            var inherit = true;
            if (!filterContext.ActionDescriptor.IsDefined(typeof(AllowAnonymousAttribute), inherit) &&
                !filterContext.ActionDescriptor.ControllerDescriptor.IsDefined(typeof(AllowAnonymousAttribute), inherit))
            {
                if (CurrentUser == null)
                {
                    //无效用户
                    filterContext.Result = RedirectToRoute(new { Controller = "Account", Action = "Index" });
                    return;
                }
            }
            //权限验证   
            var controllName = filterContext.ActionDescriptor.ControllerDescriptor.ControllerName;
            if (controllName != "Account" && controllName != "Main")
            {
                if (1 == CurrentUser.Id)
                {
                    return;
                }
                //当前执行的方法没有控制权限
                var actionDesc = filterContext.ActionDescriptor.GetCustomAttributes(typeof(DescriptionAttribute), false);
                if (actionDesc == null || actionDesc.Length == 0)
                {
                    return;
                }
                //判断权限 {Area}{ControllName}{ActionName}
                var actionName = filterContext.ActionDescriptor.ActionName;
                var actionRight = $"{filterContext.RouteData?.DataTokens["area"]}{controllName}{actionName}";
                //var rightResutl = CurrentUser.UserOperList?.Any(u => u.Code == actionRight);
                //if (!rightResutl.HasValue || !rightResutl.Value)
                //{
                //    filterContext.Result = DWZResultTips.Error("没有模块操作权限");
                //    return;
                //}
            }
        }

        protected virtual void BindParameter(SearchParameter searchParameter)
        {
            if (searchParameter.Page < 1) searchParameter.Page = 1;
            if (searchParameter.PageSize < 1) searchParameter.Page = 20;
        }
    }
}