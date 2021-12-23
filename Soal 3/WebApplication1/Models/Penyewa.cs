using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace SewaAPI.Models
{
    [Table("Tb_T_Penyewa")]
    [Index(nameof(Penyewa.NoTelp), IsUnique = true)]
    public class Penyewa
    {
        [Key]
        public int PenyewaId { get; set; }
        public string Nama { get; set; }
        public string Alamat { get; set; }
        public string NoTelp { get; set; }

        [JsonIgnore]
        public virtual ICollection<LogPenyewa> LogPenyewa{ get; set; }
       

    }
}
