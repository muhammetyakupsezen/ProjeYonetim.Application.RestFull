using ProjeYonetim.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjeYonetim.Business
{
    public class BaseKisiFirma : IKisiFirma
    {
        public TResult DoAddPersonToFirm(TblKisiFirma tblKisiFirma)
        {
            throw new NotImplementedException();
        }

        public TResult DoDeletePersonInFirm(Guid PersonGuid, int FirmaId)
        {
            throw new NotImplementedException();
        }

        public TResult DoGetAllPersonToFirm()
        {
            throw new NotImplementedException();
        }

        public TResult DoGetPersonFirm(int FirmaId)
        {
            throw new NotImplementedException();
        }
    }
}
