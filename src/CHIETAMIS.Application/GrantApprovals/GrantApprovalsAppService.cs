using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Abp.Linq.Extensions;
using Abp.Localization;
using Abp.Runtime.Session;
using Abp.UI;
using Microsoft.EntityFrameworkCore;
using Abp.Application.Services;
using CHIETAMIS.DiscretionaryProjects.Dtos;
using Abp.Domain.Repositories;
using Abp.Extensions;
using Abp.Application.Services.Dto;
using Abp.ObjectMapping;
using System.Text;
using Abp.Configuration;
using Abp.Zero.Configuration;
using CHIETAMIS.GrantApprovals.Dtos;
using CHIETAMIS.Lookups;
using CHIETAMIS.Lookups.DTOs;
using CHIETAMIS.DiscretionaryProjects;
using CHIETAMIS.DiscretionaryStratRess;

namespace CHIETAMIS.GrantApprovals
{
    public class GrantApprovalsAppService: CHIETAMISAppServiceBase
    {
        private readonly IRepository<DiscretionaryGrantApproval> _discGrantApprovalRepository;
        private readonly IRepository<GrantApprovalStatus> _discGrantApprovalStatusRepository;
        private readonly IRepository<GrantApprovalType> _discGrantApprovalTypeRepository;
        private readonly IRepository<DiscretionaryProject> _discProjRepository;
        private readonly IRepository<DiscretionaryDetailApproval> _discDetailApprovalRepository;
        private readonly IRepository<DiscretionaryResearchApproval> _discResearchApprovalRepository;
        private readonly IRepository<DiscretionaryProjectDetailsApproval> _discProjDetRepository;
        private readonly IRepository<DiscretionaryStratResDetailsApproval> _discResDetRepository;


        public GrantApprovalsAppService(IRepository<DiscretionaryGrantApproval> discGrantApprovalRepository,
                                        IRepository<GrantApprovalStatus> discGrantApprovalStatusRepository,
                                        IRepository<GrantApprovalType> discGrantApprovalTypeRepository,
                                        IRepository<DiscretionaryProject> dicprojRepository,
                                        IRepository<DiscretionaryDetailApproval> discDetailApprovalRepository,
                                        IRepository<DiscretionaryProjectDetailsApproval> discProjDetRepository,
                                        IRepository<DiscretionaryResearchApproval> discResearchApprovalRepository,
                                        IRepository<DiscretionaryStratResDetailsApproval> discResDetRepository)
        {
            _discGrantApprovalRepository = discGrantApprovalRepository;
            _discGrantApprovalStatusRepository = discGrantApprovalStatusRepository;
            _discGrantApprovalTypeRepository = discGrantApprovalTypeRepository;
            _discProjRepository = dicprojRepository;
            _discDetailApprovalRepository = discDetailApprovalRepository;
            _discProjDetRepository = discProjDetRepository;
            _discResearchApprovalRepository = discResearchApprovalRepository;
            _discResDetRepository = discResDetRepository;
        }

