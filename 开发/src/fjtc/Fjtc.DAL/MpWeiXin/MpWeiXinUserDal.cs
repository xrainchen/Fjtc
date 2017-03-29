using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using Fjtc.DbHelper;
using Fjtc.Model.Entity;
using RPoney;
using RPoney.Log;
using Fjtc.Model;
using Fjtc.Model.SearchModel;

namespace Fjtc.DAL.MpWeiXin
{
    public class MpWeiXinUserDal
    {
        public IList<MpWeiXinUser> GetList(SearchParameter serachParameter)
        {
            var search = serachParameter as MpWeiXinUserSerachParameter;
            var tbname = "MpWeiXinUser with(nolock)";
            var filter = "*";
            var orderBy = "SubscribeTime desc";
            var where = "";
            var parameters = new List<SqlParameter>();
            if (search.ProductUserId.HasValue)
            {
                where += " and ProductUserId=@ProductUserId";
                parameters.Add(new SqlParameter("@ProductUserId", SqlDbType.BigInt) { Value = search.ProductUserId });
            }
            if (!string.IsNullOrWhiteSpace(search.Country))
            {
                where += " and Country=@Country";
                parameters.Add(new SqlParameter("@Country", SqlDbType.NVarChar) { Value = search.Country });
            }
            if (!string.IsNullOrWhiteSpace(search.Province))
            {
                where += " and Province=@Province";
                parameters.Add(new SqlParameter("@Province", SqlDbType.NVarChar) { Value = search.Province });
            }
            if (!string.IsNullOrWhiteSpace(search.City))
            {
                where += " and City=@City";
                parameters.Add(new SqlParameter("@City", SqlDbType.NVarChar) { Value = search.City });
            }
            if (!string.IsNullOrWhiteSpace(search.Name))
            {
                where += " and NickName like @NickName";
                parameters.Add(new SqlParameter("@NickName", SqlDbType.NVarChar) { Value = $"%{search.Name}%" });
            }
            if (search.BeginSearchTime.HasValue)
            {
                where += " and DATEADD(S,SubscribeTime + 8 * 3600,'1970-01-01 00:00:00')>=@BeginSearchTime";
                parameters.Add(new SqlParameter("@BeginSearchTime", SqlDbType.DateTime) { Value = search.BeginSearchTime });
            }
            if (search.EndSearchTime.HasValue)
            {
                where += " and DATEADD(S,SubscribeTime + 8 * 3600,'1970-01-01 00:00:00')<=@EndSearchTime";
                parameters.Add(new SqlParameter("@EndSearchTime", SqlDbType.DateTime) { Value = search.EndSearchTime });
            }
            var pageSql = DataBaseManager.GetPageString(tbname, filter, orderBy, where, search.Page, search.PageSize);
            var pageCount = DataBaseManager.GetCountString(tbname, where);
            serachParameter.Count = DataBaseManager.MainDb().ExecuteScalar(pageCount, parameters.ToArray()).CInt(0, false);
            if (search.IsAll)
            {
                pageSql = DataBaseManager.GetPageString(tbname, filter, orderBy, where, 1, serachParameter.Count);
            }
            return
                RPoney.Data.ModelConvertHelper<MpWeiXinUser>.ToModels(
                    DataBaseManager.MainDb().ExecuteFillDataTable(pageSql, parameters.ToArray()));
        }
        public bool Save(MpWeiXinUser entity)
        {
            try
            {
                var sql = $@"IF(SELECT EXISTS(SELECT Id FROM MpWeiXinUser WITH(NOLOCK) WHERE OpenId=@OpenId))>0
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
                                WHERE OpenId=@OpenId and ProductUserId=@ProductUserId
                            END
                            ELSE
                            BEGIN
                                INSERT INTO MpWeiXinUser(OpenId,Subscribe,NickName,Sex,City,Country,Province,Language,HeadImgUrl,SubscribeTime,UnionId,Remark,GroupId,ProductUserId)
                                VALUES(@OpenId,@Subscribe,@NickName,@Sex,@City,@Country,@Province,@Language,@HeadImgUrl,@SubscribeTime,@UnionId,@Remark,@GroupId,@ProductUserId)
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
                new SqlParameter("@ProductUserId",SqlDbType.BigInt) {Value =  entity.ProductUserId},
                };
                return DataBaseManager.MainDb().ExecuteNonQuery(sql, parameters).CInt(0, false) > 0;
            }
            catch (Exception ex)
            {
                LoggerManager.Error(GetType().Name, "添加菜单异常", ex);
                return false;
            }
        }

        public MpWeiXinUser Get(long id)
        {
            var sql = "select * from MpWeiXinUser with(nolock) where Id=@Id";
            var parameters = new List<SqlParameter>();
            parameters.Add(new SqlParameter("@Id", SqlDbType.BigInt) { Value = id });
            return RPoney.Data.ModelConvertHelper<MpWeiXinUser>.ToModel(DataBaseManager.MainDb().ExecuteFillDataTable(sql, parameters.ToArray()));
        }
        public MpWeiXinUser GetBelongMpProductUser(long id, long productUserId)
        {
            var sql = "select * from MpWeiXinUser with(nolock) where Id=@Id and ProductUserId=@ProductUserId";
            var parameters = new List<SqlParameter>();
            parameters.Add(new SqlParameter("@Id", SqlDbType.BigInt) { Value = id });
            parameters.Add(new SqlParameter("@ProductUserId", SqlDbType.BigInt) { Value = productUserId });
            return RPoney.Data.ModelConvertHelper<MpWeiXinUser>.ToModel(DataBaseManager.MainDb().ExecuteFillDataTable(sql, parameters.ToArray()));
        }
    }
}
