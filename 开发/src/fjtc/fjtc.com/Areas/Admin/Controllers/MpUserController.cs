using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using fjtc.com.Common;
using Fjtc.BLL.MpWeiXin;
using Fjtc.Model.Entity;
using Fjtc.Model.SearchModel;
using Senparc.Weixin;

namespace fjtc.com.Areas.Admin.Controllers
{
    public class MpUserController : BaseAdminControl
    {
        // GET: Admin/MpUser

        private readonly Lazy<MpWeiXinUserBll> _mpWeiXinUserBll = new Lazy<MpWeiXinUserBll>();

        [HttpGet]
        public ActionResult List(MpWeiXinUserSerachParameter serachParameter)
        {
            return View(serachParameter);
        }
        [HttpPost]
        public ActionResult List(MpWeiXinUserSerachParameter serachParameter, FormCollection collection)
        {
            BindParameter(serachParameter);
            serachParameter.ProductUserId = CurrentUser.Id;
            serachParameter.ReturnList = new MpWeiXinUserBll().GetList(serachParameter);
            return Json(serachParameter);
        }
        [HttpPost]
        public ActionResult SyncUserInfoData(string nextOpenId)
        {
            var success = 0;
            var fail = 0;
            var message = string.Empty;
            var result = Fjtc.MpWeiXin.AdvancedAPIs.UserApi.Get(AccessToken().access_token, nextOpenId);
            if (result.errcode == ReturnCode.请求成功)
            {
                if (result.data.openid.Any())
                {
                    foreach (var openId in result.data.openid)
                    {
                        var weiXinUser = Fjtc.MpWeiXin.CommonAPIs.CommonApi.GetUserInfo(AccessToken().access_token, openId);
                        if (_mpWeiXinUserBll.Value.Save(new MpWeiXinUser()
                        {
                            OpenId = weiXinUser.openid,
                            Subscribe = weiXinUser.subscribe,
                            NickName = weiXinUser.nickname,
                            Sex = weiXinUser.sex,
                            City = weiXinUser.city,
                            Country = weiXinUser.country,
                            Province = weiXinUser.province,
                            Language = weiXinUser.language,
                            HeadImgUrl = weiXinUser.headimgurl,
                            SubscribeTime = weiXinUser.subscribe_time,
                            UnionId = weiXinUser.unionid,
                            Remark = weiXinUser.remark,
                            GroupId = weiXinUser.groupid,
                            ProductUserId = CurrentUser.Id
                        }))
                        {
                            success++;
                            message += $"{openId}同步成功";
                        }
                        else
                        {
                            fail++;
                            message += $"{openId}同步失败";
                        }
                    }
                    RPoney.Log.LoggerManager.Debug(GetType().Name, $"同步记录:{message}");
                }
            }
            return DwzHelper.Success($"同步成功：{success}条,同步失败：{fail}条");
        }

        [HttpGet]
        public ActionResult SelectMpUser(MpWeiXinUserSerachParameter search)
        {
            BindParameter(search);
            search.IsAll = true;
            search.ReturnList = _mpWeiXinUserBll.Value.GetList(search);
            return View(search);
        }
    }
}