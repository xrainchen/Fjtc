using System.Collections.Generic;

namespace Fjtc.Model.ViewModel
{
    public class CMSPowerViewModel:BaseViewModel
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
        public IList<CMSPowerViewModel> Children { get; set; }
        /// <summary>
        /// 是否有子节点
        /// </summary>
        public bool NoChild { get; set; }
        /// <summary>
        /// 级别
        /// </summary>
        public int Level { get; set; }
        /// <summary>
        /// 是否展开
        /// </summary>
        public bool IsExtend { get; set; }
    }
}
