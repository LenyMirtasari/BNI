using SewaAPI.Context;
using SewaAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SewaAPI.Repository.Data
{
    public class MobilRepository : GeneralRepository<MyContext, Mobil, int>
    {
        private readonly MyContext myContext;
        public MobilRepository(MyContext myContext) : base(myContext)
        {
            this.myContext = myContext;
        }
    }
}
