using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Magenic.Manpower.EFCore.Models;

namespace Magenic.Manpower.WebApi.Services.Repository
{
    public class ReferenceNumberRepository : BaseRepository, IReferenceNumberRepository
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ReferenceNumberRepository"/> class.
        /// </summary>
        /// <param name="dbContext">The database context.</param>
        public ReferenceNumberRepository(MagenicManpowerDBContext dbContext) : base(dbContext)
        { }

        /// <summary>
        /// Creates the reference number.
        /// </summary>
        /// <param name="requestId">The request identifier.</param>
        /// <param name="statusId">The status identifier.</param>
        /// <param name="levelId">The level identifier.</param>
        public int CreateReferenceNumber(int requestId, int statusId, int levelId)
        {
            using (var scope = _dbContext.Database.BeginTransaction())
            {
                // get current year
                var currentYear = DateTime.Now.Year;
                // get the record with max id
                var refRecord = _dbContext.ReferenceNumber.OrderByDescending(a => a.Id).FirstOrDefault();
                string refNumber = string.Empty;
                if (refRecord == null)
                {
                    // first record
                    refNumber = currentYear + "-1";
                }
                else
                {
                    // trim refRecord refNumber year and match it with current year
                    int refYear;
                    int refCounter = 0;
                    string[] arrRefString = refRecord.ReferenceString.Split(new string[] { "-" }, StringSplitOptions.RemoveEmptyEntries);
                    if (int.TryParse(arrRefString[0], out refYear))
                    {
                        // if current year is more than refRecord year
                        if (currentYear > refYear)
                        {
                            // reset counter to 1
                            refNumber = currentYear + "-1";
                        }
                        else
                        {
                            // increment refRecord counter
                            refCounter = int.Parse(arrRefString[1]) + 1;
                            refNumber = refYear + "-" + refCounter;
                        }
                    }
                    else
                    {
                        // use current year
                        // increment refRecord counter
                        refCounter = int.Parse(arrRefString[1]) + 1;
                        refNumber = refYear + "-" + refCounter;
                    }
                }

                var referenceNumber = new ReferenceNumber()
                {
                    ManpowerRequestId = requestId,
                    StatusId = statusId,
                    DateCreated = DateTime.Now,
                    ReferenceString = refNumber,
                    LevelId = levelId
                };

                _dbContext.ReferenceNumber.Add(referenceNumber);
                _dbContext.SaveChanges();
                _dbContext.Database.CommitTransaction();

                return referenceNumber.Id;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="refId"></param>
        /// <param name="applicantId"></param>
        /// <returns></returns>
        public bool FillRequest(int refId, int applicantId)
        {
            try
            {
                var _request = _dbContext.ReferenceNumber.FirstOrDefault(a => a.Id == refId);
                if(_request==null)
                    throw new KeyNotFoundException("Reference Id not found.");

                _request.StatusId = (int)ReferenceNumberStatus.Filled;
                _request.ApplicantId = applicantId;
                _request.DateUpdated = DateTime.Now;

                _dbContext.Update(_request);
                _dbContext.SaveChanges();

                return true;
            }
            catch (Exception)
            {

                throw;
            }   
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="requestId">based on Manpower Request, if any</param>
        /// <returns></returns>
        public IList<ReferenceNumber> GetReferenceNumbers(int? requestId)
        {
            try
            {
                var refNumbers = _dbContext.ReferenceNumber.AsEnumerable();
                if (requestId.HasValue)
                    refNumbers = refNumbers.Where(a => a.ManpowerRequestId == requestId.Value);

                return refNumbers.ToList();
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
