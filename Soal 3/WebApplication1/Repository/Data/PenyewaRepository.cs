using SewaAPI.Context;
using SewaAPI.Models;
using SewaAPI.PasswordHashing;
using SewaAPI.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SewaAPI.Repository.Data
{
    public class PenyewaRepository : GeneralRepository<MyContext, Penyewa, int>
    {
        private readonly MyContext myContext;
        public PenyewaRepository(MyContext myContext) : base(myContext)
        {
            this.myContext = myContext;
        }
        public int Register(RegisterVM registerVM)
        {
            Penyewa p = new Penyewa();
            p.Nama = registerVM.Nama;
            p.Alamat = registerVM.Alamat;
            p.NoTelp = registerVM.NoTelp;
            myContext.Add(p);
            myContext.SaveChanges();
            return 0;
        }

        public Object GetPenyewa(int Key)
        {
            var result = from p in myContext.Penyewas where p.PenyewaId== Key select new PenyewaVM()
                         {
                             PenyewaId = Key,
                             Nama = p.Nama,
                             Alamat = p.Alamat,
                             NoTelp = p.NoTelp                       
                         };

            return result.First();
        }

        public object GetMobil()
        {
            var result = from m in myContext.Mobils where m.StatusMobil == StatusMobil.Available
                         select new MobilVM()
                         {
                             MobilId = m.MobilId,
                             Tipe= m.Tipe,
                             plat = m.plat,
                             StatusMobil =m.StatusMobil
                         };

            return result;
        }

        public object LogHistory()
        {
            var result = from lp in myContext.LogPenyewas
                         join p in myContext.Penyewas on lp.PenyewaId equals p.PenyewaId
                         join m in myContext.Mobils on lp.MobilId equals m.MobilId
                         where m.StatusMobil == StatusMobil.NotAvailable && lp.Status == Status.Pinjam
                         select new LogHistoryVM()
                         {  
                             LogId = lp.LogId,
                             PenyewaId = lp.PenyewaId,
                             MobilId =lp.MobilId,
                             MulaiSewa = lp.MulaiSewa,
                             AkhirSewa = lp.AkhirSewa,
                             Tipe = m.Tipe,
                             plat = m.plat,
                             StatusMobil = m.StatusMobil,
                             Nama = p.Nama,
                             Alamat = p.Alamat,
                             NoTelp = p.NoTelp
                         };

            return result;
        }

        public object LogHistory(int Key)
        {
            var result = from lp in myContext.LogPenyewas
                         join p in myContext.Penyewas on lp.PenyewaId equals p.PenyewaId
                         join m in myContext.Mobils on lp.MobilId equals m.MobilId
                         where m.StatusMobil == StatusMobil.NotAvailable && lp.Status == Status.Pinjam 
                         && lp.LogId == Key
                         select new LogHistoryVM()
                         {
                             LogId = lp.LogId,
                             PenyewaId = lp.PenyewaId,
                             MobilId = lp.MobilId,
                             MulaiSewa = lp.MulaiSewa,
                             AkhirSewa = lp.AkhirSewa,
                             Tipe = m.Tipe,
                             plat = m.plat,
                             StatusMobil = m.StatusMobil,
                             Nama = p.Nama,
                             Alamat = p.Alamat,
                             NoTelp = p.NoTelp
                         };

            return result.First();
        }
        public bool HasExpired(DateTime date)
        {
            DateTime Expires = date;
            return DateTime.Now.CompareTo(Expires.Add(new TimeSpan(2, 0, 0))) > 0;
        }
        public int TambahSewa(TambahSewaVM tambahSewaVM)
        {
            DateTime MulaiSewa = tambahSewaVM.MulaiSewa;
            DateTime AkhirSewa = tambahSewaVM.AkhirSewa;
            var exp = HasExpired(MulaiSewa);
            if (exp == true)
            {
                return 1;
            }
            else
            {
                LogPenyewa lp = new LogPenyewa();
                lp.MulaiSewa = MulaiSewa;
                lp.AkhirSewa = AkhirSewa;
                lp.MobilId = tambahSewaVM.MobilId;
                lp.PenyewaId = tambahSewaVM.PenyewaId;
                lp.Status = Status.Pinjam;
                myContext.Add(lp);
                myContext.SaveChanges();

                Mobil m= myContext.Mobils.FirstOrDefault(x => x.MobilId == tambahSewaVM.MobilId);
                m.StatusMobil = StatusMobil.NotAvailable;
                myContext.SaveChanges();

                return 0;           
            }
        }


        public int Kembali(KembaliVM kembaliVM)
        {
            LogPenyewa lp = myContext.LogPenyewas.FirstOrDefault(x => x.LogId == kembaliVM.LogId);
            lp.TglKembali = DateTime.Now;
            if (kembaliVM.TglKembali > lp.AkhirSewa)
            {
                lp.Status = Status.Telat;
            }
            else
            {
                lp.Status = Status.OnTime;
            }
            myContext.SaveChanges();

            Mobil m = myContext.Mobils.FirstOrDefault(x => x.MobilId == kembaliVM.MobilId);
            m.StatusMobil = StatusMobil.Available;
            myContext.SaveChanges();

            return 0;
        }

        public int TambahUser(UserVM userVM)
        {
            Account a = new Account();
            a.Email = userVM.Email;
            a.Password = Hashing.HashPassword(userVM.Password);
            myContext.Add(a);
            myContext.SaveChanges();
            return 0;
        }

        public int GetLogin(string emailInput, string passwordInput)
        {
            try
            {
                var checkEmail = myContext.Accounts.Where(p => p.Email == emailInput).FirstOrDefault();
                var password = (from acc in myContext.Accounts
                                where acc.Email == emailInput
                                select acc.Password).Single();
                var validPw = Hashing.ValidatePassword(passwordInput, password);
                if (checkEmail != null)
                {
                    if (validPw == true)
                    {
                        return 0;

                    }
                    else if (validPw == false)
                    {
                        return 2;
                    }

                }
            }
            catch (InvalidOperationException)
            {
                return 1;
            }
            return 3;
        }


    }
}
