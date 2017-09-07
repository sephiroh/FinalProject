using Magenic.Manpower.EFCore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Magenic.Manpower.WebApi.Services.Repository
{
    /// <summary>
    /// 
    /// </summary>
    public interface IReferenceNumberRepository
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="requestId"></param>
        /// <param name="statusId"></param>
        /// <param name="levelId"></param>
        /// <returns></returns>
        int CreateReferenceNumber(int requestId, int statusId, int levelId);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="requestId"></param>
        /// <returns></returns>
        IList<ReferenceNumber> GetReferenceNumbers(int? requestId);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="refId"></param>
        /// <param name="applicantId"></param>
        /// <returns></returns>
        bool FillRequest(int refId, int applicantId);
    }
}
