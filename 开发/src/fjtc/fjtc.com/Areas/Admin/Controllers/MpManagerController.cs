using System;
using System.Web.Mvc;
using fjtc.com.Common;
using Fjtc.BLL.MpWeiXin;
using Fjtc.Model.Entity;
using RPoney.Log;

namespace fjtc.com.Areas.Admin.Controllers
{
    public class MpManagerController : BaseAdminControl
    {
        // GET: Admin/MpManager

        /// <summary>
        /// 开发者接入配置
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult Setting()
        {
            var setting = new MpWeiXinAccessSettingBll().GetMpWeiXinAccessSetting(CurrentUser.Id);
            return View(setting ?? new MpWeiXinAccessSetting { UserId = CurrentUser.Id });
        }

        [HttpPost]
        public ActionResult Save(MpWeiXinAccessSetting model)
        {
            try
            {
                var setting = new MpWeiXinAccessSettingBll().GetMpWeiXinAccessSetting(CurrentUser.Id);
                var entity = setting ?? new MpWeiXinAccessSetting { UserId = CurrentUser.Id };
                entity.AppId = model.AppId;
                entity.AppSecret = model.AppSecret;
                entity.Token = model.Token;
                var result = new MpWeiXinAccessSettingBll().AddOrUpdate(entity);
                if (result)
                {
                    return DwzHelper.Success("保存成功");
                }
                return DwzHelper.Warn("保存失败");
            }
            catch (Exception ex)
            {
                LoggerManager.Error(GetType().Name, "保存微信AccessToken配置异常:", ex);
                return DwzHelper.Warn("保存异常");
            }
        }
    }
}