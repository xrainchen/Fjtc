using System;
using System.Data;
using System.Data.SqlClient;
using Fjtc.DbHelper;
using Fjtc.Model.Entity;
using RPoney;
using RPoney.Log;


namespace Fjtc.DAL
{
    public class UserDal
    {
        public bool AddUser(User user)
        {
            try
            {
                var sql = @"IF (SELECT COUNT(1) FROM User with(nolock) WHERE LoginName=@LoginName OR MobilePhone=@MobilePhone) = 0
                            BEGIN
                                INSERT INTO User(Name,LoginName,Password,HeadPhoto,CreatedTime,MobilePhone,BindHost)  Values(@Name,@LoginName,@Password,@HeadPhoto,@CreatedTime,@MobilePhone,@BindHost);
                                SELECT 1;
                            END
                            ELSE
                            BEGIN
                            SELECT 0;
                            END";
                IDataParameter[] parameters ={
                new SqlParameter("@Name",SqlDbType.NVarChar,32) {Value =  user.Name},
                new SqlParameter("@LoginName", SqlDbType.VarChar,32) {Value=user.LoginName},
                new SqlParameter("@Password",SqlDbType.VarChar,32) {Value =  user.Password},
                new SqlParameter("@HeadPhoto", SqlDbType.VarChar) {Value = user.HeadPhoto},
                new SqlParameter("@CreatedTime",SqlDbType.DateTime) {Value =  DateTime.Now},
                new SqlParameter("@MobilePhone", SqlDbType.VarChar,16) {Value=user.MobilePhone},
                new SqlParameter("@BindHost",SqlDbType.VarChar,32) {Value =  user.BindHost}
                };
                return DataBaseManager.MainDb().ExecuteScalar(sql, parameters).CInt(0, false) > 0;
            }
            catch (Exception ex)
            {
                LoggerManager.Error(GetType().Name, "添加用户异常", ex);
                return false;
            }
        }

        public bool IsExistLoginName(string loginName)
        {
            try
            {
                var sql = "SELECT COUNT(1) FROM User with(nolock) WHERE LoginName=@LoginName";
                IDataParameter[] parameters ={
                new SqlParameter("@LoginName", SqlDbType.VarChar,32) {Value = loginName},
                };
                return DataBaseManager.MainDb().ExecuteScalar(sql, parameters).CInt(0, false) > 0;
            }
            catch (Exception ex)
            {
                LoggerManager.Error(GetType().Name, "查询登录名是否存在异常", ex);
                return false;
            }
        }

        public bool IsExistMobilePhone(string mobilephone)
        {
            try
            {
                var sql = "SELECT COUNT(1) FROM User with(nolock) WHERE MobilePhone=@MobilePhone";
                IDataParameter[] parameters ={
                new SqlParameter("@MobilePhone", SqlDbType.VarChar,32) {Value = mobilephone},
                };
                return DataBaseManager.MainDb().ExecuteScalar(sql, parameters).CInt(0, false) > 0;
            }
            catch (Exception ex)
            {
                LoggerManager.Error(GetType().Name, "查询登录名是否存在异常", ex);
                return false;
            }
        }
    }
}
