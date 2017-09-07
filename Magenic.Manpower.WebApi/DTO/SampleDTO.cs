using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Magenic.Manpower.WebApi.DTO
{
    public class SampleDTO
    {
        public SampleDTO(int id, string name, string url)
        {
            this.Id = id;
            this.Name = name;
            this.Url = url;
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public string Url { get; set; }
    }
}
