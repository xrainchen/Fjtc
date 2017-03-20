using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using Fjtc.DbHelper;
using Fjtc.Model;
using Fjtc.Model.Entity;
using RPoney;
using RPoney.Data;
using RPoney.Log;

namespace Fjtc.DAL
{
    public class CMSRoleDal
    {
        public bool Add(CMSRole entity)
        {
            try
            {
                var insertSql = @"INSERT INTO CMSRole(Name,CreatedBy,CreatedTime) Values(@Name, @CreatedBy, @CreatedTime)";
                IDataParameter[] parameters =
                {
                    new SqlParameter("@Name", SqlDbType.NVarChar, 32) {Value = entity.Name},
                    new SqlParameter("@CreatedBy", SqlDbType.VarChar, 32) {Value = entity.CreatedBy},
                    new SqlParameter("@CreatedTime", SqlDbType.DateTime) {Value = DateTime.Now}
                };
                return DataBaseManager.CmsDb().ExecuteNonQuery(insertSql, parameters).CInt(0, false) > 0;
            }
            catch (Exception ex)
            {
                LoggerManager.Error(GetType().Name, "添加用户异常", ex);
                return false;
            }
        }

        public bool Update(CMSRole entity)
        {
            try
            {
                var insertSql = @"Update CMSRole Set Name=@Name Where Id=@Id";
                IDataParameter[] parameters =
                {
                    new SqlParameter("@Name", SqlDbType.NVarChar, 32) {Value = entity.Name},
                    new SqlParameter("@Id", SqlDbType.BigInt) {Value = entity.Id},
                };
                return DataBaseManager.CmsDb().ExecuteNonQuery(insertSql, parameters).CInt(0, false) > 0;
            }
            catch (Exception ex)
            {
                LoggerManager.Error(GetType().Name, "添加用户异常", ex);
                return false;
            }
        }

        public IList<CMSRole> GetList(SearchParameter searchParameter)
        {
            try
            {
                var tbname = " CMSRole with(nolock) ";
                var filter = " *";
                var orderField = " CreatedTime ";
                var where = "";
                var paramlist = new List<SqlParameter>();
                if (!string.IsNullOrWhiteSpace(searchParameter.Name))
                {
                    where += " and Name like @Name";
                    paramlist.Add(new SqlParameter("@Name", SqlDbType.NVarChar, 32)
                    {
                        Value = $"'%{searchParameter.Name}%'"
                    });
                }
                var pageSql = DataBaseManager.GetPageString(tbname, filter, orderField, where, searchParameter.Page, searchParameter.PageSize);
                var countSql = DataBaseManager.GetCountString(tbname, where);
                searchParameter.Count = int.Parse(DataBaseManager.MainDb().ExecuteScalar(countSql, paramlist.ToArray()).ToString());
                return ModelConvertHelper<CMSRole>.ToModels(DataBaseManager.CmsDb().ExecuteFillDataTable(pageSql, paramlist.ToArray()));

            }
            catch (Exception ex)
            {
                LoggerManager.Error(GetType().Name, "获取用户列表异常", ex);
                return Enumerable.Empty<CMSRole>().ToList();
            }
        }

        public bool Delete(CMSRole entity)
        {
            try
            {
                var insertSql = @"Delete CMSRole Where id=@id";
                IDataParameter[] parameters =
                {
                    new SqlParameter("@id", SqlDbType.BigInt) {Value = entity.Id}
                };
                return DataBaseManager.CmsDb().ExecuteNonQuery(insertSql, parameters).CInt(0, false) > 0;
            }
            catch (Exception ex)
            {
                LoggerManager.Error(GetType().Name, "删除角色异常", ex);
                return false;
            }
        }

        public CMSRole Get(long id)
        {
            try
            {
                var insertSql = @"Select * from  CMSRole with(nolock) Where id=@Id";
                IDataParameter[] parameters =
                {
                    new SqlParameter("@Id", SqlDbType.BigInt) {Value = id}
                };
                return ModelConvertHelper<CMSRole>.ToModel(DataBaseManager.CmsDb().ExecuteFillDataTable(insertSql, parameters));
            }
            catch (Exception ex)
            {
                LoggerManager.Error(GetType().Name, "删除角色异常", ex);
                return null;
            }
        }
    }
}
