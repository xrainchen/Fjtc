using Fjtc.DAL;
using Fjtc.Model.Entity;

namespace Fjtc.BLL
{
    public class UserBLL
    {
        private readonly UserDal _dal = new UserDal();
        public bool AddUser(User user)
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

        public User GetModel(string loginName)
        {
            return _dal.GetModel(loginName);
        }
    }
}
