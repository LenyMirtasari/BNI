using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace SewaAPI.Models
{
    [Table("Tb_T_Account")]
    [Index(nameof(Account.Email), IsUnique = true)]
    public class Account
    {
        [Key]
        public string Email { get; set; }
        public string Password { get; set; }
    }
}