        public async Task<string> CreateEditGrantApproval(DiscretionaryGrantApprovalDto input)
        {
            var output = "";

            var discprojs = await (from det in _discDetailApprovalRepository.GetAll()
                            join projd in _discProjDetRepository.GetAll() on det.ApplicationId equals projd.Id
                            select new
                            {
                                Approval = det,
                                ProjectId = projd.ProjectId
                            })
                        .Where(a => a.ProjectId == input.ProjectId)
                        .ToListAsync();

            var cntdecline = 0;
            var cntrecomm = 0;

            foreach(var proj in discprojs)
            {
                if (proj.Approval.ApprovalStatusId == 1) { cntrecomm = cntrecomm + 1; };
                if (proj.Approval.ApprovalStatusId == 2) { cntdecline = cntdecline + 1; };
            }

            if ((input.ApprovalStatusId == 1 && cntrecomm == 0) || (input.ApprovalStatusId == 2 && cntrecomm > 0))
            {
                if (input.ApprovalStatusId == 1 && cntrecomm == 0) { output = output + ", There are no recommended lines cannot Recommend"; };
                if (input.ApprovalStatusId == 2 && cntrecomm > 0) { output = output + ", There are recommended lines, cannot Decline"; };
            }
            else
            {
                var dets = _discProjDetRepository.GetAll().Where(a => a.ProjectId == input.ProjectId);

                if (dets.Count() != discprojs.Count())
                {
                    output = output + ", Some lines have not been Evaluated.";
                }
                else
                {
                    var dgapproval = _discGrantApprovalRepository.GetAll().Where(a => a.ProjectId == input.ProjectId && a.ApprovalStatusId == input.ApprovalStatusId && a.ApprovalTypeId == input.ApprovalTypeId);

                    if (dgapproval.Count() == 0)
                    {
                        //var dgappr = _discGrantApprovalRepository.Get(input.Id);
                        input.DateCreated = DateTime.Now;
                        input.OutcomeDate = DateTime.Now;
                        var app = ObjectMapper.Map<DiscretionaryGrantApproval>(input);
                        await _discGrantApprovalRepository.InsertAsync(app);

                        var cappl = await _discProjRepository.FirstOrDefaultAsync(input.ProjectId);
                        cappl.DteUpd = DateTime.Now;
                        cappl.UsrUpd = input.UserId;
                        if (input.ApprovalStatusId == 1)
                        {
                            cappl.ProjectStatusID = 239;
                        }
                        if (input.ApprovalStatusId == 2){

                            cappl.ProjectStatusID = 89;
                        }
                        await _discProjRepository.UpdateAsync(cappl);
                    }
                    else
                    {
                        var dgappr = await _discGrantApprovalRepository.FirstOrDefaultAsync(dgapproval.First().Id);
                        dgappr.DteUpd = DateTime.Now;
                        dgappr.UsrUpd = input.UsrUpd;

                        await _discGrantApprovalRepository.UpdateAsync(dgappr);

                        var cappl = await _discProjRepository.FirstOrDefaultAsync(input.ProjectId);
                        cappl.DteUpd = DateTime.Now;
                        cappl.UsrUpd = input.UserId;
                        if (input.ApprovalStatusId == 1)
                        {
                            cappl.ProjectStatusID = 239;
                        }
                        if (input.ApprovalStatusId == 2)
                        {

                            cappl.ProjectStatusID = 89;
                        }
                        await _discProjRepository.UpdateAsync(cappl);
                    }
                }
            }

            return output;
        }

        public async Task<string> CreateEditRMGrantApproval(DiscretionaryGrantApprovalDto input)
        {
            var output = "";

            var discprojs = await (from det in _discDetailApprovalRepository.GetAll()
                                   join projd in _discProjDetRepository.GetAll() on det.ApplicationId equals projd.Id
                                   select new
                                   {
                                       Approval = det,
                                       ProjectId = projd.ProjectId
                                   })
                        .Where(a => a.ProjectId == input.ProjectId)
                        .ToListAsync();

            var cntdecline = 0;
            var cntrecomm = 0;

            foreach (var proj in discprojs)
            {
                if (proj.Approval.ApprovalStatusId == 1) { cntrecomm = cntrecomm + 1; };
                if (proj.Approval.ApprovalStatusId == 2) { cntdecline = cntdecline + 1; };
            }

            if ((input.ApprovalStatusId == 1 && cntrecomm == 0) || (input.ApprovalStatusId == 2 && cntrecomm > 0))
            {
                if (input.ApprovalStatusId == 1 && cntrecomm == 0) { output = output + ", There are no recommended lines cannot Recommend"; };
                if (input.ApprovalStatusId == 2 && cntrecomm > 0) { output = output + ", There are recommended lines, cannot Decline"; };
            }
            else
            {
                var dets = _discProjDetRepository.GetAll().Where(a => a.ProjectId == input.ProjectId);

                if (dets.Count() != discprojs.Count())
                {
                    output = output + ", Some lines have not been Evaluated.";
                }
                else
                {
                    var dgapproval = _discGrantApprovalRepository.GetAll().Where(a => a.ProjectId == input.ProjectId && a.ApprovalStatusId == input.ApprovalStatusId && a.ApprovalTypeId == input.ApprovalTypeId);

                    if (dgapproval.Count() == 0)
                    {
                        //var dgappr = _discGrantApprovalRepository.Get(input.Id);
                        input.DateCreated = DateTime.Now;
                        input.OutcomeDate = DateTime.Now;
                        var app = ObjectMapper.Map<DiscretionaryGrantApproval>(input);
                        await _discGrantApprovalRepository.InsertAsync(app);

                        var cappl = await _discProjRepository.FirstOrDefaultAsync(input.ProjectId);
                        cappl.DteUpd = DateTime.Now;
                        cappl.UsrUpd = input.UserId;
                        if (input.ApprovalStatusId == 1)
                        {
                            cappl.ProjectStatusID = 240;
                        }
                        if (input.ApprovalStatusId == 2)
                        {
                            cappl.ProjectStatusID = 89;
                        }
                        await _discProjRepository.UpdateAsync(cappl);
                    }
                    else
                    {
                        var dgappr = await _discGrantApprovalRepository.FirstOrDefaultAsync(dgapproval.First().Id);
                        dgappr.DteUpd = DateTime.Now;
                        dgappr.UsrUpd = input.UsrUpd;

                        await _discGrantApprovalRepository.UpdateAsync(dgappr);

                        var cappl = await _discProjRepository.FirstOrDefaultAsync(input.ProjectId);
                        cappl.DteUpd = DateTime.Now;
                        cappl.UsrUpd = input.UserId;
                        if (input.ApprovalStatusId == 1)
                        {
                            cappl.ProjectStatusID = 240;
                        }
                        if (input.ApprovalStatusId == 2)
                        {

                            cappl.ProjectStatusID = 89;
                        }
                        await _discProjRepository.UpdateAsync(cappl);
                    }
                }
            }

            return output;
        }

