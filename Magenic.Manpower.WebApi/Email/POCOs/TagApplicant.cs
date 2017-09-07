using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Magenic.Manpower.WebApi.Email
{
    public class TagApplicant : IMailReference
    {
        public string Requestor { get; set; }
        public string ProjectName { get; set; }
        public int EstimateHires { get; set; }
        public string ManpowerAppUrl { get; set; }
        public string EmailSubject { get { return "New Manpower Request"; } }
        public EmailMessageType MessageType { get { return EmailMessageType.TagApplicantEmail; } }
        public EmailContentType ContentType { get { return EmailContentType.Plain; } }
        public Dictionary<string, string> MapObjectDictionary()
        {
            Dictionary<string, string> dictionary = new Dictionary<string, string>();
            dictionary.Add("Requestor", this.Requestor);
            dictionary.Add("ProjectName", this.ProjectName);
            dictionary.Add("EstimateHires", this.EstimateHires.ToString());
            dictionary.Add("ManpowerAppUrl", this.ManpowerAppUrl);
            return dictionary;
        }
    }
}
