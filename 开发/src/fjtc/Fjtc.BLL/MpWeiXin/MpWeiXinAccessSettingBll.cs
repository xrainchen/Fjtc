using Fjtc.DAL.MpWeiXin;
using Fjtc.Model.Entity;

namespace Fjtc.BLL.MpWeiXin
{
    public class MpWeiXinAccessSettingBll
    {
        private readonly MpWeiXinAccessSettingDal _mpWeiXinAccessSettingDal = new MpWeiXinAccessSettingDal();

        public MpWeiXinAccessSetting GetMpWeiXinAccessSetting(long userId)
        {
            return _mpWeiXinAccessSettingDal.GetMpWeiXinAccessSetting(userId);
        }

        public bool AddOrUpdate(MpWeiXinAccessSetting entity)
        {
            return _mpWeiXinAccessSettingDal.AddOrUpdate(entity);
        }
    }
}
