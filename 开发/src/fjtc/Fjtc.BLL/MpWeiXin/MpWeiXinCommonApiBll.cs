using System.Threading.Tasks;
using Fjtc.Model.Entity;
using Senparc.Weixin.MP.Entities;

namespace Fjtc.BLL.MpWeiXin
{
    public class MpWeiXinCommonApiBll
    {
        public async Task<AccessTokenResult> GetAccessTokenAsync(MpWeiXinAccessSetting setting)
        {
            return await Senparc.Weixin.MP.CommonAPIs.CommonApi.GetTokenAsync(setting.AppId, setting.AppSecret);
        }

        /// <summary>
        /// 获取AccessToken  缓存处理 1.5小时更新一次
        /// </summary>
        /// <param name="setting"></param>
        /// <returns></returns>
        public AccessTokenResult GetAccessToken(MpWeiXinAccessSetting setting)
        {
            return Senparc.Weixin.MP.CommonAPIs.CommonApi.GetToken(setting.AppId, setting.AppSecret);
        }

        public WeixinUserInfoResult GetUserInfo(string accessTokenOrAppid, string nextOpenId="")
        {
            return Senparc.Weixin.MP.CommonAPIs.CommonApi.GetUserInfo(accessTokenOrAppid, nextOpenId);
        }
        public async Task<WeixinUserInfoResult> GetUserInfoAsync(string accessTokenOrAppid, string nextOpenId = "")
        {
            return await Senparc.Weixin.MP.CommonAPIs.CommonApi.GetUserInfoAsync(accessTokenOrAppid, nextOpenId);
        }
    }
}
