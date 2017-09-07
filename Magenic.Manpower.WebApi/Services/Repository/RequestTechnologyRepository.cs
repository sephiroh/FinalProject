using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Magenic.Manpower.EFCore.Models;

namespace Magenic.Manpower.WebApi.Services.Repository
{
    public class RequestTechnologyRepository : BaseRepository, IRequestTechnologyRepository
    {
        public RequestTechnologyRepository(MagenicManpowerDBContext dbContext) : base(dbContext)
        {
        }

        /// <summary>
        /// Adds the request technology.
        /// </summary>
        /// <param name="model">The model.</param>
        public void AddRequestTechnology(ManpowerRequestTechnology model)
        {
            _dbContext.Add(model);
            _dbContext.SaveChanges();
        }
    }
}
