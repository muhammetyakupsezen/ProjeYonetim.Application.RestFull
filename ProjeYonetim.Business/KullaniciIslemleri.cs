using ProjeYonetim.Data;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace ProjeYonetim.Business
{
    public class KullaniciIslemleri : BaseKullaniciIslemleri
    {
        private static int ExpireMinute = Convert.ToInt32(System.Configuration.ConfigurationSettings.AppSettings["TokenExpireMinute"].ToString());
        public TResult GetToken(string KullaniciBilgisi, string Sifre, string SecretKey)
        {
            TResult Result = new TResult();
            string Mesaj = "";
            string Token = "";
            Result.StatusCode = -1000;
            VwKisiKullaniciIletisim KullaniciKisi;

            bool success = base.DoLogin(KullaniciBilgisi, Sifre, SecretKey, out KullaniciKisi, out Mesaj);



            if (success)
            {
                if (KullaniciKisi != null)
                {
                    Token = DoCreateToken(KullaniciKisi, SecretKey, ExpireMinute);
                }

                Result.Success = success;
                Result.StatusCode = 200;
                Result.Data.Add(Token);


            }

            return Result;
        }


        public TResult GetPersonDetail(string Token, string SecretKey, Guid PersonGuid)
        {

            TResult Result = new TResult();
            Result.StatusCode = -1001;
            TToken OpenToken;
            Result = ValidToken(Token, SecretKey, out OpenToken);
            if (Result.Success)
            {
                string Mesaj = "";
                VwKisiKullaniciIletisim KullaniciKisi;

                bool success = base.DoGetPersonDetail(PersonGuid, out KullaniciKisi, out Mesaj);
                Result.Message = Mesaj;

                if (success)
                {
                    if (KullaniciKisi != null)
                    {
                        Result.Success = success;
                        Result.StatusCode = 200;
                        Result.Data.Add(KullaniciKisi);

                    }
                }
            }



            return Result;
        }


        public TResult GetPersonDetail(string Token, string SecretKey)
        {

            TResult Result = new TResult();
            Result.StatusCode = -1001;

            TToken OpenToken;
            Result = ValidToken(Token, SecretKey, out OpenToken);
            if (Result.Success && OpenToken != null)
            {
                Guid PersonGuid = OpenToken.Guid;
                string Mesaj = "";
                VwKisiKullaniciIletisim KullaniciKisi;

                bool success = base.DoGetPersonDetail(PersonGuid, out KullaniciKisi, out Mesaj);
                Result.Message = Mesaj;

                if (success)
                {
                    if (KullaniciKisi != null)
                    {
                        Result.Success = success;
                        Result.StatusCode = 200;
                        Result.Data.Add(KullaniciKisi);

                    }
                }
            }



            return Result;
        }




        //internal bool DoGetPersonDetail(Guid PersonGuid, out VwKisiKullaniciIletisim KullaniciKisi, out string Mesaj)
        //{
        //    bool result = false;
        //    KullaniciKisi = null;
        //    Mesaj = "";

        //    try
        //    {

        //        var KisiKaydi = (from d in Database.VwKisiKullaniciIletisim where d.Guid.Value == PersonGuid select d).FirstOrDefault();

        //        if (KisiKaydi == null)
        //        {
        //            Mesaj = "Kişi bilgileri hatalı ";
        //        }
        //        else
        //        {
        //            KullaniciKisi = KisiKaydi;
        //            result = true;
        //        }


        //    }
        //    catch (Exception ex)
        //    {

        //        Mesaj = ex.Message;
        //    }

        //    return result;

        //}




















        public TResult ValidToken(string Token, string SecretKey, out TToken OpenToken )
        {
            TResult Result = new TResult();
            Result.StatusCode = -1000;

            VwKisiKullaniciIletisim KullaniciKisi;

            bool IsValid = DoValidToken(Token, SecretKey, out OpenToken);

            Result.Success = IsValid;
            Result.StatusCode = 200;
          //  Result.Data.Add(Token);




            return Result;
        }


        public TResult Register(TKullaniciKisiIletisim KisiBilgileri)
        {
            return DoRegister(KisiBilgileri);

        }



        private string DoCreateToken(VwKisiKullaniciIletisim KullaniciKisi, string SecretKey, int ExpireMinute)
        {
            string result = KullaniciKisi.Guid.Value.ToString() + "|" +
                          KullaniciKisi.FirmaKodu + "|" +
                          KullaniciKisi.KisiId.ToString() + "|" +
                          KullaniciKisi.Tc.ToString() + "|" +
                          KullaniciKisi.KullaniciAdi + "|" +
                          KullaniciKisi.KullaniciId.ToString() + "|" +
                          DateTime.Now.AddMinutes(ExpireMinute) + "|" +
                          KullaniciKisi.Guid.Value;
            result = result.Replace(" ", "+").Replace("-", "_");
            result = ProjeYonetim.Security.Security.Encrypt(result, SecretKey);

            return result;

        }

        private bool DoValidToken(string Token, string SecretKey, out TToken OpenToken)
        {
            bool result = true;
            var DecyrptText = ProjeYonetim.Security.Security.Decrypt(Token, SecretKey);

            string LToken = DecyrptText.Replace("+", " ").Replace("_", "-");
            string[] values = LToken.Split('|');
            OpenToken = new TToken() {

                FirmaKodu = values[0],
                KisiId = Convert.ToInt32(values[1]),
                TC = Convert.ToUInt32(values[2]),
                KullaniciId = Convert.ToInt32(values[3]),
                ExpireMinute = Convert.ToDateTime(values[4]),
                Guid = Guid.Parse(values[5])

            };

            if (OpenToken != null)
            {
                if (DateTime.Now > OpenToken.ExpireMinute)
                {
                    result = false;
                }
            }


            return result;

        }






    }




}
