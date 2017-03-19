﻿using System;
using System.Web.Mvc;
using fjtc.com.Auth;
using Fjtc.BLL;
using Fjtc.BLL.MpWeiXin;
using Fjtc.Model.Entity;
using Senparc.Weixin.MP.Containers;
using Senparc.Weixin.MP.Entities;

namespace fjtc.com.Controllers
{
    public class BaseController : Controller
    {
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

        private MpWeiXinAccessSetting _currentAccessSetting;
        protected MpWeiXinAccessSetting CurrentAccessSetting => _currentAccessSetting;

        protected AccessTokenResult AccessToken()
        {
            if (!AccessTokenContainer.CheckRegistered(_currentAccessSetting.AppId))//检查是否已经注册
            {
                AccessTokenContainer.Register(_currentAccessSetting.AppId, _currentAccessSetting.AppSecret);//如果没有注册则进行注册
            }
            return AccessTokenContainer.GetAccessTokenResult(_currentAccessSetting.AppId); //获取AccessToken结果
        }

        protected override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            if (filterContext == null)
            {
                throw new ArgumentNullException(nameof(filterContext));
            }
            if (_currentAccessSetting == null)
            {
                var username = string.Empty;
                if (!string.IsNullOrWhiteSpace(Request.Url?.Host))
                {
                    var userNameList = Request.Url?.Host.Split('.');
                    if (userNameList.Length > 2)
                    {
                        if (userNameList[1].Equals("weixin", StringComparison.OrdinalIgnoreCase))
                        {
                            username = userNameList[0];
                            _currentAccessSetting = new MpWeiXinAccessSettingBll().GetMpWeiXinAccessSetting(username);
                        }
                    }
                }
                if (_currentAccessSetting == null)
                {
                    filterContext.Result = new ContentResult() { Content = "非法的用户" };
                }
                else
                {
                    TicketStorageFactory.InstanceTicketStorage<ProductUser>().SetTicket(new ProductUserBLL().GetModel(username));
                }
            }
        }
    }
}