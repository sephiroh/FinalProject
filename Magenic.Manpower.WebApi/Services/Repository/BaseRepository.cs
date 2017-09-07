using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Magenic.Manpower.EFCore.Models;

namespace Magenic.Manpower.WebApi.Services.Repository
{
    public class BaseRepository
    {
        protected readonly MagenicManpowerDBContext _dbContext;
        public BaseRepository(MagenicManpowerDBContext dbContext)
        {
            _dbContext = dbContext;
        }
    }
}
