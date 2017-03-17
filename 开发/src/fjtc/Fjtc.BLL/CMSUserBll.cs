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
        public bool AddUser(CMSUser user, ref string msg)
        {
            user.Password = user.EncryPassword(user.Password);
            return _user.AddUser(user, ref msg);
        }
        public bool UpdateUser(CMSUserViewModel user, ref string msg)
        {
            if (!string.IsNullOrEmpty(user.Password))
            {
                user.Password = new CMSUser().EncryPassword(user.Password);
            }
            return _user.UpdateUser(user, ref msg);
        }
        public IList<CMSUserViewModel> GetList(SearchParameter searachParam)
        {
            return _user.GetList(searachParam);
        }

        public bool UpdatePassword(CMSUserViewModel user, ref string msg)
        {
            user.Password = new CMSUser().EncryPassword(user.Password);
            return _user.UpdatePassword(user, ref msg);
        }
    }
}
