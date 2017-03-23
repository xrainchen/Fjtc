using System.Threading.Tasks;
using Senparc.Weixin.MP.Entities;

namespace Fjtc.MpWeiXin.CommonAPIs
{
    public class CommonApi
    {
        public static async Task<AccessTokenResult> GetAccessTokenAsync(string appid, string appSecret)
        {
            return await Senparc.Weixin.MP.CommonAPIs.CommonApi.GetTokenAsync(appid, appSecret);
        }

        /// <summary>
        /// 获取AccessToken  缓存处理 1.5小时更新一次
        /// </summary>
        /// <param name="appid"></param>
        /// <param name="appSecret"></param>
        /// <returns></returns>
        public static AccessTokenResult GetAccessToken(string appid, string appSecret)
        {
            return Senparc.Weixin.MP.CommonAPIs.CommonApi.GetToken(appid, appSecret);
        }

        public static WeixinUserInfoResult GetUserInfo(string accessTokenOrAppid, string nextOpenId = "")
        {
            return Senparc.Weixin.MP.CommonAPIs.CommonApi.GetUserInfo(accessTokenOrAppid, nextOpenId);
        }
        public static async Task<WeixinUserInfoResult> GetUserInfoAsync(string accessTokenOrAppid, string nextOpenId = "")
        {
            return await Senparc.Weixin.MP.CommonAPIs.CommonApi.GetUserInfoAsync(accessTokenOrAppid, nextOpenId);
        }
    }
}
