using SewaAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SewaAPI.ViewModel
{
    public class TambahSewaVM
    {
        public int PenyewaId { get; set; }
        public int MobilId { get; set; }
        public DateTime MulaiSewa { get; set; }
        public DateTime AkhirSewa { get; set; }
        public StatusMobil StatusMobil { get; set; }

    }
}
