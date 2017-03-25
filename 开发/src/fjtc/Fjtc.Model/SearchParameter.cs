using System;
using System.Security.Permissions;

namespace Fjtc.Model
{
    /// <summary>
    /// 搜索条件基类
    /// </summary>
    public class SearchParameter
    {
        #region 分页
        /// <summary>
        /// 页码
        /// </summary>
        public int Page { get; set; }
        /// <summary>
        /// 数量
        /// </summary>
        public int PageSize { get; set; }
        /// <summary>
        /// 总数
        /// </summary>
        public int Count { get; set; }
        /// <summary>
        /// 总页数
        /// </summary>
        public int TotalPage
        {
            get
            {
                if (PageSize < 1) return 1;
                return (Count + PageSize - 1) / PageSize;
            }
        }
        #endregion

        /// <summary>
        /// 排序方式
        /// </summary>
        public string OrderBy { get; set; }
        /// <summary>
        /// 返回数据
        /// </summary>
        public dynamic ReturnList { get; set; }

        /// <summary>
        /// 标签页
        /// </summary>
        public string NavTab { get; set; }


        #region 通用搜索
        /// <summary>
        /// 名称
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 开始搜索时间
        /// </summary>
        public DateTime? BeginSearchTime { get; set; }
        /// <summary>
        /// 结束搜索时间
        /// </summary>
        public DateTime? EndSearchTime { get; set; }
        #endregion
    }
}
