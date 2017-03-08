using System;
using System.Data;
using System.Data.SqlClient;
using Fjtc.DbHelper;
using Fjtc.Model.Entity;
using RPoney.Data;
using RPoney.Log;

namespace Fjtc.DAL
{
    public class UserDal
    {
        public User GetModel(string userName)
        {
            string desction = "查询系统用户";
            try
            {
                string sqlStr = @"SELECT * FROM dbo.[CMSUser](NOLOCK) WHERE LoginName=@Name";
                SqlParameter[] paramet = new[] {
                    new SqlParameter("@Name",SqlDbType.NVarChar,50) { Value = userName },
                };
                var model = ModelConvertHelper<User>.ToModel(DataBaseManager.MainDb().ExecuteFillDataTable(sqlStr, paramet));
                return model;
            }
            catch (Exception ex)
            {
                LoggerManager.Error(this.GetType().Name, $"{desction}异常：", ex);
                return null;
            }
        }

        public bool AddUser(User user, ref string msg)
        {
            try
            {
                var insertSql = @"IF (SELECT COUNT(1) FROM CMSUser with(nolock) WHERE Name=@Name) = 0
                            BEGIN
                                INSERT INTO CMSUser(Password,Name,CreatedBy,CreatedTime,Status,Number,LoginName)  Values(@Password, @Name, @CreatedBy, @CreatedTime, @Status, @Number, @LoginName)
                            END";
                IDataParameter[] parameters ={
                new SqlParameter("@Password",SqlDbType.VarChar,32) {Value =  user.Password},
                new SqlParameter("@Name", SqlDbType.VarChar,16) {Value=user.Name},
                new SqlParameter("@CreatedBy",SqlDbType.VarChar,32) {Value =  user.CreatedBy},
                new SqlParameter("@CreatedTime", SqlDbType.DateTime) {Value = DateTime.Now},
                new SqlParameter("@Status",SqlDbType.Int) {Value =  user.Status},
                new SqlParameter("@Number", SqlDbType.VarChar,16) {Value=user.Number},
                new SqlParameter("@LoginName",SqlDbType.VarChar,32) {Value =  user.LoginName}
                };
                var isSuccess = DataBaseManager.MainDb().ExecuteNonQuery(insertSql, parameters) > 0;
                msg = isSuccess ? "添加成功" : "添加失败,用户已添加";
                return isSuccess;
            }
            catch (Exception ex)
            {
                LoggerManager.Error(GetType().Name, "添加用户异常", ex);
                msg = "添加用户异常";
                return false;
            }
        }
    }
}
