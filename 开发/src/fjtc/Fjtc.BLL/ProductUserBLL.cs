using System.Collections.Generic;
using Fjtc.DAL;
using Fjtc.Model;
using Fjtc.Model.Entity;
using Fjtc.Model.ViewModel;

namespace Fjtc.BLL
{
    public class ProductUserBLL
    {
        private readonly ProductUserDal _dal = new ProductUserDal();
        public bool AddUser(ProductUser user)
        {
            return _dal.AddUser(user);
        }

        public bool IsExistLoginName(string loginName)
        {
            return _dal.IsExistLoginName(loginName);
        }

        public bool IsExistMobilePhone(string mobilephone)
        {
            return _dal.IsExistMobilePhone(mobilephone);
        }

        public ProductUser GetModel(string loginName)
        {
            return _dal.GetModel(loginName);
        }
        public ProductUser Get(long id)
        {
            return _dal.Get(id);
        }
        public IList<ProductUserViewModel> GetList(SearchParameter searchObj)
        {
            return _dal.GetList(searchObj);
        }
    }
}
