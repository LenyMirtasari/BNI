using Microsoft.AspNetCore.Mvc;
using SewaAPI.Base;
using SewaAPI.Models;
using SewaAPI.Repository.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SewaAPI.Controllers
{
    public class LogPenyewasController : BaseController<LogPenyewa, LogPenyewaRepository, int>
    {
        public LogPenyewasController(LogPenyewaRepository logPenyewaRepository) : base(logPenyewaRepository)
        {

        }
    }
}
