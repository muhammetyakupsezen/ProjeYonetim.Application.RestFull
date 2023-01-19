using ProjeYonetim.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjeYonetim.Business
{
    public interface IKisiFirma
    {
        TResult DoAddPersonToFirm(TblKisiFirma tblKisiFirma);
        TResult DoDeletePersonInFirm(Guid PersonGuid, int FirmaId);
        TResult DoGetPersonFirm(int FirmaId);
        TResult DoGetAllPersonToFirm();
      
    }
    
}
