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
        public UserViewModel GetModel(long id)
        {
            return _user.GetModel(id);
        }
        public bool AddUser(User user, ref string msg)
        {
            user.Password = user.EncryPassword(user.Password);
            return _user.AddUser(user, ref msg);
        }
        public bool UpdateUser(UserViewModel user, ref string msg)
        {
            if (!string.IsNullOrEmpty(user.Password))
            {
                user.Password = new User().EncryPassword(user.Password);
            }
            return _user.UpdateUser(user, ref msg);
        }
        public IList<UserViewModel> GetList(SearchParameter searachParam)
        {
            return _user.GetList(searachParam);
        }

        public bool UpdatePassword(UserViewModel user, ref string msg)
        {
            user.Password = new User().EncryPassword(user.Password);
            return _user.UpdatePassword(user, ref msg);
        }
    }
}
