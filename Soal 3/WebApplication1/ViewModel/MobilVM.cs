using SewaAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SewaAPI.ViewModel
{
    public class MobilVM
    {
        public int MobilId { get; set; }
        public string Tipe { get; set; }
        public string plat { get; set; }
        public StatusMobil StatusMobil { get; set; }
    }
}
