using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace Magenic.Manpower.WebApi.Controllers
{
    public class BaseController:Controller
    {
        protected readonly IServiceProvider _container;
        public BaseController(IServiceProvider container)
        {
            _container = container;
        }
    }
}
