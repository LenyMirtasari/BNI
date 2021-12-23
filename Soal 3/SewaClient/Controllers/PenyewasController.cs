using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SewaAPI.Models;
using SewaAPI.ViewModel;
using SewaClient.Base.Controllers;
using SewaClient.Repositories.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SewaClient.Controllers
{
    public class PenyewasController : BaseController<Penyewa, PenyewaRepository, int>
    {
        private readonly PenyewaRepository repository;
        public PenyewasController(PenyewaRepository repository) : base(repository)
        {
            this.repository = repository;
        }
        [Authorize]
        public IActionResult Sewa()
        {
            return View();
        }
        [Authorize]
        public IActionResult Penyewa()
        {
            return View();
        }

        [HttpGet]
        public async Task<JsonResult> LogHistory()
        {
            var result = await repository.LogHistory();
            return Json(result);
        }

        [HttpGet]
        public async Task<JsonResult> LogHistoryId(int id)
        {
            var result = await repository.LogHistoryId(id);
            return Json(result);
        }

        [HttpPost]
        public JsonResult Register(RegisterVM registerVM)
        {
            var result = repository.Register(registerVM);
            return Json(result);
        }

        [HttpGet]
        public async Task<JsonResult> PenyewaId(int id)
        {
            var result = await repository.Penyewa(id);
            return Json(result);
        }


    }
}
