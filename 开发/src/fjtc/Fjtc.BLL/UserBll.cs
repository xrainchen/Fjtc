using System.Collections.Generic;
using Fjtc.DAL;
using Fjtc.Model;
using Fjtc.Model.Entity;
using Fjtc.Model.ViewModel;

namespace Fjtc.BLL
{
    public class UserBll
    {
        private readonly DAL.UserDal _user = new UserDal();
        public User GetModel(string username)
        {
            return _user.GetModel(username);
        }

        public bool AddUser(User user, ref string msg)
        {
            user.Password = user.EncryPassword(user.Password);
            return _user.AddUser(user, ref msg);
        }

        public IList<UserViewModel> GetList(SearchParameter searachParam)
        {
            return _user.GetList(searachParam);
        }
    }
}
