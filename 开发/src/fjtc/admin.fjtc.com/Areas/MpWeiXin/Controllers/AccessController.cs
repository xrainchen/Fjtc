using System.Web.Mvc;
using admin.fjtc.com.Areas.MpWeiXin.Models;
using Fjtc.BLL.MpWeiXin;
using RPoney;
using RPoney.Log;
using Senparc.Weixin.MP;

namespace admin.fjtc.com.Areas.MpWeiXin.Controllers
{
    public class AccessController : Controller
    {
        // GET: MpWeiXin/Access

        /// <summary>
        /// 授权  ----后期改为二级域名  测试地址
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpGet]
        public ActionResult Index(MpWeiXinAccessModel model)
        {
            LoggerManager.Debug(GetType().Name, "请求数据", model.SerializeToJSON());
            var setting = new MpWeiXinAccessSettingBll().GetMpWeiXinAccessSetting(model.Id);
            if (null == setting) return Content($"对不起,不存在{model.Id}的配置信息");
            if (CheckSignature.Check(model.Signature, model.Timestamp, model.Nonce, setting.Token))
            {
                return Content(model.Echostr);
            }
            var errorMsg = "failed:" + model.Signature + "," +
                           CheckSignature.GetSignature(model.Timestamp, model.Nonce, setting.Token) +
                           "。如果您在浏览器中看到这条信息，表明此Url可以填入微信后台。";
            LoggerManager.Error(this.GetType().Name, $"公众平台授权接入失败", errorMsg);
            return Content(errorMsg);
        }
    }
}