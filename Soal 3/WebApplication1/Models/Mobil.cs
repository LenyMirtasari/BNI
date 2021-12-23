using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace SewaAPI.Models
{
    [Table("Tb_T_Mobil")]
    public class Mobil
    {   
        [Key]
        public int MobilId { get; set; }
        public string Tipe { get; set; }
        public string plat { get; set; }
        public StatusMobil StatusMobil { get; set; }

        [JsonIgnore]
        public virtual ICollection<LogPenyewa> LogPenyewa { get; set; }
    }

    public enum StatusMobil
    {
        Available,
        NotAvailable
    }
}
