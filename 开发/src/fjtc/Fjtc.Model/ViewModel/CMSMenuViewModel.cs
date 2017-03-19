using System;
using System.Collections.Generic;

namespace Fjtc.Model.ViewModel
{
    public class CMSMenuViewModel: BaseViewModel
    {
        /// <summary>
        /// 主键
        /// </summary>
        public long Id { get; set; }
        /// <summary>
        /// 名称
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// 地址
        /// </summary>
        public string Url { get; set; }
        /// <summary>
        /// 上级
        /// </summary>
        public long ParentId { get; set; }
        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime CreatedTime { get; set; }
        /// <summary>
        /// 创建人
        /// </summary>
        public string CreatedBy { get; set; }
        /// <summary>
        /// 权限值
        /// </summary>
        public string PowerValue { get; set; }

        /// <summary>
        /// 序号
        /// </summary>
        public int Sort { get; set; }

        /// <summary>
        /// 原始路径
        /// </summary>
        public string OrganPath { get; set; }

        public string ParentOrganPath { get; set; }

        /// <summary>
        /// 菜单类型枚举
        /// </summary>
        public CmsMenuTypeEnum MenuType { get; set; }


        public IList<Entity.CMSMenu> ParentMenus { get; set; }
    }
}
