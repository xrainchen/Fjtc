﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
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
    public class ProductUserDal
    {

        public ProductUser GetModel(string loginName)
        {
            string desction = "查询系统用户";
            try
            {
                string sqlStr = @"SELECT * FROM dbo.[ProductUser](NOLOCK) WHERE LoginName=@Name";
                SqlParameter[] paramet = new[] {
                    new SqlParameter("@Name",SqlDbType.NVarChar,50) { Value = loginName },
                };
                var model = ModelConvertHelper<ProductUser>.ToModel(DataBaseManager.MainDb().ExecuteFillDataTable(sqlStr, paramet));
                return model;
            }
            catch (Exception ex)
            {
                LoggerManager.Error(this.GetType().Name, $"{desction}异常：", ex);
                return null;
            }
        }

        public ProductUser Get(long id)
        {
            string desction = "查询系统用户";
            try
            {
                string sqlStr = @"SELECT * FROM dbo.[ProductUser](NOLOCK) WHERE Id=@Id";
                SqlParameter[] paramet = new[] {
                    new SqlParameter("@Id",SqlDbType.BigInt) { Value = id },
                };
                var model = ModelConvertHelper<ProductUser>.ToModel(DataBaseManager.MainDb().ExecuteFillDataTable(sqlStr, paramet));
                return model;
            }
            catch (Exception ex)
            {
                LoggerManager.Error(this.GetType().Name, $"{desction}异常：", ex);
                return null;
            }
        }
        public bool AddUser(ProductUser user)
        {
            try
            {
                var sql = @"INSERT INTO [ProductUser](Name,LoginName,Password,HeadPhoto,CreatedTime,MobilePhone,BindHost,Company,SiteName)
                            Values(@Name,@LoginName,@Password,@HeadPhoto,@CreatedTime,@MobilePhone,@BindHost,@Company,@SiteName);";
                IDataParameter[] parameters ={
                new SqlParameter("@Name",SqlDbType.NVarChar,32) {Value =  user.Name},
                new SqlParameter("@LoginName", SqlDbType.VarChar,32) {Value=user.LoginName},
                new SqlParameter("@Password",SqlDbType.VarChar,32) {Value =  user.Password},
                new SqlParameter("@HeadPhoto", SqlDbType.VarChar) {Value = user.HeadPhoto},
                new SqlParameter("@CreatedTime",SqlDbType.DateTime) {Value =  DateTime.Now},
                new SqlParameter("@MobilePhone", SqlDbType.VarChar,16) {Value=user.MobilePhone},
                new SqlParameter("@BindHost",SqlDbType.VarChar,32) {Value =  user.BindHost},
                new SqlParameter("@Company",SqlDbType.NVarChar,64) {Value =  user.Company},
                new SqlParameter("@SiteName",SqlDbType.NVarChar,64) {Value =  user.SiteName},
                };
                return DataBaseManager.MainDb().ExecuteNonQuery(sql, parameters).CInt(0, false) > 0;
            }
            catch (Exception ex)
            {
                LoggerManager.Error(GetType().Name, "添加用户异常", ex);
                return false;
            }
        }

        public bool Update(ProductUser user)
        {
            try
            {
                var sql = @"Update [ProductUser] Set 
                            Name=@Name,
                            LoginName=@LoginName,
                            Password=@Password,
                            HeadPhoto=@HeadPhoto,
                            MobilePhone=@MobilePhone,
                            BindHost=@BindHost,
                            Company=@Company,
                            SiteName=@SiteName
                            WHERE Id=@Id";
                IDataParameter[] parameters ={
                    new SqlParameter("@Id",SqlDbType.BigInt) {Value =  user.Id},
                    new SqlParameter("@Name",SqlDbType.NVarChar,32) {Value =  user.Name},
                new SqlParameter("@LoginName", SqlDbType.VarChar,32) {Value=user.LoginName},
                new SqlParameter("@Password",SqlDbType.VarChar,32) {Value =  user.Password},
                new SqlParameter("@HeadPhoto", SqlDbType.VarChar) {Value = user.HeadPhoto},
                new SqlParameter("@MobilePhone", SqlDbType.VarChar,16) {Value=user.MobilePhone},
                new SqlParameter("@BindHost",SqlDbType.VarChar,32) {Value =  user.BindHost},
                new SqlParameter("@Company",SqlDbType.NVarChar,64) {Value =  user.Company},
                new SqlParameter("@SiteName",SqlDbType.NVarChar,64) {Value =  user.SiteName},
                };
                return DataBaseManager.MainDb().ExecuteNonQuery(sql, parameters).CInt(0, false) > 0;
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
                var sql = "SELECT Count(1) FROM [ProductUser] WHERE  EXISTS(SELECT Id FROM [ProductUser] with(nolock) WHERE LoginName=@LoginName)";
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
                var sql = "SELECT Count(1) FROM [ProductUser] WHERE EXISTS(SELECT Id FROM [ProductUser] with(nolock) WHERE MobilePhone=@MobilePhone)";
                IDataParameter[] parameters ={
                new SqlParameter("@MobilePhone", SqlDbType.VarChar,32) {Value = mobilephone},
                };
                return DataBaseManager.MainDb().ExecuteScalar(sql, parameters).CInt(0, false) > 0;
            }
            catch (Exception ex)
            {
                LoggerManager.Error(GetType().Name, "查询绑定域名是否存在异常", ex);
                return false;
            }
        }
        public bool IsExistBindHost(string bindHost)
        {
            try
            {
                var sql = "SELECT Count(1) FROM [ProductUser] WHERE EXISTS(SELECT Id FROM [ProductUser] with(nolock) WHERE BindHost=@BindHost)";
                IDataParameter[] parameters ={
                new SqlParameter("@BindHost", SqlDbType.VarChar,32) {Value = bindHost},
                };
                return DataBaseManager.MainDb().ExecuteScalar(sql, parameters).CInt(0, false) > 0;
            }
            catch (Exception ex)
            {
                LoggerManager.Error(GetType().Name, "查询登录名是否存在异常", ex);
                return false;
            }
        }
        public IList<ProductUserViewModel> GetList(SearchParameter searchObj)
        {
            var tbname = " [ProductUser] u with(nolock) ";
            var filter = " Id,Name,LoginName,HeadPhoto,MobilePhone,BindHost,CreatedTime";
            var where = "";
            var orderField = " u.CreatedTime desc";
            var paramlist = new List<SqlParameter>();
            var searchParm = searchObj as ProductUserSearchParameter;
            if (!string.IsNullOrWhiteSpace(searchParm.Name))
            {
                where += " and Name like @Name";
                paramlist.Add(new SqlParameter("@Name", SqlDbType.NVarChar) { Value = "%" + searchParm.Name + "%" });
            }
            if (!string.IsNullOrWhiteSpace(searchParm.LoginName))
            {
                where += " and LoginName like @LoginName";
                paramlist.Add(new SqlParameter("@LoginName", SqlDbType.NVarChar) { Value = "%" + searchParm.LoginName + "%" });
            }
            if (!string.IsNullOrWhiteSpace(searchParm.MobilePhone))
            {
                where += " and MobilePhone=@MobilePhone";
                paramlist.Add(new SqlParameter("@MobilePhone", SqlDbType.NVarChar) { Value = searchParm.MobilePhone });
            }
            var pageSql = DataBaseManager.GetPageString(tbname, filter, orderField, where, searchObj.Page, searchObj.PageSize);
            var countSql = DataBaseManager.GetCountString(tbname, where);
            searchObj.Count = int.Parse(DataBaseManager.MainDb().ExecuteScalar(countSql, paramlist.ToArray()).ToString());
            return ModelConvertHelper<ProductUserViewModel>.ToModels(DataBaseManager.MainDb().ExecuteFillDataTable(pageSql, paramlist.ToArray()));
        }

        public bool UpdatePassword(string password, long id)
        {
            var sql = "update ProductUser set Password=@Password where Id=@Id";
            IDataParameter[] parameters ={
                new SqlParameter("@Id",SqlDbType.NVarChar,32) {Value = id},
                new SqlParameter("@Password",SqlDbType.VarChar,32) {Value =  password}
                };
            return DataBaseManager.MainDb().ExecuteNonQuery(sql, parameters).CInt(0, false) > 0;
        }

        public bool UpdateDomain(string bindost, long id)
        {
            var sql = "update ProductUser set BindHost=@BindHost where Id=@Id";
            IDataParameter[] parameters ={
                new SqlParameter("@Id",SqlDbType.NVarChar,32) {Value = id},
                new SqlParameter("@BindHost",SqlDbType.VarChar,32) {Value =  bindost}
                };
            return DataBaseManager.MainDb().ExecuteNonQuery(sql, parameters).CInt(0, false) > 0;
        }

        public bool UpdateSendRedPackPassword(string password, long id)
        {
            var sql = "update ProductUser set SendRedPackPassword=@SendRedPackPassword where Id=@Id";
            IDataParameter[] parameters ={
                new SqlParameter("@Id",SqlDbType.NVarChar,32) {Value = id},
                new SqlParameter("@SendRedPackPassword",SqlDbType.VarChar,32) {Value =  password}
                };
            return DataBaseManager.MainDb().ExecuteNonQuery(sql, parameters).CInt(0, false) > 0;
        }
    }
}
