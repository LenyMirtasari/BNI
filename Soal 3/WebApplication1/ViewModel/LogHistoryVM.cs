using SewaAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SewaAPI.ViewModel
{
    public class LogHistoryVM
    {
        public int LogId { get; set; }
        public int PenyewaId { get; set; }
        public int MobilId { get; set; }
        public DateTime MulaiSewa { get; set; }
        public DateTime AkhirSewa { get; set; }
        public string Tipe { get; set; }
        public string plat { get; set; }
        public StatusMobil StatusMobil { get; set; }
        public string Nama { get; set; }
        public string Alamat { get; set; }
        public string NoTelp { get; set; }
    }
}
