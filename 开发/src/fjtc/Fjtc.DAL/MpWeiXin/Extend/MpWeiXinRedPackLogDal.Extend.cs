using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using Fjtc.DbHelper;
using Fjtc.Model;
using Fjtc.Model.Entity;
using Fjtc.Model.SearchModel;
using Fjtc.Model.ViewModel;
using RPoney;

namespace Fjtc.DAL.MpWeiXin
{
    public partial class MpWeiXinRedPackLogDal
    {
        public IList<MpWeiXinRedPackLogViewModel> GetRedPackLog(SearchParameter serachParameter)
        {
            var search = serachParameter as MpWeiXinRedPackLogSearchParameter;
            var tbname = "[MpWeiXinRedPackLog] mwxrpl with(nolock) left join MpWeiXinUser mwxu with(nolock) on mwxrpl.OpenId= mwxu.OpenId";
            var filter = "mwxrpl.*,mwxu.NickName as MpUserNick";
            var orderBy = "CreatedTime desc";
            var where = "";
            var parameters = new List<SqlParameter>();
            if (search.ProductUserId.HasValue)
            {
                where += " and mwxrpl.ProductUserId=@ProductUserId";
                parameters.Add(new SqlParameter("@ProductUserId", SqlDbType.BigInt,8) { Value=search.ProductUserId });
            }
            if (!string.IsNullOrWhiteSpace(search.MpUserNick))
            {
                where += " and mwxu.NickName like @MpUserNick or mwxu.Remark like @MpUserNick";
                parameters.Add(new SqlParameter("@MpUserNick", SqlDbType.NVarChar,64) { Value = $"%{search.MpUserNick}%" });
            }
            if (search.BeginSearchTime.HasValue)
            {
                where += " and mwxrpl.CreatedTime>=@BeginSearchTime";
                parameters.Add(new SqlParameter("@BeginSearchTime", SqlDbType.DateTime) { Value = search.BeginSearchTime });
            }
            if (search.EndSearchTime.HasValue)
            {
                where += " and mwxrpl.CreatedTime<=@EndSearchTime";
                parameters.Add(new SqlParameter("@EndSearchTime", SqlDbType.DateTime) { Value = search.EndSearchTime });
            }
            var pageSql = DataBaseManager.GetPageString(tbname, filter, orderBy, where, search.Page, search.PageSize);
            var pageCount = DataBaseManager.GetCountString(tbname, where);
            serachParameter.Count = DataBaseManager.MainDb().ExecuteScalar(pageCount, parameters.ToArray()).CInt(0, false);
            return
                RPoney.Data.ModelConvertHelper<MpWeiXinRedPackLogViewModel>.ToModels(
                    DataBaseManager.MainDb().ExecuteFillDataTable(pageSql, parameters.ToArray()));
        }
    }
}
