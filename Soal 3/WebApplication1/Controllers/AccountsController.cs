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
    public class AccountsController : BaseController<Account, AccountRepository, string>
    {
        public AccountsController(AccountRepository accountRepository) : base(accountRepository)
        {

        }
    }
}
