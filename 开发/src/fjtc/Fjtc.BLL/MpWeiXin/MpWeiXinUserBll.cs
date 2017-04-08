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
        public bool Add(MpWeiXinUser entity)
        {
            return _dal.Add(entity);
        }
        public IList<MpWeiXinUser> GetList(SearchParameter serachParameter)
        {
            return _dal.GetList(serachParameter);
        }

        public MpWeiXinUser Get(long id)
        {
            return _dal.Get(id);
        }

        public MpWeiXinUser GetBelongMpProductUser(long id, long productUserId)
        {
            return _dal.GetBelongMpProductUser(id, productUserId);
        }

        public bool IsExistOpenId(string openId)
        {
            return _dal.IsExistOpenId(openId);
        }

        public bool RemarkUser(MpWeiXinUser entity)
        {
            return _dal.RemarkUser(entity);
        }
    }
}
