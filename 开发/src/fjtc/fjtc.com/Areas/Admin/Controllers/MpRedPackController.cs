using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using fjtc.com.Areas.Admin.Models;
using fjtc.com.Common;
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
        public ActionResult SendRedPack(RedPackModel model, FormCollection collection)
        {
            //校验
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
                    setting.ApiKey,
                    setting.CertPath,
                    user.OpenId,
                    CurrentUser.Company,
                    item.RedPackAmount,
                    model.Wishing,
                    model.ActionName,
                    model.Remark,
                    out nonceStr,
                    out paySign,
                    OrderHelper.GetMachBillno(setting.MachId),
                    model.SceneIdType

                    );
            }
            return null;
        }

        private IList<RedPackItemModel> MakeRedPack(RedPackModel model)
        {
            var totalMoney = model.RedPackTotalAmount.GetValueOrDefault() * 100;//单位换算  元换算成分
            var redpackmoney = Convert.ToInt32(totalMoney);
            var redPackList = new List<RedPackItemModel>();
            switch (model.SendRedPackType)
            {
                case 1://平均
                    redPackList.AddRange(model.MpUserIds.Select(mpUserIds => new RedPackItemModel()
                    {
                        MpUserId = mpUserIds,
                        RedPackAmount = redpackmoney
                    }));
                    break;
                case 2://随机
                    var people = model.MpUserIds.Count;
                    var min = 1;
                    for (int i = 0; i < people - 1; i++)
                    {
                        int leftPeople = people - i;
                        var avg = totalMoney / leftPeople;
                        //随机计算
                    }
                    break;
            }
            return redPackList;
        }
    }
}