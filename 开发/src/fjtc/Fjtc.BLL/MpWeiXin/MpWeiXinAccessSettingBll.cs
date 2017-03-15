using System.Collections.Concurrent;
using Fjtc.DAL.MpWeiXin;
using Fjtc.Model.Entity;

namespace Fjtc.BLL.MpWeiXin
{
    public class MpWeiXinAccessSettingBll
    {
        private readonly MpWeiXinAccessSettingDal _mpWeiXinAccessSettingDal = new MpWeiXinAccessSettingDal();
        private static readonly ConcurrentDictionary<string, MpWeiXinAccessSetting> AccessSettingDic = new ConcurrentDictionary<string, MpWeiXinAccessSetting>();

        public MpWeiXinAccessSetting GetMpWeiXinAccessSetting(long userId)
        {
            return _mpWeiXinAccessSettingDal.GetMpWeiXinAccessSetting(userId);
        }

        public MpWeiXinAccessSetting GetMpWeiXinAccessSetting(string userName)
        {
            return AccessSettingDic.GetOrAdd(userName, x => _mpWeiXinAccessSettingDal.GetMpWeiXinAccessSetting(userName));
        }
        public bool AddOrUpdate(MpWeiXinAccessSetting entity)
        {
            if (_mpWeiXinAccessSettingDal.AddOrUpdate(entity))
            {
                var user = new CMSUserBll().GetModel(entity.UserId);
                AccessSettingDic.TryRemove(user.LoginName, out entity);
                return true;
            }
            return false;
        }
    }
}
