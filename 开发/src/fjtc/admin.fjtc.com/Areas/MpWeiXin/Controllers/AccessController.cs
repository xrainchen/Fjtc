using System.Web.Mvc;
using admin.fjtc.com.Areas.MpWeiXin.Models;
using RPoney;
using RPoney.Log;
using Senparc.Weixin.MP;

namespace admin.fjtc.com.Areas.MpWeiXin.Controllers
{
    /// <summary>
    /// 开发者接入页面
    /// </summary>
    public class AccessController : BaseWeiXinController
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
            if (CheckSignature.Check(model.Signature, model.Timestamp, model.Nonce, CurrentAccessSetting.Token))
            {
                return Content(model.Echostr);
            }
            var errorMsg = "failed:" + model.Signature + "," +
                           CheckSignature.GetSignature(model.Timestamp, model.Nonce, CurrentAccessSetting.Token) +
                           "。如果您在浏览器中看到这条信息，表明此Url可以填入微信后台。";
            LoggerManager.Error(this.GetType().Name, "公众平台授权接入失败", errorMsg);
            return Content(errorMsg);
        }
    }
}