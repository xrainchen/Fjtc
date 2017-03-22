using System.Collections.Generic;
using System.Threading.Tasks;

namespace Fjtc.BLL.MpWeiXin
{
    public class MpWeiXinAdvancedApiBll
    {
        public Senparc.Weixin.MP.AdvancedAPIs.User.BatchGetUserInfoJsonResult BatchGetUserInfo(string accessTokenOrAppid, List<Senparc.Weixin.MP.AdvancedAPIs.User.BatchGetUserInfoData> userList)
        {
            return Senparc.Weixin.MP.AdvancedAPIs.UserApi.BatchGetUserInfo(accessTokenOrAppid, userList);
        }

        public async Task<Senparc.Weixin.MP.AdvancedAPIs.User.BatchGetUserInfoJsonResult> BatchGetUserInfoAsync(string accessTokenOrAppid, List<Senparc.Weixin.MP.AdvancedAPIs.User.BatchGetUserInfoData> userList)
        {
            return  await Senparc.Weixin.MP.AdvancedAPIs.UserApi.BatchGetUserInfoAsync(accessTokenOrAppid, userList);
        }
    }
}
