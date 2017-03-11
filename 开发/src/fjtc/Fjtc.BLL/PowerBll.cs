using System.Collections.Generic;
using Fjtc.DAL;
using Fjtc.Model.Entity;
using Fjtc.Model.ViewModel;

namespace Fjtc.BLL
{
    public class PowerBll
    {
        readonly PowerDal _powerDal = new PowerDal();
        public IList<PowerViewModel> GetAllPower()
        {
            return _powerDal.GetAllPower();
        }

        public Power GetPowerTree()
        {
            return _powerDal.GetPowerTree();
        }
    }
}
