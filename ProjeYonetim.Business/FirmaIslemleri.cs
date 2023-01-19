using ProjeYonetim.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjeYonetim.Business
{
    public class FirmaIslemleri : BaseFirmaIslemleri
    {
        public TResult FirmaEkle(TblFirmalar Firma)
        {
            return base.FirmaEkle(Firma);
        }



        public TResult UpdateFirm(int FirmaId, TblFirmalar tblFirma)
        {
           

            return base.UpdateFirm(FirmaId, tblFirma);
        }

        public TResult DeleteFirm(int FirmaId)
        {
            
            return base.DeleteFirm(FirmaId);
        }



        public TResult DoFirmaListele()
        {
            

            return base.DoFirmaListele();
        }


        public TResult DoFirmaArama(string FirmaAdi)
        {
           

            return base.DoFirmaArama(FirmaAdi);
        }


        public TResult DoFirmaDetay(int FirmaId)
        {
            

            return base.DoFirmaDetay(FirmaId);
        }




        public TResult DoPasifFirmaListele()
        {
            

            return base.DoPasifFirmaListele();
        }




    }



}
