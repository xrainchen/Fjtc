using System.Collections.Generic;
using Fjtc.DAL;
using Fjtc.Model;
using Fjtc.Model.Entity;
using Fjtc.Model.ViewModel;

namespace Fjtc.BLL
{
    public class CMSUserBll
    {
        private readonly DAL.CMSUserDal _user = new CMSUserDal();
        public CMSUser GetModel(string username)
        {
            return _user.GetModel(username);
        }
        public CMSUserViewModel GetModel(long id)
        {
            return _user.GetModel(id);
        }
        public bool AddUser(CMSUser user)
        {
            user.Password = user.EncryPassword(user.Password);
            return _user.AddUser(user);
        }
        public bool UpdateUser(CMSUserViewModel user)
        {
            if (!string.IsNullOrEmpty(user.Password))
            {
                user.Password = new CMSUser().EncryPassword(user.Password);
            }
            return _user.UpdateUser(user);
        }
        public IList<CMSUserViewModel> GetList(SearchParameter searachParam)
        {
            return _user.GetList(searachParam);
        }

        public bool UpdatePassword(CMSUserViewModel user)
        {
            user.Password = new CMSUser().EncryPassword(user.Password);
            return _user.UpdatePassword(user);
        }
    }
}
