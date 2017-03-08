using Fjtc.DAL;
using Fjtc.Model.Entity;

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
            return _user.AddUser(user, ref msg);
        }
    }
}
