using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SewaAPI.ViewModel
{
    public class PenyewaVM
    {
        public int PenyewaId { get; set; }
        public string Nama { get; set; }
        public string Alamat { get; set; }
        public string NoTelp { get; set; }
    }
}
