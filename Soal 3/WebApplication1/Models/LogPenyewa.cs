using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace SewaAPI.Models
{
    [Table("Tb_T_LogPenyewa")]
    public class LogPenyewa
    {
        [Key]
        public int LogId { get; set; }
        public int PenyewaId { get; set; }
        public int MobilId { get; set; }
        public DateTime MulaiSewa { get; set; }
        public DateTime AkhirSewa { get; set; }

        public DateTime TglKembali { get; set; }
        public Status Status { get; set; }

        [JsonIgnore]
        public virtual Penyewa Penyewa { get; set; }
        [JsonIgnore]
        public virtual Mobil Mobil { get; set; }

    }

    public enum Status
    {
        OnTime,
        Telat,
        Pinjam
    }
}
