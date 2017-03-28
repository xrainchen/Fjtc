using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using Fjtc.DbHelper;
using Fjtc.Model.Entity;
using RPoney;
using RPoney.Log;

namespace Fjtc.DAL.MpWeiXin
{
    public class MpWeiXinRedPackLogDal : IdentityDal<MpWeiXinRedPackLog>
    {
        public override bool Add(MpWeiXinRedPackLog entity)
        {
            try
            {
                var insertSql = @"INSERT INTO MpWeiXinRedPackLog(ProductUserId,RedPackAmount,OpenId,CreatedTime,CreatedBy,Remark)
Values(@ProductUserId,@RedPackAmount,@OpenId,@CreatedTime,@CreatedBy,@Remark)";
                IDataParameter[] parameters ={
                new SqlParameter("@ProductUserId",SqlDbType.BigInt,32) {Value =  entity.ProductUserId},
                new SqlParameter("@RedPackAmount", SqlDbType.Decimal) {Value=entity.RedPackAmount},
                new SqlParameter("@OpenId",SqlDbType.VarChar) {Value =  entity.OpenId},
                new SqlParameter("@CreatedTime", SqlDbType.DateTime) {Value = DateTime.Now},
                new SqlParameter("@CreatedBy",SqlDbType.NVarChar,64) {Value =  entity.CreatedBy},
                new SqlParameter("@Remark",SqlDbType.NVarChar,128) {Value =  entity.Remark},
                };
                return DataBaseManager.CmsDb().ExecuteNonQuery(insertSql, parameters).CInt(0, false) > 0;
            }
            catch (Exception ex)
            {
                LoggerManager.Error(GetType().Name, "添加菜单异常", ex);
                return false;
            }
        }

        public override bool Delete(MpWeiXinRedPackLog entity)
        {
            throw new NotImplementedException();
        }

        public override IList<MpWeiXinRedPackLog> Get(MpWeiXinRedPackLog entity)
        {
            throw new NotImplementedException();
        }

        public override MpWeiXinRedPackLog Get(long id)
        {
            throw new NotImplementedException();
        }

        public override bool Update(MpWeiXinRedPackLog entity)
        {
            throw new NotImplementedException();
        }
    }
}
