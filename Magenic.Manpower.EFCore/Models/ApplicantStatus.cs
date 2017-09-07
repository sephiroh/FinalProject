using System;
using System.Collections.Generic;

namespace Magenic.Manpower.EFCore.Models
{
    public partial class ApplicantStatus
    {
        public ApplicantStatus()
        {
           
        }

        public int Id { get; set; }
        public string Name { get; set; }        
    }

    public enum ApplicantStatusType
    {
        New = 1,
        ForTechnicalExam = 2,
        PassedTechnicalExam = 3,
        FailedTechnicalExam = 4,
        ForHRInterview = 5,
        PassedHRInterview = 6,
        FailedHRInterview = 7,
        ForComprehensiveInterview = 8,
        PassedComprehensiveInterview = 9,
        FailedComprehensiveInterview = 10,
        ForJobOffer = 11,
        DeclinedOffer = 12,
        AcceptedOffer = 13,
        WithdrewApplication = 14,
        Hired = 15
    }
}
