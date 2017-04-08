using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using fjtc.com.Areas.Admin.Models;
using fjtc.com.Common;
using Fjtc.BLL.MpWeiXin;
using Fjtc.Model.Entity;
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
            var errMsg = string.Empty;
            foreach (var item in MakeRedPack(model))
            {
                try
                {
                    var user = mpUserBll.GetBelongMpProductUser(item.MpUserId, CurrentUser.Id);
                    var nonceStr = string.Empty;
                    var paySign = string.Empty;
                    var result = Fjtc.MpWeiXin.TenPayLibV3.RedPackApi.SendNormalRedPack(
                         setting.AppId,
                         setting.MachId,
                         setting.ApiKey,
                         setting.CertPath,
                         user.OpenId,
                         model.SenderName,
                         item.RedPackAmount,
                         model.Wishing,
                         model.ActionName,
                         model.Remark,
                         out nonceStr,
                         out paySign,
                         OrderHelper.GetMachBillno(setting.MachId),
                         model.SceneIdType
                         );
                    RPoney.Log.LoggerManager.Info(GetType().Name, result.SerializeToJSON());
                    if (result.return_code == "SUCCESS")
                    {
                        if (result.result_code == "SUCCESS")
                        {
                            _mpWeiXinRePackLogBll.Value.Add(new MpWeiXinRedPackLogEntity()
                            {
                                CreatedBy = CurrentUser.LoginName,
                                OpenId = result.re_openid,
                                RedPackAmount = Convert.ToInt64(result.total_amount),
                                Remark = $"微信单号：{result.send_listid}",
                                ProductUserId = CurrentUser.Id
                            });
                        }
                        else if (result.result_code == "FAIL")
                        {
                            RPoney.Log.LoggerManager.Info(GetType().Name, $"{result.err_code}:{result.err_code_des}");
                            errMsg += $"发送失败:{ result.err_code}:{ result.err_code_des}{Environment.NewLine}";
                        }
                        else
                        {
                            errMsg += $"未知返回码:{result.result_code}";
                        }
                    }
                    else if (result.return_code == "FAIL ")
                    {
                        RPoney.Log.LoggerManager.Info(GetType().Name, $"{item.MpUserId}发送失败,消息:{result.return_msg}{Environment.NewLine}");
                        errMsg += $"{item.MpUserId}发送失败,消息:{result.return_msg}{Environment.NewLine}";
                    }
                    else
                    {
                        errMsg += $"未知返回码:{result.return_code}";
                    }

                }
                catch (Exception ex)
                {
                    errMsg += $"{item.MpUserId}发送失败,详细请查看日志{Environment.NewLine}";
                    RPoney.Log.LoggerManager.Error(GetType().Name, $"{item.MpUserId}发送红包异常", ex);
                }
            }
            if (!string.IsNullOrWhiteSpace(errMsg))
            {
                return DwzHelper.Warn(errMsg);
            }
            return DwzHelper.Success("发送成功");
        }

        private IList<RedPackItemModel> MakeRedPack(RedPackModel model)
        {
            var totalMoney = model.RedPackTotalAmount.GetValueOrDefault() * 100;//单位换算  元换算成分
            var redpackmoney = Convert.ToInt32(totalMoney);
            var redPackList = new List<RedPackItemModel>();
            var mpUserIds = model.MpUserIds.Split(',');
            switch (model.SendRedPackType)
            {
                case 1://平均
                    redPackList.AddRange(mpUserIds.Select(mpuserId => new RedPackItemModel()
                    {
                        MpUserId = Convert.ToInt64(mpuserId),
                        RedPackAmount = redpackmoney
                    }));
                    break;
                case 2://随机
                    var people = mpUserIds.Length;
                    var min = 1;
                    for (int i = 0; i < people; i++)
                    {
                        int leftPeople = people - i;
                        var avg = (redpackmoney * 1.00) / leftPeople;
                        //随机计算
                        var afgFloor = Convert.ToInt32(Math.Floor(Convert.ToDouble(avg)));
                        var redPackItem = new RedPackItemModel()
                        {
                            MpUserId = Convert.ToInt64(mpUserIds[i]),
                        };
                        if (leftPeople == 1)
                        {
                            redPackItem.RedPackAmount = redpackmoney;
                        }
                        else
                        {
                            var randomTime = new Random(leftPeople);
                            var time = randomTime.Next(1, leftPeople - 1);
                            var random = new Random(afgFloor * leftPeople);
                            var ranValue = random.Next(1, afgFloor * time);
                            redPackItem.RedPackAmount = ranValue;
                            redpackmoney = redpackmoney - ranValue;
                        }
                        redPackList.Add(redPackItem);
                    }
                    break;
            }
            return redPackList;
        }
    }
}