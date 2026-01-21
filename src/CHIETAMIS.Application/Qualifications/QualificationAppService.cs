using Abp.Application.Services.Dto;
using Abp.Domain.Repositories;
using CHIETAMIS.People;
using CHIETAMIS.Providers;
using CHIETAMIS.Providers.Dto;
using CHIETAMIS.Qualifications.Dtos;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CHIETAMIS.Qualifications
{
    public class QualificationAppService : CHIETAMISAppServiceBase
    {
        private readonly IRepository<Qualification> _qual;
        private readonly IRepository<Discretionary_Universtity_College> _college;

        public QualificationAppService(IRepository<Qualification> qual,
                                 IRepository<Discretionary_Universtity_College> college)

        {
            _qual = qual;
            _college = college;
        }

        public async Task<PagedResultDto<QualificationList>> GetQualificationsByTitle(string SearchName)
        {

            var cquals = (from q in _qual.GetAll()
                          select new
                          {
                              QualificationTitle = q.QUALIFICATION_TITLE,
                              QualificatioinId = q.QUALIFICATION_ID,
                              Provider_Name = q.PROVIDER_NAME,
                              Id = q.Id
                          }).Distinct().ToList();
                    //.Where(a => a.QualificationTitle.Contains(SearchName)).Distinct().ToList();

            var quals = from o in cquals
                       select new QualificationList()
                       {
                           Qualification = new QualificationDto

                           {
                               QUALIFICATION_TITLE = o.QualificationTitle,
                               QUALIFICATION_ID = o.QualificatioinId,
                               PROVIDER_NAME = o.Provider_Name,
                               Id = o.Id
                           }
                       };

            var totalCount = quals.Distinct().Count();

            return new PagedResultDto<QualificationList>(
                totalCount,
                quals.Distinct().ToList()
            );
        }

        public async Task<QualificationDto> GetQualificationByQualId(int QualId)
        {
            var qual = await _qual.GetAll()
                .Where(q => q.QUALIFICATION_ID == QualId)
                .Select(ql => new QualificationDto
                {
                    QUALIFICATION_TITLE = ql.QUALIFICATION_TITLE,
                    QUALIFICATION_ID = ql.QUALIFICATION_ID,
                    PROVIDER_NAME = ql.PROVIDER_NAME,
                    Id = ql.Id
                }).FirstOrDefaultAsync();

            return qual;
        }

        public async Task<QualificationDto> GetQualificationById(int Id)
        {
            var qual = await _qual.GetAll()
                .Where(q => q.Id == Id)
                .Select(ql => new QualificationDto
                {
                    QUALIFICATION_TITLE = ql.QUALIFICATION_TITLE,
                    QUALIFICATION_ID = ql.QUALIFICATION_ID,
                    PROVIDER_NAME = ql.PROVIDER_NAME,
                    Id = ql.Id
                }).FirstOrDefaultAsync();

            return qual;
        }
    }
}
