using SewaAPI.Context;
using SewaAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SewaAPI.Repository.Data
{
    public class LogPenyewaRepository : GeneralRepository<MyContext, LogPenyewa, int>
    {
        private readonly MyContext myContext;
        public LogPenyewaRepository(MyContext myContext) : base(myContext)
        {
            this.myContext = myContext;
        }
    }
}
