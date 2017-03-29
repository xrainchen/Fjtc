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
    /// <summary>
    ///红包日志
    /// </summary>
    public partial class MpWeiXinRedPackLogDal : IdentityDal<MpWeiXinRedPackLogEntity>
    {
        public override MpWeiXinRedPackLogEntity Get(long id)
        {
            var description = "根据id查询红包日志";
            try
            {
                var sql = @"SELECT * FROM  MpWeiXinRedPackLog WITH(NOLOCK) WHERE Id=@Id";
                IDataParameter[] parameters ={
                new SqlParameter("@Id", SqlDbType.BigInt,8) {Value = id},
                };
                LoggerManager.Debug(GetType().Name, $"{description}", $"sql:{sql}{Environment.NewLine}参数:id={id}");
                return RPoney.Data.ModelConvertHelper<MpWeiXinRedPackLogEntity>.ToModel(DataBaseManager.MainDb().ExecuteFillDataTable(sql, parameters));
            }
            catch (Exception ex)
            {
                LoggerManager.Error(GetType().Name, $"{description}异常", ex);
                return null;
            }
        }
        public override bool Delete(long id)
        {
            var description = "根据id删除红包日志";
            try
            {
                var sql = @"DELETE MpWeiXinRedPackLog WHERE Id=@Id";
                IDataParameter[] parameters ={
                new SqlParameter("@Id", SqlDbType.BigInt,8) {Value = id},
                };
                LoggerManager.Debug(GetType().Name, $"{description}", $"sql:{sql}{Environment.NewLine}参数:id={id}");
                return DataBaseManager.MainDb().ExecuteNonQuery(sql, parameters).CInt(0, false) > 0;
            }
            catch (Exception ex)
            {
                LoggerManager.Error(GetType().Name, $"{description}异常", ex);
                return false;
            }
        }
        public override bool Exist(long id)
        {
            var description = "检查红包日志是否存在";
            try
            {
                var sql = @"IF EXISTS(SELECT Id FROM MpWeiXinRedPackLog with(nolock) WHERE ID=@Id) BEGIN SELECT 1 END ELSE BEGIN SELECT 0 END";
                IDataParameter[] parameters ={
                new SqlParameter("@Id", SqlDbType.BigInt,8) {Value = id},
                };
                LoggerManager.Debug(GetType().Name, $"{description},sql:{sql}{Environment.NewLine}参数:id={id}");
                return DataBaseManager.MainDb().ExecuteScalar(sql, parameters).CInt(0, false) > 0;
            }
            catch (Exception ex)
            {
                LoggerManager.Error(GetType().Name, $"{description}异常", ex);
                return false;
            }
        }

        public override bool Add(MpWeiXinRedPackLogEntity entity)
        {
            var description = "添加红包日志";
            try
            {
                var sql = @"INSERT INTO MpWeiXinRedPackLog(ProductUserId,OpenId,RedPackAmount,CreatedTime,Remark,CreatedBy) VALUES(@ProductUserId,@OpenId,@RedPackAmount,@CreatedTime,@Remark,@CreatedBy);";
                IDataParameter[] parameters = {
                        new SqlParameter("@ProductUserId", SqlDbType.BigInt,8) {Value = entity.ProductUserId},
                                    new SqlParameter("@OpenId", SqlDbType.BigInt,8) {Value = entity.OpenId},
                                    new SqlParameter("@RedPackAmount", SqlDbType.Decimal,9) {Value = entity.RedPackAmount},
                                    new SqlParameter("@CreatedTime", SqlDbType.DateTime) {Value = entity.CreatedTime},
                                    new SqlParameter("@Remark", SqlDbType.NVarChar,256) {Value = entity.Remark},
                                    new SqlParameter("@CreatedBy", SqlDbType.NVarChar,64)
            };
                LoggerManager.Debug(GetType().Name, $"{description},sql:{sql}{Environment.NewLine}参数:{entity.SerializeToJSON()}");
                return DataBaseManager.MainDb().ExecuteNonQuery(sql, parameters).CInt(0, false) > 0;
            }
            catch (Exception ex)
            {
                LoggerManager.Error(GetType().Name, $"{description}异常", ex);
                return false;
            }
        }
        public override bool Update(MpWeiXinRedPackLogEntity entity)
        {
            var description = "更新红包日志";
            try
            {
                var sql = @"UPDATE MpWeiXinRedPackLog  SET                                                 
            ProductUserId=@ProductUserId,                                     
            OpenId=@OpenId,                                     
            RedPackAmount=@RedPackAmount,                                     
            CreatedTime=@CreatedTime,                                     
            Remark=@Remark,                                     
            CreatedBy=@CreatedBy             			
			WHERE Id=@Id;
                ";
                IDataParameter[] parameters = {
                        new SqlParameter("@Id", SqlDbType.BigInt,8) {Value = entity.Id},
                                    new SqlParameter("@ProductUserId", SqlDbType.BigInt,8) {Value = entity.ProductUserId},
                                    new SqlParameter("@OpenId", SqlDbType.BigInt,8) {Value = entity.OpenId},
                                    new SqlParameter("@RedPackAmount", SqlDbType.Decimal,9) {Value = entity.RedPackAmount},
                                    new SqlParameter("@CreatedTime", SqlDbType.DateTime) {Value = entity.CreatedTime},
                                    new SqlParameter("@Remark", SqlDbType.NVarChar,256) {Value = entity.Remark},
                                    new SqlParameter("@CreatedBy", SqlDbType.NVarChar,64)
            };
                LoggerManager.Debug(GetType().Name, $"{description},sql:{sql}{Environment.NewLine}参数:{entity.SerializeToJSON()}");
                return DataBaseManager.MainDb().ExecuteNonQuery(sql, parameters).CInt(0, false) > 0;
            }
            catch (Exception ex)
            {
                LoggerManager.Error(GetType().Name, $"{description}异常", ex);
                return false;
            }
        }
        public override IList<MpWeiXinRedPackLogEntity> Get(MpWeiXinRedPackLogEntity entity)
        {
            throw new System.NotImplementedException();
        }
    }
}

