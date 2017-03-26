using System;
using System.Data;
using System.Data.SqlClient;
using Fjtc.DbHelper;
using Fjtc.Model.Entity;
using RPoney.Data;
using RPoney.Log;

namespace Fjtc.DAL.MpWeiXin
{
    public class MpWeiXinAccessSettingDal
    {
        public MpWeiXinAccessSetting GetMpWeiXinAccessSetting(long userId)
        {
            try
            {
                var sqlStr = "select * from MpWeiXinAccessSetting with(nolock) where UserId=@UserId";
                var paramet = new[] {
                    new SqlParameter("@UserId",SqlDbType.BigInt) { Value = userId },
                };
                var model = ModelConvertHelper<MpWeiXinAccessSetting>.ToModel(DataBaseManager.MainDb().ExecuteFillDataTable(sqlStr, paramet));
                return model;
            }
            catch (Exception ex)
            {
                LoggerManager.Debug(GetType().Name, $"获取用户{userId}微信接入配置异常", ex);
                return null;
            }
        }

        public MpWeiXinAccessSetting GetMpWeiXinAccessSetting(string userName)
        {
            try
            {
                var sqlStr = "select mwxas.* from MpWeiXinAccessSetting mwxas with(nolock) left join ProductUser u on u.Id=mwxas.userId where u.LoginName=@LoginName";
                var paramet = new[] {
                    new SqlParameter("@LoginName",SqlDbType.NVarChar,64) { Value = userName },
                };
                var model = ModelConvertHelper<MpWeiXinAccessSetting>.ToModel(DataBaseManager.MainDb().ExecuteFillDataTable(sqlStr, paramet));
                return model;
            }
            catch (Exception ex)
            {
                LoggerManager.Debug(GetType().Name, $"获取用户{userName}微信接入配置异常", ex);
                return null;
            }
        }

        public MpWeiXinAccessSetting GetMpWeiXinAccessSettingByHost(string host)
        {
            try
            {
                var sqlStr = "select mwxas.* from MpWeiXinAccessSetting mwxas with(nolock) left join ProductUser u on u.Id=mwxas.userId where u.BindHost=@BindHost";
                var paramet = new[] {
                    new SqlParameter("@BindHost",SqlDbType.NVarChar,64) { Value = host },
                };
                var model = ModelConvertHelper<MpWeiXinAccessSetting>.ToModel(DataBaseManager.MainDb().ExecuteFillDataTable(sqlStr, paramet));
                return model;
            }
            catch (Exception ex)
            {
                LoggerManager.Debug(GetType().Name, $"获取绑定{host}域名接入配置异常", ex);
                return null;
            }
        }


        public bool AddOrUpdate(MpWeiXinAccessSetting entity)
        {
            try
            {
                var sqlStr = @"IF (SELECT COUNT(1) FROM MpWeiXinAccessSetting with(nolock) WHERE UserId=@UserId)=0
                            BEGIN
                                   INSERT INTO MpWeiXinAccessSetting(UserId,AppId,AppSecret,Token,MachId,ApiKey)
                                   Values(@UserId,@AppId,@AppSecret,@Token,@MachId,@ApiKey)
                            END
                            ELSE
                            BEGIN
                                UPDATE MpWeiXinAccessSetting 
                                SET AppId=@AppId,AppSecret=@AppSecret,Token=@Token,MachId=@MachId,ApiKey=@ApiKey  
                                Where UserId=@UserId
                            END";
                var paramet = new[] {
                    new SqlParameter("@UserId",SqlDbType.BigInt) { Value = entity.UserId },
                    new SqlParameter("@AppSecret",SqlDbType.VarChar,128) { Value = entity.AppSecret },
                    new SqlParameter("@AppId",SqlDbType.VarChar,128) { Value = entity.AppId },
                    new SqlParameter("@Token",SqlDbType.NVarChar,128) { Value = entity.Token },
                    new SqlParameter("@MachId",SqlDbType.VarChar,128) { Value = entity.MachId },
                    new SqlParameter("@ApiKey",SqlDbType.NVarChar,128) { Value = entity.ApiKey },
                };
                return DataBaseManager.MainDb().ExecuteNonQuery(sqlStr, paramet) > 0;
            }
            catch (Exception ex)
            {
                LoggerManager.Debug(GetType().Name, "保存微信接入配置异常", ex);
                return false;
            }
        }
    }
}
