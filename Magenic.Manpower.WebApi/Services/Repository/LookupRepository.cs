using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Magenic.Manpower.EFCore.Models;

namespace Magenic.Manpower.WebApi.Services.Repository
{
    public class LookupRepository : BaseRepository, ILookupRepository
    {
        public LookupRepository(MagenicManpowerDBContext dbContext) : base(dbContext)
        {
        }

        public List<ApplicantLevel> ApplicantLevels
        {
            get
            {
                /*add caching support next sprint*/
                return _dbContext.ApplicantLevel.ToList();
            }
        }

        public List<Permission> Permissions
        {
            get
            {
                /*add caching support next sprint*/
                return _dbContext.Permission.ToList();
            }
        }

        public List<MagenicRegion> Regions
        {
            get
            {
                /*add caching support next sprint*/
                return _dbContext.MagenicRegion.ToList();
            }
        }

        public List<Status> Status
        {
            get
            {
                /*add caching support next sprint*/
                return _dbContext.Status.ToList();
            }
        }


        public List<ApplicantStatus> ApplicantStatus
        {
            get
            {
                /*add caching support next sprint*/
                return _dbContext.ApplicantStatus.ToList();
            }
        }

    }
}
