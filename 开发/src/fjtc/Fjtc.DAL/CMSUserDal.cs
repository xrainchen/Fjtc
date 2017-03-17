using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using Fjtc.DbHelper;
using Fjtc.Model;
using Fjtc.Model.Entity;
using Fjtc.Model.SearchModel;
using Fjtc.Model.ViewModel;
using RPoney.Data;
using RPoney.Data.SqlClient;
using RPoney.Log;

namespace Fjtc.DAL
{
    public class CMSUserDal
    {
        public CMSUser GetModel(string userName)
        {
            string desction = "查询系统用户";
            try
            {
                string sqlStr = @"SELECT * FROM dbo.[CMSUser](NOLOCK) WHERE LoginName=@Name";
                SqlParameter[] paramet = new[] {
                    new SqlParameter("@Name",SqlDbType.NVarChar,50) { Value = userName },
                };
                var model = ModelConvertHelper<CMSUser>.ToModel(DataBaseManager.MainDb().ExecuteFillDataTable(sqlStr, paramet));
                return model;
            }
            catch (Exception ex)
            {
                LoggerManager.Error(this.GetType().Name, $"{desction}异常：", ex);
                return null;
            }
        }
        public CMSUserViewModel GetModel(long id)
        {
            string desction = "查询系统用户";
            try
            {
                string sqlStr = @"SELECT * FROM dbo.[CMSUser](NOLOCK) WHERE Id=@Id";
                SqlParameter[] paramet = new[] {
                    new SqlParameter("@Id",SqlDbType.BigInt) { Value = id },
                };
                var model = ModelConvertHelper<CMSUserViewModel>.ToModel(DataBaseManager.MainDb().ExecuteFillDataTable(sqlStr, paramet));
                return model;
            }
            catch (Exception ex)
            {
                LoggerManager.Error(this.GetType().Name, $"{desction}异常：", ex);
                return null;
            }
        }
        public bool AddUser(CMSUser user, ref string msg)
        {
            try
            {
                var insertSql = $@"IF (SELECT COUNT(1) FROM CMSUser with(nolock) WHERE Name=@Name) = 0
                            BEGIN
                                INSERT INTO CMSUser(Password,Name,CreatedBy,CreatedTime,Status,Number,LoginName,UserType)  Values(@Password, @Name, @CreatedBy, @CreatedTime, @Status, @Number, @LoginName,{(int)UserTypeEnum.User})
                            END";
                IDataParameter[] parameters ={
                new SqlParameter("@Password",SqlDbType.VarChar,32) {Value =  user.Password},
                new SqlParameter("@Name", SqlDbType.NVarChar,32) {Value=user.Name},
                new SqlParameter("@CreatedBy",SqlDbType.VarChar,32) {Value =  user.CreatedBy},
                new SqlParameter("@CreatedTime", SqlDbType.DateTime) {Value = DateTime.Now},
                new SqlParameter("@Status",SqlDbType.Int) {Value =  user.Status},
                new SqlParameter("@Number", SqlDbType.VarChar,16) {Value=user.Number},
                new SqlParameter("@LoginName",SqlDbType.NVarChar,32) {Value =  user.LoginName}
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

        public bool UpdateUser(CMSUserViewModel user, ref string msg)
        {
            try
            {
                var insertSql = $@"IF (SELECT COUNT(1) FROM CMSUser with(nolock) WHERE LoginName=@LoginName And Id<>@Id And UserType<>{(int)UserTypeEnum.Administrator}) = 0
                            BEGIN
                                Update CMSUser Set Status=@Status,Name=@Name{(string.IsNullOrEmpty(user.Password) ? "" : ",Password=@Password")} Where Id=@Id
                            END";
                IDataParameter[] parameters ={
                new SqlParameter("@Id", SqlDbType.BigInt) {Value = user.Id},
                new SqlParameter("@Password",SqlDbType.VarChar,32) {Value =  user.Password},
                new SqlParameter("@Name", SqlDbType.NVarChar,32) {Value=user.Name},
                new SqlParameter("@LoginName", SqlDbType.NVarChar,32) {Value=user.LoginName},
                new SqlParameter("@Status",SqlDbType.Int) {Value =  user.Status},
                };
                var isSuccess = DataBaseManager.MainDb().ExecuteNonQuery(insertSql, parameters) > 0;
                msg = isSuccess ? "更新成功" : "更新失败,用户名已存在";
                return isSuccess;
            }
            catch (Exception ex)
            {
                LoggerManager.Error(GetType().Name, "更新用户异常", ex);
                msg = "更新用户异常";
                return false;
            }
        }
        public IList<CMSUserViewModel> GetList(SearchParameter searachParam)
        {
            var tbname = " CMSUser with(nolock) ";
            var filter = " [Id],[Name],[LoginName],[CreatedTime],[Status],[CreatedBy],[Number],[UserType]";
            var orderField = " CreatedTime ";
            var where = $" and [UserType]={(int)UserTypeEnum.User}";
            var search = searachParam as CMSUserSearchParameter;
            var paramlist = new List<SqlParameter>();
            if (!string.IsNullOrWhiteSpace(search.LoginName))
            {
                where += $" and LoginName like '%{search.LoginName}%'";
                //paramlist.Add(new SqlParameter("@LoginName", search.LoginName));
            }
            var pageSql = DataBaseManager.GetPageString(tbname, filter, orderField, where, searachParam.Page,
                searachParam.PageSize);
            var countSql = DataBaseManager.GetCountString(tbname, where);
            searachParam.Count = int.Parse(DataBaseManager.MainDb().ExecuteScalar(countSql, paramlist.ToArray()).ToString());
            return ModelConvertHelper<CMSUserViewModel>.ToModels(DataBaseManager.MainDb().ExecuteFillDataTable(pageSql, paramlist.ToArray()));
        }

        public bool UpdatePassword(CMSUserViewModel user, ref string msg)
        {
            try
            {
                var insertSql = "Update CMSUser Set Password=@Password Where Id=@Id";
                IDataParameter[] parameters ={
                new SqlParameter("@Id", SqlDbType.BigInt) {Value = user.Id},
                new SqlParameter("@Password",SqlDbType.VarChar,32) {Value =  user.Password}
                };
                var isSuccess = DataBaseManager.MainDb().ExecuteNonQuery(insertSql, parameters) > 0;
                msg = isSuccess ? "修改密码成功" : "修改密码失败,";
                return isSuccess;
            }
            catch (Exception ex)
            {
                LoggerManager.Error(GetType().Name, "修改密码异常", ex);
                msg = "修改密码异常";
                return false;
            }
        }

    }
}