        public async Task<string> CreateEditResApproval(DiscretionaryGrantApprovalDto input)
        {
            var output = "";

            var discprojs = await (from det in _discResearchApprovalRepository.GetAll()
                                   join projd in _discResDetRepository.GetAll() on det.ApplicationId equals projd.Id
                                   select new
                                   {
                                       Approval = det,
                                       ProjectId = projd.ProjectId
                                   })
                        .Where(a => a.ProjectId == input.ProjectId)
                        .ToListAsync();

            var cntdecline = 0;
            var cntrecomm = 0;

            foreach (var proj in discprojs)
            {
                if (proj.Approval.ApprovalStatusId == 1) { cntrecomm = cntrecomm + 1; };
                if (proj.Approval.ApprovalStatusId == 2) { cntdecline = cntdecline + 1; };
            }

            if ((input.ApprovalStatusId == 1 && cntrecomm == 0) || (input.ApprovalStatusId == 2 && cntrecomm > 0))
            {
                if (input.ApprovalStatusId == 1 && cntrecomm == 0) { output = output + ", There are no recommended lines cannot Recommend"; };
                if (input.ApprovalStatusId == 2 && cntrecomm > 0) { output = output + ", There are recommended lines, cannot Decline"; };
            }
            else
            {
                var dets = _discResDetRepository.GetAll().Where(a => a.ProjectId == input.ProjectId);

                if (dets.Count() != discprojs.Count())
                {
                    output = output + ", Some lines have not been Evaluated.";
                }
                else
                {
                    var dgapproval = _discGrantApprovalRepository.GetAll().Where(a => a.ProjectId == input.ProjectId && a.ApprovalStatusId == input.ApprovalStatusId && a.ApprovalTypeId == input.ApprovalTypeId);

                    if (dgapproval.Count() == 0)
                    {
                        //var dgappr = _discGrantApprovalRepository.Get(input.Id);
                        input.DateCreated = DateTime.Now;
                        input.OutcomeDate = DateTime.Now;
                        var app = ObjectMapper.Map<DiscretionaryGrantApproval>(input);
                        await _discGrantApprovalRepository.InsertAsync(app);

                        var cappl = await _discProjRepository.FirstOrDefaultAsync(input.ProjectId);
                        cappl.DteUpd = DateTime.Now;
                        cappl.UsrUpd = input.UserId;
                        if (input.ApprovalStatusId == 1)
                        {
                            cappl.ProjectStatusID = 239;
                        }
                        if (input.ApprovalStatusId == 2)
                        {

                            cappl.ProjectStatusID = 89;
                        }
                        await _discProjRepository.UpdateAsync(cappl);
                    }
                    else
                    {
                        var dgappr = await _discGrantApprovalRepository.FirstOrDefaultAsync(dgapproval.First().Id);
                        dgappr.DteUpd = DateTime.Now;
                        dgappr.UsrUpd = input.UsrUpd;

                        await _discGrantApprovalRepository.UpdateAsync(dgappr);

                        var cappl = await _discProjRepository.FirstOrDefaultAsync(input.ProjectId);
                        cappl.DteUpd = DateTime.Now;
                        cappl.UsrUpd = input.UserId;
                        if (input.ApprovalStatusId == 1)
                        {
                            cappl.ProjectStatusID = 239;
                        }
                        if (input.ApprovalStatusId == 2)
                        {

                            cappl.ProjectStatusID = 89;
                        }
                        await _discProjRepository.UpdateAsync(cappl);
                    }
                }
            }

            return output;
        }

