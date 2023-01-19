using ProjeYonetim.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjeYonetim.Business
{
    public class BaseKullaniciIslemleri
    {
        private DbProjeYonetimEntities2 Database;

        public BaseKullaniciIslemleri()
        {
            Database = new DbProjeYonetimEntities2();
        }


        internal bool DoLogin(string KullaniciBilgisi, string Sifre, string SecretKey, out VwKisiKullaniciIletisim KullaniciKisi, out string Message)
        {

            bool result = false;

            Message = "";

            KullaniciKisi = null;

            try
            {   

                var KisiKaydi = (from d in Database.VwKisiKullaniciIletisim
                                 where d.Iletisim == KullaniciBilgisi ||
                                 d.KullaniciAdi == KullaniciBilgisi 
                                 select d).FirstOrDefault();

                if (KisiKaydi == null)
                {
                    Message = "Kullanıcı bulunamadı iletişim bilgileri veya kullanıcı adı hatalı";
                }
                else
                {
                    if (ProjeYonetim.Security.Security.StrToMd5(KisiKaydi.Sifre) != ProjeYonetim.Security.Security.StrToMd5(Sifre))
                    {
                        Message = "Kullanıcı bilgileri hatalı";
                    }
                    else
                    {
                        KullaniciKisi = KisiKaydi;
                        result = true;
                    }
                }


            }
            catch (Exception ex)
            {

                Message = ex.Message;
            }



            return result;
        }


        internal TResult DoRegister(TKullaniciKisiIletisim KisiBilgileri)
        {

            bool ExistUser()
            {
                bool exist = false;

                var Kisi = (from d in Database.TblKisi where d.Tc == KisiBilgileri.Kisi.Tc select d).FirstOrDefault();
                if (Kisi != null)
                    exist = true;


                return exist;
            }

            TResult result = new TResult();

            try
            {
                if (!ExistUser())
                {
                    TblKisi Kisi = KisiBilgileri.Kisi;

                    TblKisiFirma KisiFirma = KisiBilgileri.KisiFirma;

                    TblKullanicilar Kullanicilar = KisiBilgileri.Kullanici;

                    List<TblKisiIletisim> KisiIletisimler = KisiBilgileri.KisiIletisimler;


                  

                    Database.TblKisi.Add(Kisi);
                    Database.SaveChanges();

                    Kullanicilar.Sifre = ProjeYonetim.Security.Security.StrToMd5(Kullanicilar.Sifre);
                    Kullanicilar.KisiId = Kisi.KisiId;
                    Database.TblKullanicilar.Add(Kullanicilar);
                    Database.SaveChanges();


                    KisiFirma.KisiId = Kisi.KisiId;
                    Database.TblKisiFirma.Add(KisiFirma);
                    Database.SaveChanges();


                    foreach (var Iletisim in KisiIletisimler)
                    {
                        Database.TblKisiIletisim.Add(Iletisim);
                        Database.SaveChanges();

                    }

                    Kullanicilar.Sifre = "";
                    result.StatusCode = 200;
                    result.Success = true;
                    result.Data.Add(Kisi);
                    result.Data.Add(KisiFirma);
                    result.Data.Add(Kullanicilar);
                    result.Data.Add(KisiIletisimler);

                }


            }
            catch (Exception )
            {
                result.Message = "hata meydana geldi";
                result.StatusCode = -1001;
            }


            return result;

        }


        //private bool ExistUser(TKullaniciKisiIletisim KullaniciKisiIletisim)
        //{
        //    bool result = false;

        //    var Kisi = (from d in Database.TblKisi where d.Tc == KullaniciKisiIletisim.Kisi.Tc select d).FirstOrDefault();
        //    if (Kisi != null)
        //        result= true;


        //    return result;
        //}

        internal bool DoGetPersonDetail(Guid PersonGuid, out VwKisiKullaniciIletisim KullaniciKisi, out string Mesaj)
        {
            bool result = false;
            KullaniciKisi = null;
            Mesaj = "";

            try
            {

                var KisiKaydi = (from d in Database.VwKisiKullaniciIletisim where d.Guid.Value == PersonGuid select d).FirstOrDefault();

                if (KisiKaydi == null)
                {
                    Mesaj = "Kişi bilgileri hatalı ";
                }
                else
                {
                    KullaniciKisi = KisiKaydi;
                    result = true;
                }


            } 
            catch (Exception ex)
            {

                Mesaj = ex.Message ;
            }

            return result;

        }




    }
}
