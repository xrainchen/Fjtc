using System.Collections.Generic;
using Fjtc.DAL.MpWeiXin;
using Fjtc.Model;
using Fjtc.Model.Entity;

namespace Fjtc.BLL.MpWeiXin
{
    public class MpWeiXinUserBll
    {
        private readonly MpWeiXinUserDal _dal=new MpWeiXinUserDal();
        public bool Save(MpWeiXinUser entity)
        {
            return _dal.Save(entity);
        }

        public IList<MpWeiXinUser> GetList(SearchParameter serachParameter)
        {
            return _dal.GetList(serachParameter);
        }
    }
}
