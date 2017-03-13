using System.Data;
using System.Data.SqlClient;
using Fjtc.DbHelper;
using Fjtc.Model.Entity;
using RPoney.Data;

namespace Fjtc.DAL.MpWeiXin
{
    public class MpWeiXinAccessSettingDal
    {
        public MpWeiXinAccessSetting GetMpWeiXinAccessSetting(long userId)
        {
            var sqlStr = "select * from MpWeiXinAccessSetting with(nolock) where UserId=@UserId";
            var paramet = new[] {
                    new SqlParameter("@UserId",SqlDbType.BigInt) { Value = userId },
                };
            var model = ModelConvertHelper<MpWeiXinAccessSetting>.ToModel(DataBaseManager.MainDb().ExecuteFillDataTable(sqlStr, paramet));
            return model;
        }

        public bool AddOrUpdate(MpWeiXinAccessSetting entity)
        {
            var sqlStr = $@"IF (SELECT COUNT(1) FROM MpWeiXinAccessSetting with(nolock) WHERE UserId=@UserId)=0
                            BEGIN
                                   INSERT INTO MpWeiXinAccessSetting(UserId,AppId,AppSecret,Token)  Values(@UserId,@AppId,@AppSecret,@Token)
                            END
                            ELSE
                            BEGIN
                                UPDATE MpWeiXinAccessSetting SET AppId=@AppId,AppSecret=@AppSecret,Token=@Token Where UserId=@UserId
                            END";
            var paramet = new[] {
                    new SqlParameter("@UserId",SqlDbType.BigInt) { Value = entity.UserId },
                    new SqlParameter("@AppSecret",SqlDbType.VarChar,128) { Value = entity.AppSecret },
                    new SqlParameter("@AppId",SqlDbType.VarChar,128) { Value = entity.AppId },
                    new SqlParameter("@Token",SqlDbType.NVarChar,128) { Value = entity.Token },
                };
            return DataBaseManager.MainDb().ExecuteNonQuery(sqlStr, paramet) > 0;
        }
    }
}
