using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using SewaAPI.Base;
using SewaAPI.Models;
using SewaAPI.Repository.Data;
using SewaAPI.ViewModel;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Net;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace SewaAPI.Controllers
{
    public class PenyewasController : BaseController<Penyewa, PenyewaRepository, int>
    {
        private readonly PenyewaRepository repository;
        public IConfiguration _configuration;
        public PenyewasController(PenyewaRepository penyewaRepository, IConfiguration configuration) : base(penyewaRepository)
        {
            this.repository = penyewaRepository;
            this._configuration = configuration;
        }

        [Route("Register")]
        [HttpPost]
        public ActionResult Register(RegisterVM registerVM)
        {
            try { 
                repository.Register(registerVM);          
                return Ok(new { status = HttpStatusCode.OK, result = "", message = "Berhasil Memasukkan Data Baru " });
            }
            catch
            {
                return BadRequest(new { status = HttpStatusCode.BadRequest, result = "", message = "Phone Number Already Existed ! " });
            }
        }


        [Route("Penyewa/{Key}")]
        [HttpGet]
        public ActionResult Penyewa(int Key)
        {
           
            try
            {
                var result = repository.GetPenyewa(Key);
                return Ok(result);
            }
            catch
            {
                return NotFound(new { status = HttpStatusCode.NotFound, result = "", message = "Data Not Found " });
            }
        }

        [Route("Mobil")]
        [HttpGet]
        public ActionResult Mobil()
        {

            try
            {
                var result = repository.GetMobil();
                return Ok(result);
            }
            catch
            {
                return NotFound(new { status = HttpStatusCode.NotFound, result = "", message = "Data Not Found " });
            }
        }

        [Route("LogHistory")]
        [HttpGet]
        public ActionResult LogHistory()
        {

            try
            {
                var result = repository.LogHistory();
                return Ok(result);
            }
            catch
            {
                return NotFound(new { status = HttpStatusCode.NotFound, result = "", message = "Data Not Found " });
            }
        }

        [Route("LogHistory/{Key}")]
        [HttpGet]
        public ActionResult LogHistory(int Key)
        {

            try
            {
                var result = repository.LogHistory(Key);
                return Ok(result);
            }
            catch
            {
                return NotFound(new { status = HttpStatusCode.NotFound, result = "", message = "Data Not Found " });
            }
        }

        [Route("Kembali")]
        [HttpPut]
        public ActionResult Kembali(KembaliVM kembaliVM)
        {
            try
            {
                var result = repository.Kembali(kembaliVM);
                return Ok(new { status = HttpStatusCode.OK, result, message = "Data Updated" });
            }
            catch (Exception)
            {
                return Ok(new { status = HttpStatusCode.InternalServerError, result = "", message = "Data Update Failed" });
            }
        }


        [Route("TambahSewa")]
        [HttpPost]
        public ActionResult LeaveRequest(TambahSewaVM tambahSewaVM)
        {
            var result = repository.TambahSewa(tambahSewaVM);

            if (result == 1)
            {
                return NotFound(new { status = HttpStatusCode.NotFound, result = "", message = "Date has passed" });
            }
            else
            {
                return Ok(result);
            }

        }

        [Route("TambahUser")]
        [HttpPost]
        public ActionResult TambahUser(UserVM userVM)
        {      
            try
            {
                var validasi = repository.TambahUser(userVM);
                return Ok(new { status = HttpStatusCode.OK, result = "", message = "Berhasil Menambah Data Baru " });
            }
            catch
            {
                return BadRequest(new { status = HttpStatusCode.BadRequest, result = "", message = "Email Sudah Terdaftar! " });
            }
        }

        [Route("Login")]
        [HttpPost]
        public ActionResult Login(LoginVM loginVM)
        {
            var check = repository.GetLogin(loginVM.Email, loginVM.Password);

            if (check == 1)
            {
                return BadRequest(new JWTokenVM { Messages = "Email/Password Salah", Token = null });

            }
            else if (check == 2)
            {
                return BadRequest(new JWTokenVM { Messages = "Email/Password Salah", Token = null });
            }
            else
            {
                
                var data = new LoginDataVM()
                {
                    Email = loginVM.Email,
                };

                var claims = new List<Claim>
                {
                    new Claim("email", data.Email),
                    
                };
               
                var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
                var signIn = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
                var token = new JwtSecurityToken(
                            _configuration["Jwt:Issuer"],
                            _configuration["Jwt:Audience"],
                            claims,
                            expires: DateTime.UtcNow.AddMinutes(10),
                            signingCredentials: signIn
                            );
                var idtoken = new JwtSecurityTokenHandler().WriteToken(token);
                claims.Add(new Claim("TokenSecurity", idtoken.ToString()));
                return Ok(new JWTokenVM
                {
                    Messages = "Login Berhasil",
                    Token = idtoken,
                    Email = loginVM.Email
                });
            }

        }

    }
}
