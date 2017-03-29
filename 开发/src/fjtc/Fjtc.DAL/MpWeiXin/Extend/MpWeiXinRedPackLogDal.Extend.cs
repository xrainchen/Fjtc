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
            var tbname = "MpWeiXinRedPackLogEntity with(nolock)";
            var filter = "*";
            var orderBy = "CreatedTime desc";
            var where = "";
            var parameters = new List<SqlParameter>();
            
            var pageSql = DataBaseManager.GetPageString(tbname, filter, orderBy, where, search.Page, search.PageSize);
            var pageCount = DataBaseManager.GetCountString(tbname, where);
            serachParameter.Count = DataBaseManager.MainDb().ExecuteScalar(pageCount, parameters.ToArray()).CInt(0, false);
            return
                RPoney.Data.ModelConvertHelper<MpWeiXinRedPackLogViewModel>.ToModels(
                    DataBaseManager.MainDb().ExecuteFillDataTable(pageSql, parameters.ToArray()));
        }
    }
}
