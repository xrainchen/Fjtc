using System.Collections.Generic;
using System.Linq;
using Fjtc.Model.Entity;
using Fjtc.Model.ViewModel;
using ServiceStack.Common.Extensions;

namespace Fjtc.DAL
{
    public class CMSPowerDal
    {

        public CMSPower GetPowerTree() => PowerSource;
        public IList<CMSPowerViewModel> GetAllPower()
        {
            var list = new List<CMSPowerViewModel>();
            if (PowerSource.Children != null && PowerSource.Children.Any())
            {
                var root = PowerSource.Children.Select(p => new CMSPowerViewModel()
                {
                    Id = p.Id,
                    Name = p.Name,
                    ParentId = p.ParentId,
                    PowerCode = p.PowerCode,
                    Level = 1,
                    IsExtend = true,
                    NoChild = p.Children == null,
                });
                list.AddRange(root);
                foreach (var child in PowerSource.Children)
                {
                    if (child.Children != null && child.Children.Any())
                    {
                        var second = child.Children.Select(p => new CMSPowerViewModel()
                        {
                            Id = p.Id,
                            Name = p.Name,
                            ParentId = p.ParentId,
                            PowerCode = p.PowerCode,
                            Level = 2,
                            IsExtend = true,
                            NoChild = p.Children == null,
                        });
                        list.AddRange(second);
                        foreach (var thirdChild in PowerSource.Children)
                        {
                            if (thirdChild.Children != null && thirdChild.Children.Any())
                            {
                                var third = child.Children.Select(p => new CMSPowerViewModel()
                                {
                                    Id = p.Id,
                                    Name = p.Name,
                                    ParentId = p.ParentId,
                                    PowerCode = p.PowerCode,
                                    Level = 3,
                                    IsExtend = true,
                                    NoChild = p.Children == null,
                                });
                                list.AddRange(third);

                            }
                        }
                    }
                }
            }
            return list;
        }

        #region 权限码数据源
        /// <summary>
        /// 权限码
        /// </summary>
        private static readonly CMSPower PowerSource = new CMSPower()
        {
            Id = 0,
            Name = "权限节点",
            ParentId = -1,
            PowerCode = "-1",
            Children = new List<CMSPower>()
                {
                    new CMSPower()
                    {
                        Id=1,Name="系统管理",ParentId =0,PowerCode = "8888001",
                        Children = new List<CMSPower>()
                        {
                            new CMSPower(){
                                Id =11,Name="用户管理",ParentId = 1,PowerCode = "8888001|001",
                                Children =new List<CMSPower>()
                                {
                                    new CMSPower() {Id =111,Name="添加",ParentId = 11,PowerCode = "8888001|001|001", },
                                    new CMSPower() {Id =112,Name="删除",ParentId = 11,PowerCode = "8888001|001|002", },
                                    new CMSPower() {Id =113,Name="修改",ParentId = 11,PowerCode = "8888001|001|003",}
                                }
                            },
                            new CMSPower()
                            {
                                Id=12,Name="菜单管理",ParentId = 1,PowerCode = "8888001|002",
                                Children =new List<CMSPower>()
                                {
                                    new CMSPower() {Id =111,Name="添加",ParentId = 12,PowerCode = "8888001|002|001", },
                                    new CMSPower() {Id =112,Name="删除",ParentId = 12,PowerCode = "8888001|002|002",},
                                    new CMSPower() {Id =113,Name="修改",ParentId = 12,PowerCode = "8888001|002|003",}
                                }
                            },
                            new CMSPower()
                            {
                                Id=13,Name="角色管理",ParentId = 1,PowerCode = "8888001|003",
                                Children =new List<CMSPower>()
                                {
                                    new CMSPower() {Id =111,Name="添加",ParentId = 13,PowerCode = "8888001|003|001", },
                                    new CMSPower() {Id =112,Name="删除",ParentId = 13,PowerCode = "8888001|003|002",},
                                    new CMSPower() {Id =113,Name="修改",ParentId = 13,PowerCode = "8888001|003|003",}
                                }
                            },
                            new CMSPower(){Id=14,Name="T-SQL查询",ParentId =1,PowerCode = "8888001|004",},

                        }
                    }
                }
        };
        #endregion
    }
}
