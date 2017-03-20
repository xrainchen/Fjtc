using System.Collections.Generic;
using Fjtc.DAL;
using Fjtc.Model;
using Fjtc.Model.Entity;

namespace Fjtc.BLL
{
    public class CMSRoleBll
    {
        private readonly CMSRoleDal _dal = new CMSRoleDal();
        public bool Add(CMSRole entity)
        {
            return _dal.Add(entity);
        }
        public bool Update(CMSRole entity)
        {
            return _dal.Update(entity);
        }
        public IList<CMSRole> GetList(SearchParameter searchParameter)
        {
            return _dal.GetList(searchParameter);
        }

        public bool Delete(CMSRole entity)
        {
            return _dal.Delete(entity);
        }

        public CMSRole Get(long id)
        {
            return _dal.Get(id);
        }
    }
}
