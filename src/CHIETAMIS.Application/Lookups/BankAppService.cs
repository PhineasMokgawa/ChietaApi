using Abp.Application.Services.Dto;
using Abp.Domain.Repositories;
using Abp.ObjectMapping;
using CHIETAMIS.Lookups.DTOs;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CHIETAMIS.Lookups
{
    public class BankAppService: CHIETAMISAppServiceBase
    {
        private readonly IRepository<Bank> _bankRepository;
        private readonly IRepository<Bank_Account_Type> _accTypeRepository;
        public BankAppService(IRepository<Bank> bankRepository, IRepository<Bank_Account_Type> accTypeRepository)
        {
            _bankRepository = bankRepository;
            _accTypeRepository = accTypeRepository;
        }

        public async Task<PagedResultDto<BankDtoForView>> Banks()
        {
            var tasks = _bankRepository.GetAll();

            var query = (from o in tasks
                         select new BankDtoForView()
                         {
                             Bank = new BankDto
                             {
                                 Bank_Name = o.Bank_Name,
                                 Branch_Code = o.Branch_Code,
                                 Id = o.Id
                             }
                         });

            var _banks = await query.ToListAsync();
            var totalCount = await query.CountAsync();


            return new PagedResultDto<BankDtoForView>(totalCount, _banks);
        }

        public async Task<PagedResultDto<Bank_Account_TypeViewDto>> AccountTypes()
        {
            var tasks = _accTypeRepository.GetAll();

            var query = (from o in tasks
                         select new Bank_Account_TypeViewDto()
                         {
                             AccountType = new Bank_Account_TypeDto
                             {
                                 AccountType = o.AccountType,
                                 Id = o.Id
                             }
                         });

            var output = await query.ToListAsync();
            var totalCount = await query.CountAsync();


            return new PagedResultDto<Bank_Account_TypeViewDto>(totalCount, output);
        }
    }
}
