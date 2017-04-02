using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using Fjtc.DbHelper;
using Fjtc.Model;
using Fjtc.Model.Entity;
using Fjtc.Model.SearchModel;
using Fjtc.Model.ViewModel;
using RPoney;
using RPoney.Data;
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
                var model = ModelConvertHelper<CMSUser>.ToModel(DataBaseManager.CmsDb().ExecuteFillDataTable(sqlStr, paramet));
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
                var model = ModelConvertHelper<CMSUserViewModel>.ToModel(DataBaseManager.CmsDb().ExecuteFillDataTable(sqlStr, paramet));
                return model;
            }
            catch (Exception ex)
            {
                LoggerManager.Error(this.GetType().Name, $"{desction}异常：", ex);
                return null;
            }
        }
        public bool AddUser(CMSUser entity)
        {
            try
            {
                var insertSql = $@"INSERT INTO CMSUser(Password,Name,CreatedBy,CreatedTime,Status,Number,LoginName,UserType)  Values(@Password, @Name, @CreatedBy, @CreatedTime, @Status, @Number, @LoginName,{(int)UserTypeEnum.User})";
                IDataParameter[] parameters ={
                new SqlParameter("@Password",SqlDbType.VarChar,32) {Value =  entity.Password},
                new SqlParameter("@Name", SqlDbType.NVarChar,32) {Value=entity.Name},
                new SqlParameter("@CreatedBy",SqlDbType.VarChar,32) {Value =  entity.CreatedBy},
                new SqlParameter("@CreatedTime", SqlDbType.DateTime) {Value = DateTime.Now},
                new SqlParameter("@Status",SqlDbType.Int) {Value =  entity.Status},
                new SqlParameter("@Number", SqlDbType.VarChar,16) {Value=entity.Number},
                new SqlParameter("@LoginName",SqlDbType.NVarChar,32) {Value =  entity.LoginName}
                };
                return DataBaseManager.CmsDb().ExecuteNonQuery(insertSql, parameters).CInt(0, false) > 0;
            }
            catch (Exception ex)
            {
                LoggerManager.Error(GetType().Name, "添加用户异常", ex);
                return false;
            }
        }

        public bool UpdateUser(CMSUserViewModel user)
        {
            try
            {
                var sql = $@" Update CMSUser Set Status=@Status,Name=@Name{(string.IsNullOrEmpty(user.Password) ? "" : ",Password=@Password")} Where Id=@Id And UserType<>{(int)UserTypeEnum.Administrator})";
                IDataParameter[] parameters ={
                new SqlParameter("@Id", SqlDbType.BigInt) {Value = user.Id},
                new SqlParameter("@Password",SqlDbType.VarChar,32) {Value =  user.Password},
                new SqlParameter("@Name", SqlDbType.NVarChar,32) {Value=user.Name},
                new SqlParameter("@LoginName", SqlDbType.NVarChar,32) {Value=user.LoginName},
                new SqlParameter("@Status",SqlDbType.Int) {Value =  user.Status},
                };
                return DataBaseManager.CmsDb().ExecuteNonQuery(sql, parameters).CInt(0, false) > 0;
            }
            catch (Exception ex)
            {
                LoggerManager.Error(GetType().Name, "更新用户异常", ex);
                return false;
            }
        }
        public IList<CMSUserViewModel> GetList(SearchParameter searachParam)
        {
            try
            {
                var tbname = " CMSUser with(nolock) ";
                var filter = " [Id],[Name],[LoginName],[CreatedTime],[Status],[CreatedBy],[Number],[UserType]";
                var orderField = " CreatedTime ";
                var where = $" and [UserType]={(int)UserTypeEnum.User}";
                var search = searachParam as CMSUserSearchParameter;
                var paramlist = new List<SqlParameter>();
                if (!string.IsNullOrWhiteSpace(search.LoginName))
                {
                    where += " and LoginName like @LoginName";
                    paramlist.Add(new SqlParameter("@LoginName", SqlDbType.NVarChar,32)
                    {
                        Value= $"'%{search.LoginName}%'"
                    });
                }
                var pageSql = DataBaseManager.GetPageString(tbname, filter, orderField, where, searachParam.Page, searachParam.PageSize);
                var countSql = DataBaseManager.GetCountString(tbname, where);
                searachParam.Count = int.Parse(DataBaseManager.MainDb().ExecuteScalar(countSql, paramlist.ToArray()).ToString());
                return ModelConvertHelper<CMSUserViewModel>.ToModels(DataBaseManager.CmsDb().ExecuteFillDataTable(pageSql, paramlist.ToArray()));

            }
            catch (Exception ex)
            {
                LoggerManager.Error(GetType().Name, "获取用户列表异常", ex);
                return Enumerable.Empty<CMSUserViewModel>().ToList();
            }
        }

        public bool UpdatePassword(CMSUserViewModel user)
        {
            try
            {
                var insertSql = "Update CMSUser Set Password=@Password Where Id=@Id";
                IDataParameter[] parameters ={
                new SqlParameter("@Id", SqlDbType.BigInt) {Value = user.Id},
                new SqlParameter("@Password",SqlDbType.VarChar,32) {Value =  user.Password}
                };
                return DataBaseManager.CmsDb().ExecuteNonQuery(insertSql, parameters) > 0;
            }
            catch (Exception ex)
            {
                LoggerManager.Error(GetType().Name, "修改密码异常", ex);
                return false;
            }
        }

    }
}
