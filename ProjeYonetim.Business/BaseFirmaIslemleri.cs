using ProjeYonetim.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjeYonetim.Business
{
    public class BaseFirmaIslemleri
    {
        private DbProjeYonetimEntities2 Database;

        public BaseFirmaIslemleri()
        {
            Database = new DbProjeYonetimEntities2();
        }



        public TResult FirmaEkle(TblFirmalar Firma)
        {
            TResult result = new TResult();
            try
            {
                if (Firma != null)
                {
                    Database.TblFirmalar.Add(Firma);
                    Database.SaveChanges();
                    result.Success = true;
                    result.StatusCode = 200;


                }
            }
            catch (Exception ex)
            {

                result.Success = false;
                result.Message = ex.Message;
                result.StatusCode = -1001;
            }

            return result;
        }



        public TResult UpdateFirm(int FirmaId, TblFirmalar tblFirma)
        {
            TResult result = new TResult();

            try
            {
                var BulunanFirma = (from d in Database.TblFirmalar where d.FirmaId == FirmaId select d).FirstOrDefault();

                if (BulunanFirma == null)
                {
                    result.Message = "Firma bulunamadı";
                }
                else
                {
                    BulunanFirma = tblFirma;
                    Database.TblFirmalar.Add(tblFirma);
                    Database.SaveChanges();
                    result.Success = true;
                    result.StatusCode = 200;
                }
            }
            catch (Exception ex)
            {
                result.Success = false;
                result.Message = ex.Message;
                result.StatusCode = -1001;

            }

            return result;
        }

        public TResult DeleteFirm(int FirmaId)
        {
            TResult result = new TResult();

            try
            {
                var BulunanFirma = (from d in Database.TblFirmalar where d.FirmaId == FirmaId select d).FirstOrDefault();

                if (BulunanFirma == null)
                {
                    result.Message = "Firma bulunamadı";
                }
                else
                {

                    Database.Database.ExecuteSqlCommand("update TblFirmalar set Silik=1 where FirmaId='" + FirmaId + "'");

                    Database.SaveChanges();
                    result.Success = true;
                    result.StatusCode = 200;
                }
            }
            catch (Exception ex)
            {
                result.Success = false;
                result.Message = ex.Message;
                result.StatusCode = -1001;

            }

            return result;
        }



        public TResult DoFirmaListele()
        {
            TResult result = new TResult();

            try
            {
                List<TblFirmalar> FirmaListesi = ((List<TblFirmalar>)(from d in Database.TblFirmalar where d.Silik != true select d)).ToList();

                if (FirmaListesi == null)
                {
                    result.Message = "Firma bulunamadı";
                }
                else
                {
                   result.Data.AddRange(FirmaListesi);
                    result.Success = true;
                    result.StatusCode = 200;
                }

            }
            catch (Exception ex)
            {
                result.Success = false;
                result.StatusCode = -1001;

            }


            return result;
        }


        public TResult DoFirmaArama(string FirmaAdi)
        {
            TResult result = new TResult();

            try
            {
                List<TblFirmalar> FirmaListesi = ((List<TblFirmalar>)(from d in Database.TblFirmalar where d.FirmaUnvan.Contains(FirmaAdi) || d.FirmaKodu.Contains(FirmaAdi) select d)).ToList();

                if (FirmaListesi == null)
                {
                    result.Message = "Firma bulunamadı";
                }
                else
                {
                    result.Data.AddRange(FirmaListesi);
                    result.Success = true;
                    result.StatusCode = 200;
                }

            }
            catch (Exception ex)
            {
                result.Success = false;
                result.StatusCode = -1001;

            }


            return result;
        }


        public TResult DoFirmaDetay(int FirmaId)
        {
            TResult result = new TResult();

            try
            {
                var Kayit = (from d in Database.TblFirmalar where d.FirmaId == FirmaId select d).FirstOrDefault();

                if (Kayit == null)
                {
                    result.Message = "Firma bulunamadı";
                }
                else
                {
                    result.Data.Add(Kayit);
                    result.Success = true;
                    result.StatusCode = 200;
                }

            }
            catch (Exception ex)
            {
                result.Success = false;
                result.StatusCode = -1001;

            }


            return result;
        }




        public TResult DoPasifFirmaListele()
        {
            TResult result = new TResult();

            try
            {
                List<TblFirmalar> FirmaListesi = ((List<TblFirmalar>)(from d in Database.TblFirmalar where d.Silik == true select d)).ToList();

                if (FirmaListesi == null)
                {
                    result.Message = "Firma bulunamadı";
                }
                else
                {
                    result.Data.AddRange(FirmaListesi);
                    result.Success = true;
                    result.StatusCode = 200;
                }

            }
            catch (Exception ex)
            {
                result.Success = false;
                result.StatusCode = -1001;

            }


            return result;
        }






    }


}
