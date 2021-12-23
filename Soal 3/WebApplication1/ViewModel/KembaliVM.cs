using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SewaAPI.ViewModel
{
    public class KembaliVM
    {
        public int LogId { get; set; }
        public int PenyewaId { get; set; }
        public int MobilId { get; set; }

        public DateTime TglKembali { get; set; }
    }
}