        public async Task<string> CreateEditRMResApproval(DiscretionaryGrantApprovalDto input)
        {
            var output = "";

            var discprojs = await (from det in _discResearchApprovalRepository.GetAll()
                                   join projd in _discResDetRepository.GetAll() on det.ApplicationId equals projd.Id
                                   select new
                                   {
                                       Approval = det,
                                       ProjectId = projd.ProjectId
                                   })
                        .Where(a => a.ProjectId == input.ProjectId)
                        .ToListAsync();

            var cntdecline = 0;
            var cntrecomm = 0;

            foreach (var proj in discprojs)
            {
                if (proj.Approval.ApprovalStatusId == 1) { cntrecomm = cntrecomm + 1; };
                if (proj.Approval.ApprovalStatusId == 2) { cntdecline = cntdecline + 1; };
            }

            if ((input.ApprovalStatusId == 1 && cntrecomm == 0) || (input.ApprovalStatusId == 2 && cntrecomm > 0))
            {
                if (input.ApprovalStatusId == 1 && cntrecomm == 0) { output = output + ", There are no recommended lines cannot Recommend"; };
                if (input.ApprovalStatusId == 2 && cntrecomm > 0) { output = output + ", There are recommended lines, cannot Decline"; };
            }
            else
            {
                var dets = _discResDetRepository.GetAll().Where(a => a.ProjectId == input.ProjectId);

                if (dets.Count() != discprojs.Count())
                {
                    output = output + ", Some lines have not been Evaluated.";
                }
                else
                {
                    var dgapproval = _discGrantApprovalRepository.GetAll().Where(a => a.ProjectId == input.ProjectId && a.ApprovalStatusId == input.ApprovalStatusId && a.ApprovalTypeId == input.ApprovalTypeId);

                    if (dgapproval.Count() == 0)
                    {
                        //var dgappr = _discGrantApprovalRepository.Get(input.Id);
                        input.DateCreated = DateTime.Now;
                        input.OutcomeDate = DateTime.Now;
                        var app = ObjectMapper.Map<DiscretionaryGrantApproval>(input);
                        await _discGrantApprovalRepository.InsertAsync(app);

                        var cappl = await _discProjRepository.FirstOrDefaultAsync(input.ProjectId);
                        cappl.DteUpd = DateTime.Now;
                        cappl.UsrUpd = input.UserId;
                        if (input.ApprovalStatusId == 1)
                        {
                            cappl.ProjectStatusID = 249;
                        }
                        if (input.ApprovalStatusId == 2)
                        {

                            cappl.ProjectStatusID = 89;
                        }
                        await _discProjRepository.UpdateAsync(cappl);
                    }
                    else
                    {
                        var dgappr = await _discGrantApprovalRepository.FirstOrDefaultAsync(dgapproval.First().Id);
                        dgappr.DteUpd = DateTime.Now;
                        dgappr.UsrUpd = input.UsrUpd;

                        await _discGrantApprovalRepository.UpdateAsync(dgappr);

                        var cappl = await _discProjRepository.FirstOrDefaultAsync(input.ProjectId);
                        cappl.DteUpd = DateTime.Now;
                        cappl.UsrUpd = input.UserId;
                        if (input.ApprovalStatusId == 1)
                        {
                            cappl.ProjectStatusID = 249;
                        }
                        if (input.ApprovalStatusId == 2)
                        {

                            cappl.ProjectStatusID = 89;
                        }
                        await _discProjRepository.UpdateAsync(cappl);
                    }
                }
            }

            return output;
        }

