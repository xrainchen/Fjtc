using System;
using System.ComponentModel;
using System.Text;
using System.Web.Mvc;
using fjtc.com.Auth;
using Fjtc.BLL;
using Fjtc.BLL.MpWeiXin;
using Fjtc.Common.Encrypt;
using Fjtc.Model;
using Fjtc.Model.Entity;
using RPoney;
using Senparc.Weixin.MP.Containers;
using Senparc.Weixin.MP.Entities;

namespace fjtc.com.Areas.Admin.Controllers
{
    [Authorize]
    public class BaseAdminControl : Controller
    {
        // GET: Admin/BaseControl

        private ProductUser _user;
        protected ProductUser CurrentUser
        {
            get
            {
                if (_user == null)
                {
                    var tick = TicketStorageFactory.InstanceTicketStorage<ProductUser>();
                    _user = tick.GetTicket();
                    if (_user == null)
                    {
                        if (User.Identity.IsAuthenticated)
                        {
                            var username = User.Identity.Name;
                            _user = new ProductUserBLL().GetModel(username);
                            tick.SetTicket(_user);
                        }
                    }
                }
                return _user;
            }
            set
            {
                var tick = TicketStorageFactory.InstanceTicketStorage<ProductUser>();
                tick.SetTicket(value);
                _user = value;
                _user.Password = "";
            }
        }
        protected AccessTokenResult AccessToken()
        {
            var mpWeiXinAccessSetting = new MpWeiXinAccessSettingBll().GetMpWeiXinAccessSettingByHost(CurrentUser.BindHost);
            var appId = mpWeiXinAccessSetting.AppId;
            var appSecret = mpWeiXinAccessSetting.AppSecret;
            if (!AccessTokenContainer.CheckRegistered(appId))//检查是否已经注册
            {
                AccessTokenContainer.Register(appId, appSecret);//如果没有注册则进行注册
            }
            return AccessTokenContainer.GetAccessTokenResult(appId); //获取AccessToken结果
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
            if (!filterContext.ActionDescriptor.IsDefined(typeof(AllowAnonymousAttribute), inherit) && !filterContext.ActionDescriptor.ControllerDescriptor.IsDefined(typeof(AllowAnonymousAttribute), inherit))
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
            searchParameter.IsAll = false;
        }
    }
}