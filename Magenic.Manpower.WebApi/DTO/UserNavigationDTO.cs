using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Magenic.Manpower.WebApi.DTO
{
    public class UserNavigationDTO
    {
        public UserNavigationDTO(string displayText, string url, List<UserNavigationDTO> childNavigations = null)
        {
            this.DisplayText = displayText;
            this.Url = url;
            this.ChildNavigations = new List<UserNavigationDTO>();
            if (childNavigations.Any())
            {
                this.ChildNavigations.AddRange(childNavigations);
            }
        }

        public string DisplayText { get; set; }
        public string Url { get; set; }
        public List<UserNavigationDTO> ChildNavigations { get; set; }
    }
}
