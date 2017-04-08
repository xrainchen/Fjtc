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

        /// <summary>
        /// 同步新关注用户
        /// </summary>
        /// <param name="nextOpenId"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult SyncNewUserInfoData(string nextOpenId)
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
                        if (_mpWeiXinUserBll.Value.IsExistOpenId(openId))
                        {
                            continue;
                        }
                        var weiXinUser = Fjtc.MpWeiXin.CommonAPIs.CommonApi.GetUserInfo(AccessToken().access_token, openId);
                        if (_mpWeiXinUserBll.Value.Add(new MpWeiXinUser()
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
            return DwzHelper.Success($"新增粉丝：{success}条,新增失败：{fail}条");
        }

        [HttpGet]
        public ActionResult SelectMpUser(MpWeiXinUserSerachParameter search)
        {
            BindParameter(search);
            search.IsAll = true;
            search.ProductUserId = CurrentUser.Id;
            search.ReturnList = _mpWeiXinUserBll.Value.GetList(search);
            return View(search);
        }

        /// <summary>
        /// 备注用户
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpGet]
        public ActionResult RemarkUser(MpWeiXinUser model)
        {
            return View(model);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="model"></param>
        /// <param name="collection"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult RemarkUser(MpWeiXinUser model, FormCollection collection)
        {
            var entity = _mpWeiXinUserBll.Value.Get(model.Id);
            if (entity.ProductUserId != CurrentUser.Id)
            {
                return DwzHelper.Warn("非法操作");
            }
            entity.Remark = model.Remark;
            var result=_mpWeiXinUserBll.Value.RemarkUser(entity);
            if (result)
            {
                return DwzHelper.SuccessAndClose("", "备注完成");
            }
            return DwzHelper.Warn("备注异常");
        }
    }
}