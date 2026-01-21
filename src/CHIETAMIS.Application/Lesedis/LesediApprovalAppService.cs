using Abp.Domain.Repositories;
using CHIETAMIS.Authorization.Users;
using CHIETAMIS.Lesedis.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CHIETAMIS.Lesedis
{
    public class LesediApprovalAppService : CHIETAMISAppServiceBase
    {
        private readonly IUserEmailer _userEmailer;

        private readonly IRepository<Lesedi> _lesedi;

        public LesediApprovalAppService(IRepository<Lesedi> lesedi)
        {
            _lesedi = lesedi;
        }
    }
}
