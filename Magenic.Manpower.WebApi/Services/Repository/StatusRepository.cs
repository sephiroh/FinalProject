using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Magenic.Manpower.EFCore.Models;
using Magenic.Manpower.WebApi.DTO;

namespace Magenic.Manpower.WebApi.Services.Repository
{
    public class StatusRepository : BaseRepository, IStatusRepository
    {
        public StatusRepository(MagenicManpowerDBContext dbContext) : base(dbContext)
        {
        }

        public IEnumerable<StatusDTO> GetStatuses()
        {
            var result = new List<StatusDTO>();
            var statuses = _dbContext.Status;
            foreach (var status in statuses)
            {
                result.Add(new StatusDTO()
                {
                    Id = status.Id,
                    Name = status.Name 
                });
            }

            return result;
        }
    }
}
