using System;
using System.Text;
using System.Web.Mvc;
using fjtc.com.Common;
using Fjtc.BLL.MpWeiXin;
using Fjtc.Common.Encrypt;
using Fjtc.Model.Entity;
using RPoney;
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
            var setting = new MpWeiXinAccessSettingBll().GetMpWeiXinAccessSetting(CurrentUser.Id) ?? new MpWeiXinAccessSetting { UserId = CurrentUser.Id };
            setting.AppId = string.IsNullOrWhiteSpace(setting.AppId) ? "" : Encoding.UTF8.GetString(setting.AppId.DesDecrypt().GetBytes());
            setting.AppSecret = string.IsNullOrWhiteSpace(setting.AppSecret) ? "" : Encoding.UTF8.GetString(setting.AppSecret.DesDecrypt().GetBytes());
            setting.Token = string.IsNullOrWhiteSpace(setting.Token) ? "" : Encoding.UTF8.GetString(setting.Token.DesDecrypt().GetBytes());
            setting.MachId = string.IsNullOrWhiteSpace(setting.MachId) ? "" : Encoding.UTF8.GetString(setting.MachId.DesDecrypt().GetBytes());
            setting.ApiKey = string.IsNullOrWhiteSpace(setting.ApiKey) ? "" : Encoding.UTF8.GetString(setting.ApiKey.DesDecrypt().GetBytes());
            ViewBag.CurrentUser = CurrentUser;
            return View(setting);
        }

        [HttpPost]
        public ActionResult Save(MpWeiXinAccessSetting model)
        {
            try
            {
                var setting = new MpWeiXinAccessSettingBll().GetMpWeiXinAccessSetting(CurrentUser.Id);
                var entity = setting ?? new MpWeiXinAccessSetting { UserId = CurrentUser.Id };
                entity.AppId = string.IsNullOrWhiteSpace(model.AppId) ? "" : Encoding.UTF8.GetBytes(model.AppId).GetHexString().DesEncrypt();
                entity.AppSecret = string.IsNullOrWhiteSpace(model.AppSecret) ? "" : Encoding.UTF8.GetBytes(model.AppSecret).GetHexString().DesEncrypt();
                entity.Token = string.IsNullOrWhiteSpace(model.Token) ? "" : Encoding.UTF8.GetBytes(model.Token).GetHexString().DesEncrypt();
                entity.MachId = string.IsNullOrWhiteSpace(model.MachId) ? "" : Encoding.UTF8.GetBytes(model.MachId).GetHexString().DesEncrypt();
                entity.ApiKey = string.IsNullOrWhiteSpace(model.ApiKey) ? "" : Encoding.UTF8.GetBytes(model.ApiKey).GetHexString().DesEncrypt();
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