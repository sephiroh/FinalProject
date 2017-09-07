using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Magenic.Manpower.WebApi.DTO
{
    public class ServiceResponseDTO<T>
    {
        public ServiceResponseDTO()
        {
            this.Errors = new List<string>();
        }

        public ServiceResponseDTO(bool isSuccess, T data, IEnumerable<string> errors)
        {
            Success = isSuccess;
            ResponseData = data;
            this.Errors = new List<string>();
            if (errors != null && errors.Any())
            {
                Errors.AddRange(errors);
            }            
        }
        public bool Success { get; set; }
        public List<string> Errors { get; set; }
        public T ResponseData { get; set; }
    }
}
