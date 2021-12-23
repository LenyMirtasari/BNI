using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SewaAPI.ViewModel;
using SewaClient.Base.Controllers;
using SewaClient.Repositories.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SewaClient.Controllers
{
    public class LoginController : BaseController<LoginVM, LoginRepository, string>
    {
        private readonly LoginRepository repository;
        public LoginController(LoginRepository repository) : base(repository)
        {
            this.repository = repository;
        }
        public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> Auth(LoginVM login)
        {
            var jwtToken = await repository.Auth(login);
            var token = jwtToken.Token;
            string email = jwtToken.Email;

            // int aa = roleName.Count();
            if (token == null)
            {
                return RedirectToAction("Index", "Login");
            }

            HttpContext.Session.SetString("JWToken", token);
            HttpContext.Session.SetString("Email", email);

            return RedirectToAction("Sewa", "Penyewas");
        }

        [Authorize]
        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("index", "Login");
        }
    }
}
