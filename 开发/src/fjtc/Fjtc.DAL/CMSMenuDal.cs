using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using Fjtc.DbHelper;
using Fjtc.Model;
using Fjtc.Model.Entity;
using Fjtc.Model.SearchModel;
using RPoney;
using RPoney.Data;
using RPoney.Log;

namespace Fjtc.DAL
{
    public class CMSMenuDal
    {
        public bool Add(CMSMenu entity)
        {
            try
            {
                var insertSql = @"IF (SELECT COUNT(1) FROM CMSMenu with(nolock) WHERE PowerValue=@PowerValue) = 0
                            BEGIN
                                INSERT INTO CMSMenu(Name,Url,ParentId,CreatedTime,CreatedBy,PowerValue,Sort,OrganPath,MenuType)  Values(@Name,@Url,@ParentId,@CreatedTime,@CreatedBy,@PowerValue,@Sort,@OrganPath,@MenuType)
                                SELECT 1;
                            END
                            ELSE
                            BEGIN
                                SELECT 0;
                            END";
                IDataParameter[] parameters ={
                new SqlParameter("@Name",SqlDbType.NVarChar,32) {Value =  entity.Name},
                new SqlParameter("@Url", SqlDbType.VarChar,1024) {Value=entity.Url},
                new SqlParameter("@ParentId",SqlDbType.BigInt) {Value =  entity.ParentId},
                new SqlParameter("@CreatedTime", SqlDbType.DateTime) {Value = DateTime.Now},
                new SqlParameter("@CreatedBy",SqlDbType.NVarChar,64) {Value =  entity.CreatedBy},
                new SqlParameter("@PowerValue", SqlDbType.NVarChar,128) {Value=entity.PowerValue},
                new SqlParameter("@Sort",SqlDbType.BigInt) {Value =  entity.Sort},
                new SqlParameter("@OrganPath",SqlDbType.NVarChar,256) {Value =  entity.OrganPath},
                new SqlParameter("@MenuType",SqlDbType.Int) {Value =  (int)entity.MenuType}
                };
                return DataBaseManager.CmsDb().ExecuteScalar(insertSql, parameters).CInt(0, false) > 0;
            }
            catch (Exception ex)
            {
                LoggerManager.Error(GetType().Name, "添加菜单异常", ex);
                return false;
            }
        }

        public bool Update(CMSMenu entity)
        {
            try
            {
                var insertSql = @"UPDATE CMSMenu SET Name=@Name,Url=@Url,ParentId=@ParentId,CreatedBy=@CreatedBy,PowerValue=@PowerValue,Sort=@Sort,OrganPath=@OrganPath,MenuType=@MenuType WHERE Id=@Id";
                IDataParameter[] parameters ={
                    new SqlParameter("@Id",SqlDbType.BigInt) {Value =  entity.Id},
                    new SqlParameter("@Name",SqlDbType.NVarChar,32) {Value =  entity.Name},
                    new SqlParameter("@Url", SqlDbType.VarChar,1024) {Value=entity.Url},
                    new SqlParameter("@ParentId",SqlDbType.BigInt) {Value =  entity.ParentId},
                    new SqlParameter("@CreatedBy",SqlDbType.NVarChar,64) {Value =  entity.CreatedBy},
                    new SqlParameter("@PowerValue", SqlDbType.NVarChar,128) {Value=entity.PowerValue},
                    new SqlParameter("@Sort",SqlDbType.BigInt) {Value =  entity.Sort},
                    new SqlParameter("@OrganPath",SqlDbType.NVarChar,256) {Value =  entity.OrganPath},
                    new SqlParameter("@MenuType",SqlDbType.Int) {Value =  (int)entity.MenuType}
                };
                return DataBaseManager.CmsDb().ExecuteNonQuery(insertSql, parameters).CInt(0, false) > 0;
            }
            catch (Exception ex)
            {
                LoggerManager.Error(GetType().Name, $"更新菜单{entity.Id}异常", ex);
                return false;
            }
        }

        public IList<CMSMenu> GetList(SearchParameter searchObj)
        {
            try
            {
                var searchParameter = searchObj as CMSMenuSearchParameter;
                var sql = "select * from  CMSMenu with(nolock) where 1=1 ";
                var paramList = new List<SqlParameter>();
                if (searchParameter.ParentId > 0)
                {
                    sql += " and ParentId=@ParentId";
                    paramList.Add(new SqlParameter("@ParentId", SqlDbType.BigInt) { Value = searchParameter.ParentId });
                }
                return ModelConvertHelper<CMSMenu>.ToModels(DataBaseManager.CmsDb().ExecuteFillDataTable(sql, paramList.ToArray()));
            }
            catch (Exception ex)
            {
                LoggerManager.Error(GetType().Name, "获取菜单列表异常", ex);
                return Enumerable.Empty<CMSMenu>().ToList();
            }
        }

        public IList<CMSMenu> GetAllList()
        {
            try
            {
                var sql = "select * from  CMSMenu with(nolock)";
                var paramList = new List<SqlParameter>();
                return ModelConvertHelper<CMSMenu>.ToModels(DataBaseManager.CmsDb().ExecuteFillDataTable(sql, paramList.ToArray()));
            }
            catch (Exception ex)
            {
                LoggerManager.Error(GetType().Name, "获取菜单列表异常", ex);
                return Enumerable.Empty<CMSMenu>().ToList();
            }
        }

        public CMSMenu GetRootMenu()
        {
            try
            {
                var sql = "select * from  CMSMenu with(nolock) where ParentId=-1 ";
                var paramList = new List<SqlParameter>();

                return ModelConvertHelper<CMSMenu>.ToModel(DataBaseManager.CmsDb().ExecuteFillDataTable(sql, paramList.ToArray()));
            }
            catch (Exception ex)
            {
                LoggerManager.Error(GetType().Name, "获取菜单列表异常", ex);
                return null;
            }
        }

        public CMSMenu Get(long id)
        {
            try
            {
                var sql = "select * from  CMSMenu with(nolock) where Id=@Id ";
                var paramList = new List<SqlParameter>();
                paramList.Add(new SqlParameter("@Id", SqlDbType.BigInt) { Value = id });
                return ModelConvertHelper<CMSMenu>.ToModel(DataBaseManager.CmsDb().ExecuteFillDataTable(sql, paramList.ToArray()));
            }
            catch (Exception ex)
            {
                LoggerManager.Error(GetType().Name, "获取菜单列表异常", ex);
                return null;
            }
        }
    }
}
