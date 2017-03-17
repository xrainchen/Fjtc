using System.Web.Mvc;
using fjtc.com.Models;
using RPoney;
using RPoney.Log;
using Senparc.Weixin.MP;

namespace fjtc.com.Controllers
{
    /// <summary>
    /// 微信公众平台
    /// </summary>
    public class MpWeiXinController : BaseController
    {
        /// <summary>
        /// 授权：请以xxxxxx.weixin.rponey.cc  域名访问   xxxx是商户域名标识
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpGet]
        public ActionResult Auth(MpWeiXinAccessModel model)
        {
            LoggerManager.Debug(GetType().Name, "请求数据", model.SerializeToJSON());
            if (CheckSignature.Check(model.Signature, model.Timestamp, model.Nonce, CurrentAccessSetting.Token))
            {
                return Content(model.Echostr);
            }
            var errorMsg = "failed:" + model.Signature + "," + CheckSignature.GetSignature(model.Timestamp, model.Nonce, CurrentAccessSetting.Token) + "。如果您在浏览器中看到这条信息，表明此Url可以填入微信后台。";
            LoggerManager.Error(GetType().Name, "公众平台授权接入失败", errorMsg);
            return Content(errorMsg);
        }
    }
}