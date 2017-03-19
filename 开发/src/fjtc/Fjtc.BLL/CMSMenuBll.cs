
using System.Collections.Generic;
using System.Linq;
using Fjtc.DAL;
using Fjtc.Model;
using Fjtc.Model.Entity;

namespace Fjtc.BLL
{
    public class CMSMenuBll
    {
        private readonly CMSMenuDal _cmsMenuDal = new CMSMenuDal();
        public bool Add(CMSMenu entity)
        {
            return _cmsMenuDal.Add(entity);
        }
        public bool Update(CMSMenu entity)
        {
            return _cmsMenuDal.Update(entity);
        }
        public IList<CMSMenu> GetList(SearchParameter searchObj)
        {
            return _cmsMenuDal.GetList(searchObj);
        }

        public CMSMenu GetRootMenu()
        {
            return _cmsMenuDal.GetRootMenu();
        }

        public IList<CMSMenu> GetAllList()
        {
            return _cmsMenuDal.GetAllList();
        }

        public IList<CMSMenu> GetTreeList()
        {
            var list = new List<CMSMenu>();
            var virtualRoot = GetRootMenu();
            var all = GetAllList();
            var folderMenu = all.Where(p => p.ParentId == virtualRoot.Id).OrderBy(p=>p.Sort);
            if (folderMenu.Any())
            {
                foreach (var folder in folderMenu)
                {
                    var menu = all.Where(p => p.ParentId == folder.Id).OrderBy(p => p.Sort);
                    if (menu.Any())
                    {
                        folder.Children = menu.ToList();
                    }
                    list.Add(folder);
                }
            }
            return list;
        }

        public CMSMenu Get(long id)
        {
            return _cmsMenuDal.Get(id);
        }
    }
}
