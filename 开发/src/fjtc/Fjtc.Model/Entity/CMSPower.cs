using System;
using System.Collections.Generic;

namespace Fjtc.Model.Entity
{
    /// <summary>
    /// 权限
    /// </summary>
    [Serializable]
    public class CMSPower
    {
        /// <summary>
        /// 主键
        /// </summary>
        public long Id { get; set; }
        /// <summary>
        /// 权限名称
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// 权限码
        /// </summary>
        public string PowerCode { get; set; }
        /// <summary>
        /// 父级
        /// </summary>
        public int ParentId { get; set; }
        /// <summary>
        /// 子节点
        /// </summary>
        public IList<CMSPower> Children { get; set; }
    }
}
