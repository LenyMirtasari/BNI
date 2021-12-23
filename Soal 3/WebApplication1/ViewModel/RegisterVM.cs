using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SewaAPI.ViewModel
{
    public class RegisterVM
    {
        [Required(ErrorMessage = "Please enter your name")]
        public string Nama { get; set; }

        [Required(ErrorMessage = "Please enter your address")]
        public string Alamat { get; set; }

        [Required(ErrorMessage = "Please enter your phone number")]
        public string NoTelp { get; set; }
    }
}
