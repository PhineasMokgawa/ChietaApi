using Abp.Application.Services.Dto;
using Abp.Domain.Repositories;
using Abp.ObjectMapping;
using CHIETAMIS.Counters.Dtos;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CHIETAMIS.Counters
{
    public class CounterAppService : CHIETAMISAppServiceBase
    {
        private readonly IRepository<Counter> _counterRepository;

        public CounterAppService(IRepository<Counter> counterRepository)
        {
            _counterRepository = counterRepository;
        }

        public async Task<CounterDto> Counters()
        {
            var cnt = _counterRepository.GetAll();
            var output = ObjectMapper.Map<CounterDto>(cnt);


            return output;
        }

    }
}
