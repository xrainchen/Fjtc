using System;
using System.Collections.Generic;
using Fjtc.DAL.MpWeiXin;
using Fjtc.Model;
using Fjtc.Model.Entity;
using Fjtc.Model.ViewModel;

namespace Fjtc.BLL.MpWeiXin
{
    public class MpWeiXinRedPackLogBll
    {
        private readonly Lazy<MpWeiXinRedPackLogDal> _mpWeiXinRedPackLogDal = new Lazy<MpWeiXinRedPackLogDal>();
        public bool Add(MpWeiXinRedPackLogEntity entity)
        {
            return _mpWeiXinRedPackLogDal.Value.Add(entity);
        }
        public bool Update(MpWeiXinRedPackLogEntity entity)
        {
            return _mpWeiXinRedPackLogDal.Value.Update(entity);
        }
        public IList<MpWeiXinRedPackLogEntity> Get(MpWeiXinRedPackLogEntity entity)
        {
            return _mpWeiXinRedPackLogDal.Value.Get(entity);
        }
        public MpWeiXinRedPackLogEntity Get(long id)
        {
            return _mpWeiXinRedPackLogDal.Value.Get(id);
        }
        public bool Delete(long id)
        {
            return _mpWeiXinRedPackLogDal.Value.Delete(id);
        }
        public bool Exist(long id)
        {
            return _mpWeiXinRedPackLogDal.Value.Exist(id);
        }

        public IList<MpWeiXinRedPackLogViewModel> GetRedPackLog(SearchParameter search)
        {
            return _mpWeiXinRedPackLogDal.Value.GetRedPackLog(search);
        }
    }
}
