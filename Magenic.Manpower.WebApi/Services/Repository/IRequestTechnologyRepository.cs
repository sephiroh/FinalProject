using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Magenic.Manpower.EFCore.Models;

namespace Magenic.Manpower.WebApi.Services.Repository
{
    /// <summary>
    /// 
    /// </summary>
    public interface IRequestTechnologyRepository
    {
        /// <summary>
        /// Adds the request technology.
        /// </summary>
        /// <param name="model">The model.</param>
        void AddRequestTechnology(ManpowerRequestTechnology model);
    }
}
