using System.Collections.Generic;
using Fjtc.DAL;
using Fjtc.Model.Entity;
using Fjtc.Model.ViewModel;

namespace Fjtc.BLL
{
    public class CMSPowerBll
    {
        readonly CMSPowerDal _powerDal = new CMSPowerDal();
        public IList<CMSPowerViewModel> GetAllPower()
        {
            return _powerDal.GetAllPower();
        }

        public CMSPower GetPowerTree()
        {
            return _powerDal.GetPowerTree();
        }
    }
}