        public async Task<PagedResultDto<DiscretionaryGrantApprovalsForViewDto>> GetGrantApprovals(int ProjectId)
        {
            var det = _discGrantApprovalRepository.GetAll().Where(a => a.ProjectId == ProjectId);


            var projdet = await (from o in det
                                 select new DiscretionaryGrantApprovalsForViewDto
                                 {
                                     DiscretionaryGrantApproval = new DiscretionaryGrantApprovalDto
                                     {
                                         ProjectId = o.ProjectId,
                                         ApprovalTypeId = o.ApprovalTypeId,
                                         ApprovalStatusId = o.ApprovalStatusId,
                                         BBBEE = o.BBBEE,
                                         TaxClearance = o.TaxClearance,
                                         BankLetter = o.BankLetter,
                                         DeclarationOfInterest = o.DeclarationOfInterest,
                                         ProjectProposal = o.ProjectProposal,
                                         Comments = o.Comments,
                                         Outcome = o.Outcome,
                                         MeetingDate = o.MeetingDate,
                                         OutcomeDate = o.OutcomeDate,
                                         DateCreated = o.DateCreated,
                                         DteUpd = o.DteUpd,
                                         UsrUpd = o.UsrUpd,
                                         Id = o.Id
                                     }
                                 }).ToListAsync();

            var totalCount = det.Count();

            return new PagedResultDto<DiscretionaryGrantApprovalsForViewDto>(
                totalCount,
                projdet.ToList()
            );
        }

        public async Task<PagedResultDto<DiscretionaryGrantApprovalsDtoForView>> GetGrantApprovalsForView(int ProjectId)
        {
            var det =  (from grantappr in _discGrantApprovalRepository.GetAll()
                              join stat in _discGrantApprovalStatusRepository.GetAll() on grantappr.ApprovalStatusId equals stat.Id
                              join typ in _discGrantApprovalTypeRepository.GetAll() on grantappr.ApprovalTypeId equals typ.Id
                             select new
                             {
                                 GrantApprovals = grantappr,
                                 ApprovalDescription = typ.ApprovalDescription,
                                 StatusDescription = stat.GrantStatusDescription
                             })
                .Where(a => a.GrantApprovals.ProjectId == ProjectId);


            var projdet = await (from o in det
                                 select new DiscretionaryGrantApprovalsDtoForView
                                 {
                                     DiscretionaryGrantApprovals = new DiscretionaryGrantApprovalViewDto
                                     {
                                         ProjectId = o.GrantApprovals.ProjectId,
                                         StatusDescription = o.StatusDescription,
                                         ApprovalDescription = o.ApprovalDescription,
                                         BBBEE = o.GrantApprovals.BBBEE,
                                         TaxClearance = o.GrantApprovals.TaxClearance,
                                         BankLetter = o.GrantApprovals.BankLetter,
                                         DeclarationOfInterest = o.GrantApprovals.DeclarationOfInterest,
                                         ProjectProposal = o.GrantApprovals.ProjectProposal,
                                         Comments = o.GrantApprovals.Comments,
                                         Outcome = o.GrantApprovals.Outcome,
                                         MeetingDate = o.GrantApprovals.MeetingDate,
                                         OutcomeDate = o.GrantApprovals.OutcomeDate,
                                         Id = o.GrantApprovals.Id
                                     }
                                 }).ToListAsync();

            var totalCount = det.Count();

            return new PagedResultDto<DiscretionaryGrantApprovalsDtoForView>(
                totalCount,
                projdet.ToList()
            );
        }

        public async Task<string> ReopenReview(int projId, int userId)
        {
            string output = "";
            var dgappr = _discGrantApprovalRepository.GetAll().Where(a=>a.ProjectId == projId).FirstOrDefault();
            dgappr.DteUpd = DateTime.Now;
            dgappr.UsrUpd = userId;
            dgappr.Comments = "Reopened by User: " + userId;

            await _discGrantApprovalRepository.UpdateAsync(dgappr);

            var cappl = _discProjRepository.FirstOrDefault(projId);
            cappl.DteUpd = DateTime.Now;
            cappl.UsrUpd = userId;
            cappl.ProjectStatusID = 247;
           
            await _discProjRepository.UpdateAsync(cappl);

            return output;
        }
    }
}
