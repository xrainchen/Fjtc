using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using Fjtc.DbHelper;
using Fjtc.Model.Entity;
using RPoney;
using RPoney.Log;
using Fjtc.Model;

namespace Fjtc.DAL.MpWeiXin
{
    public class MpWeiXinUserDal
    {
        public IList<MpWeiXinUser> GetList(SearchParameter serachParameter)
        {
            var tbname = "MpWeiXinUser with(nolock)";
            var filter = "*";
            var orderBy = "Id";
            var where = "";
            var parameters = new List<SqlParameter>();
            var pageSql = DataBaseManager.GetPageString(tbname, filter, orderBy, where,
                serachParameter.Page, serachParameter.PageSize);
            var pageCount = DataBaseManager.GetCountString(tbname, where);
            serachParameter.Count = DataBaseManager.MainDb()
                .ExecuteScalar(pageCount, parameters.ToArray())
                .CInt(0, false);
            return
                RPoney.Data.ModelConvertHelper<MpWeiXinUser>.ToModels(
                    DataBaseManager.MainDb().ExecuteFillDataTable(pageSql, parameters.ToArray()));
        }
        public bool Save(MpWeiXinUser entity)
        {
            try
            {
                var sql = $@"IF(SELECT COUNT(1) FROM MpWeiXinUser WITH(NOLOCK) WHERE OpenId=@OpenId)>0
                            BEGIN
                                UPDATE MpWeiXinUser SET 
                                Subscribe=@Subscribe
                                {(string.IsNullOrWhiteSpace(entity.NickName) ? "" : ",NickName=@NickName")}
                                {(entity.Sex > 0 ? "" : ",Sex=@Sex")}
                                {(string.IsNullOrWhiteSpace(entity.City) ? "" : ",City=@City")}
                                {(string.IsNullOrWhiteSpace(entity.Country) ? "" : ",Country=@Country")}
                                {(string.IsNullOrWhiteSpace(entity.Province) ? "" : ",Province=@Province")}
                                {(string.IsNullOrWhiteSpace(entity.Language) ? "" : ",Language=@Language")}
                                {(string.IsNullOrWhiteSpace(entity.HeadImgUrl) ? "" : ",HeadImgUrl=@HeadImgUrl")}
                                {(entity.SubscribeTime > 0 ? "" : ",SubscribeTime=@SubscribeTime")}
                                {(string.IsNullOrWhiteSpace(entity.UnionId) ? "" : ",UnionId=@UnionId")}
                                {(string.IsNullOrWhiteSpace(entity.Remark) ? "" : ",Remark=@Remark")}
                                {(entity.GroupId > 0 ? "" : ",GroupId=@GroupId")}
                                WHERE OpenId=@OpenId
                            END
                            ELSE
                            BEGIN
                                INSERT INTO MpWeiXinUser(OpenId,Subscribe,NickName,Sex,City,Country,Province,Language,HeadImgUrl,SubscribeTime,UnionId,Remark,GroupId)
                                VALUES(@OpenId,@Subscribe,@NickName,@Sex,@City,@Country,@Province,@Language,@HeadImgUrl,@SubscribeTime,@UnionId,@Remark,@GroupId)
                            END
                            ";
                IDataParameter[] parameters ={
                new SqlParameter("@OpenId",SqlDbType.VarChar,64) {Value =  entity.OpenId},
                new SqlParameter("@Subscribe", SqlDbType.Int) {Value=entity.Subscribe},
                new SqlParameter("@NickName",SqlDbType.NVarChar,64) {Value =  entity.NickName},
                new SqlParameter("@Sex", SqlDbType.Int) {Value = entity.Sex},
                new SqlParameter("@City",SqlDbType.NVarChar,64) {Value =  entity.City},
                new SqlParameter("@Country", SqlDbType.NVarChar,64) {Value=entity.Country},
                new SqlParameter("@Province",SqlDbType.NVarChar,64) {Value =  entity.Province},
                new SqlParameter("@Language",SqlDbType.NVarChar,64) {Value =  entity.Language},
                new SqlParameter("@HeadImgUrl",SqlDbType.NVarChar,512) {Value =  entity.HeadImgUrl},
                new SqlParameter("@SubscribeTime",SqlDbType.BigInt) {Value =  entity.SubscribeTime},
                new SqlParameter("@UnionId",SqlDbType.VarChar,64) {Value =  entity.UnionId},
                new SqlParameter("@Remark",SqlDbType.NVarChar,512) {Value =  entity.Remark},
                new SqlParameter("@GroupId",SqlDbType.Int) {Value =  entity.GroupId},
                };
                return DataBaseManager.MainDb().ExecuteNonQuery(sql, parameters).CInt(0, false) > 0;
            }
            catch (Exception ex)
            {
                LoggerManager.Error(GetType().Name, "添加菜单异常", ex);
                return false;
            }
        }
    }
}
