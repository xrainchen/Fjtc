using System;
using System.Collections.Generic;
using System.Web.Mvc;
using fjtc.com.Areas.Admin.Models;
using Fjtc.BLL.MpWeiXin;
using Fjtc.Model.SearchModel;
using RPoney;

namespace fjtc.com.Areas.Admin.Controllers
{
    public class MpRedPackController : BaseAdminControl
    {
        // GET: Admin/MpRedPack
        private readonly Lazy<MpWeiXinRedPackLogBll> _mpWeiXinRePackLogBll = new Lazy<MpWeiXinRedPackLogBll>();

        [HttpGet]
        public ActionResult List(MpWeiXinRedPackLogSearchParameter searchParameter)
        {
            return View(searchParameter);
        }
        [HttpPost]
        public ActionResult List(MpWeiXinRedPackLogSearchParameter searchParameter, FormCollection collection)
        {
            BindParameter(searchParameter);
            searchParameter.ReturnList = _mpWeiXinRePackLogBll.Value.GetRedPackLog(searchParameter);
            return Json(searchParameter);
        }

        [HttpGet]
        public ActionResult SendRedPack(RedPackModel model)
        {
            return View(model);
        }

        [ValidateAntiForgeryToken]
        [HttpPost]
        public ActionResult SendRedPack(RedPackModel model,FormCollection collection)
        {
            RPoney.Log.LoggerManager.Info(GetType().Name, "发送红包", model.SerializeToJSON());
            var setting = new MpWeiXinAccessSettingBll().GetMpWeiXinAccessSettingByHost(CurrentUser.BindHost);
            var mpUserBll = new MpWeiXinUserBll();
            foreach (var item in MakeRedPack(model))
            {
                var user = mpUserBll.GetBelongMpProductUser(item.MpUserId, CurrentUser.Id);
                var nonceStr = string.Empty;
                var paySign = string.Empty;
                Fjtc.MpWeiXin.TenPayLibV3.RedPackApi.SendNormalRedPack(
                    setting.AppId,
                    setting.MachId,
                    "tenPayCertPath",
                    "tenPayCertPath",
                    user.OpenId,
                    "senderName",
                    "iP",
                    item.RedPackAmount,
                    "wishingWord",
                    "actionName",
                    model.Remark,
                    out nonceStr,
                    out paySign,
                    "mchBillNo"
                    );
            }
            return null;
        }

        private IList<RedPackItemModel> MakeRedPack(RedPackModel model)
        {
            var redPackList = new List<RedPackItemModel>();
            switch (model.SendRedPackType)
            {
                case 1://平均

                    break;
                case 2://随机

                    break;
            }
            return redPackList;
        }
    }
}