using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Magenic.Manpower.WebApi.ServiceLogic
{
    public class BaseSvc
    {
        protected readonly IServiceProvider _container;
        protected readonly IMapper _mapper;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="container"></param>
        /// <param name="mapper"></param>
        public BaseSvc(IServiceProvider container, IMapper mapper)
        {
            _container = container;
            _mapper = mapper;
        }
    }
}
