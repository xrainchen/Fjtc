using System;
using Fjtc.DAL.MpWeiXin;
using Fjtc.Model.Entity;

namespace Fjtc.BLL.MpWeiXin
{
    public class MpWeiXinRedPackLogBll
    {
        private readonly Lazy<MpWeiXinRedPackLogDal> _mpWeiXinRedPackLogDal = new Lazy<MpWeiXinRedPackLogDal>();
        public bool Add(MpWeiXinRedPackLog entity)
        {
            return _mpWeiXinRedPackLogDal.Value.Add(entity);
        }
    }
}
