using cms.rponey.cc.Utilty;
using Senparc.Weixin.MP;
using Senparc.Weixin.MP.TenPayLibV3;

namespace Fjtc.MpWeiXin.TenPayLibV3
{
    /// <summary>
    /// 红包
    /// </summary>
    public class RedPackApi
    {
        /// <summary>
        /// 发送普通红包
        /// </summary>
        /// <param name="appId"></param>
        /// <param name="mchId"></param>
        /// <param name="tenPayKey"></param>
        /// <param name="tenPayCertPath"></param>
        /// <param name="openId"></param>
        /// <param name="senderName"></param>
        /// <param name="redPackAmount"></param>
        /// <param name="wishingWord"></param>
        /// <param name="actionName"></param>
        /// <param name="remark"></param>
        /// <param name="nonceStr"></param>
        /// <param name="paySign"></param>
        /// <param name="mchBillNo"></param>
        /// <param name="scene"></param>
        /// <param name="riskInfo"></param>
        /// <param name="consumeMchId"></param>
        /// <returns></returns>
        public static NormalRedPackResult SendNormalRedPack(string appId, string mchId, string tenPayKey,
            string tenPayCertPath,
            string openId, string senderName,
            int redPackAmount, string wishingWord, string actionName, string remark,
            out string nonceStr, out string paySign,
            string mchBillNo, RedPack_Scene? scene = null, string riskInfo = null, string consumeMchId = null)
        {
            return Senparc.Weixin.MP.TenPayLibV3.RedPackApi.SendNormalRedPack(
                appId,
                mchId,
                tenPayKey,
                 tenPayCertPath,
                 openId,
                 senderName,
                 Tools.GetIp(),
                 redPackAmount,
                 wishingWord,
                 actionName,
                 remark,
                out nonceStr,
                out paySign,
                 mchBillNo,
                 scene,
                 riskInfo,
                 consumeMchId
                 );
        }
    }
}
