using Abp.Application.Features;
using Abp.Application.Services.Dto;
using Abp.Authorization.Users;
using Abp.Domain.Entities;
using Abp.Domain.Repositories;
using AutoMapper;
using CHIETAMIS.DiscretionaryProjects;
using CHIETAMIS.Workflows.Dto;
using Microsoft.EntityFrameworkCore;
using Org.BouncyCastle.Ocsp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CHIETAMIS.Authorization.Accounts;
//using CHIETAMIS.Authorization.Users;
using Abp.Authorization.Users;
using CHIETAMIS.Lookups;
using CHIETAMIS.MandatoryGrants;
using Newtonsoft.Json.Linq;
using System.Security.Cryptography;
using Abp.Authorization.Users;
using CHIETAMIS.Authorization.Users;
using System.Drawing;
using System.Collections.Immutable;
using System.Collections;
using Org.BouncyCastle.Utilities;

namespace CHIETAMIS.Workflows
{
    public class WorkflowAppService : CHIETAMISAppServiceBase
    {
        private readonly IRepository<wfTarget> _targetRepository;
        private readonly IRepository<wfActivity> _activityRepository;
        private readonly IRepository<wfAction> _actionRepository;
        private readonly IRepository<wfActionTarget> _actionTargetRepository;
        private readonly IRepository<wfActionType> _actionTypeRepository;
        private readonly IRepository<wfActivityTarget> _activityTargetRepository;
        private readonly IRepository<wfActivityType> _activityTypeRepository;
        private readonly IRepository<wfProcess> _processRepository;
        private readonly IRepository<wfRequest> _requestRepository;
        private readonly IRepository<wfRequestAction> _requestActionRepository;
        private readonly IRepository<wfRequestData> _requestDataRepository;
        private readonly IRepository<wfRequestFile> _requestFileRepository;
        private readonly IRepository<wfRequestNote> _requestNoteRepository;
        private readonly IRepository<wfRequestStakeholder> _requestStakeholderRepository;
        private readonly IRepository<wfState> _stateRepository;
        private readonly IRepository<wfStateActivity> _stateActivityRepository;
        private readonly IRepository<wfStateType> _stateTypeRepository;
        private readonly IRepository<wfTimer> _timerRepository;
        private readonly IRepository<wfTimerResult> _timerResultRepository;
        private readonly IRepository<wfTimerDuration> _timerDurationRepository;
        private readonly IRepository<wfTransition> _transitionRepository;
        private readonly IRepository<wfTransitionAction> _transitionActionRepository;
        private readonly IRepository<wfTransitionActivity> _transitionActivityRepository;
        private readonly IRepository<wfTransitionTimer> _transitionTimerRepository;
        private readonly IRepository<DiscretionaryProject> _projRepository;
        private readonly IRepository<MandatoryApplication> _mgRepository;
        private readonly IRepository<RegionRM> _rmRepository;
        private readonly IRepository<RegionRSA> _rsaRepository;
        private readonly IRepository<User, long> _userRepository;
        private readonly IRepository<SpecialistProject> _specRepository;

        public WorkflowAppService(IRepository<wfTarget> targetRepository,
                                IRepository<wfActivity> activityRepository,
                                IRepository<wfAction> actionRepository,
                                IRepository<wfActionTarget> actionTargetRepository,
                                IRepository<wfActionType> actionTypeRepository,
                                IRepository<wfActivityTarget> activityTargetRepository,
                                IRepository<wfActivityType> activityTypeRepository,
                                IRepository<wfProcess> processRepository,
                                IRepository<wfRequest> requestRepository,
                                IRepository<wfRequestAction> requestActionRepository,
                                IRepository<wfRequestData> requestDataRepository,
                                IRepository<wfRequestFile> requestFileRepository,
                                IRepository<wfRequestNote> requestNoteRepository,
                                IRepository<wfRequestStakeholder> requestStakeholderRepository,
                                IRepository<wfState> stateRepository,
                                IRepository<wfStateActivity> stateActivityRepository,
                                IRepository<wfStateType> stateTypeRepository,
                                IRepository<wfTimer> timerRepository,
                                IRepository<wfTimerResult> timerResultRepository,
                                IRepository<wfTimerDuration> timerDurationRepository,
                                IRepository<wfTransition> transitionRepository,
                                IRepository<wfTransitionAction> transitionActionRepository,
                                IRepository<wfTransitionActivity> transitionActivityRepository,
                                IRepository<wfTransitionTimer> transitionTimerRepository,
                                IRepository<DiscretionaryProject> projRepository,
                                IRepository<RegionRM> rmRepository,
                                IRepository<RegionRSA> rsaRepository,
                                IRepository<MandatoryApplication> mgRepository,
                                IRepository<User, long> userRepository,
                                IRepository<SpecialistProject> specRepository)
        {
            _targetRepository = targetRepository;
            _activityRepository = activityRepository;
            _actionRepository = actionRepository;
            _actionTypeRepository = actionTypeRepository;
            _actionTargetRepository = actionTargetRepository;
            _activityTargetRepository = activityTargetRepository;
            _activityTypeRepository = activityTypeRepository;
            _processRepository = processRepository;
            _requestRepository = requestRepository;
            _requestActionRepository = requestActionRepository;
            _requestDataRepository = requestDataRepository;
            _requestFileRepository = requestFileRepository;
            _requestNoteRepository = requestNoteRepository;
            _requestStakeholderRepository = requestStakeholderRepository;
            _stateRepository =  stateRepository;
            _stateActivityRepository = stateActivityRepository;
            _stateTypeRepository = stateTypeRepository;
            _timerRepository = timerRepository;
            _timerResultRepository = timerResultRepository;
            _timerDurationRepository = timerDurationRepository;
            _transitionRepository = transitionRepository;
            _transitionActionRepository = transitionActionRepository;
            _transitionActivityRepository = transitionActivityRepository;
            _transitionTimerRepository = transitionTimerRepository;
            _projRepository = projRepository;
            _rsaRepository= rsaRepository;
            _rmRepository= rmRepository;
            _mgRepository = mgRepository;
            _userRepository = userRepository;
            _specRepository= specRepository;
        }

        public int ValueInArray(string[] lst, string val)
        {
            int index = -1;
            for (int i = 0; i < lst.Length; i++) { 
                if (lst[i].Trim() == val.Trim()) { 
                    index = i; break; 
                } 
            }
            return index;
        }

        public async Task<wfActionDto> ActionByName(int processid, string name)
        {
            if (name.Length == 0)
            {
                return null;
            }

            var actn = _actionRepository.GetAll().Where(x=>x.ProcessId == processid && x.Name == name).FirstOrDefault();
            if (actn == null)
            {
                return null;
            }

            return ObjectMapper.Map<wfActionDto>(actn);
        }

        public async Task<wfActivityDto> ActivityByName(int processid, string name)
        {
            if (name.Length == 0)
            {
                return null;
            }

            var actvt = _activityRepository.GetAll().Where(x => x.ProcessId == processid && x.Name == name).FirstOrDefault();
            if (actvt == null)
            {
                return null;
            }

            return ObjectMapper.Map<wfActivityDto>(actvt);
        }

        public async Task<wfStateDto> StateByName(int processid, string name)
        {
            if (name.Length == 0)
            {
                return null;
            }

            var stat = _stateRepository.GetAll().Where(x => x.ProcessId == processid && x.Name == name).FirstOrDefault();
            if (stat == null)
            {
                return null;
            }

            return ObjectMapper.Map<wfStateDto>(stat);
        }

        public async Task<wfProcessDto> ProcessByName(string name)
        {
            if (name.Length == 0)
            {
                return null;
            }

            var proc = _processRepository.GetAll().Where(x => x.Name == name).FirstOrDefault();
            if (proc == null)
            {
                return null;
            }

            return ObjectMapper.Map<wfProcessDto>(proc);
        }

        public async Task<wfRequestDto> RequestByTitle(string input)
        {
            if (input.Length == 0)
            {
                return null;
            }

            var req = _requestRepository.GetAll().Where(x => x.Title == input).FirstOrDefault();
            if (req == null)
            {
                return null;
            }

            return ObjectMapper.Map<wfRequestDto>(req);
        }
        public async Task<wfActionDto> ActionById(int processid, int input)
        {
            var actn = _actionRepository.GetAll().Where(x => x.ProcessId == processid && x.Id == input).FirstOrDefault();
            if (actn == null)
            {
                return null;
            }

            return ObjectMapper.Map<wfActionDto>(actn);
        }

        public async Task<wfActivityDto> ActivityById(int processid, int input)
        {
            var actvt = _activityRepository.GetAll().Where(x => x.ProcessId == processid && x.Id == input).FirstOrDefault();
            if (actvt == null)
            {
                return null;
            }

            return ObjectMapper.Map<wfActivityDto>(actvt);
        }

        public async Task<wfStateDto> StateById(int processid, int input)
        {
            var stat = _stateRepository.GetAll().Where(x => x.ProcessId == processid && x.Id == input).FirstOrDefault();
            if (stat == null)
            {
                return null;
            }

            return ObjectMapper.Map<wfStateDto>(stat);
        }

        public async Task<wfStateActivityDto> StateActivityById(int activityid, int stateid)
        {
            var actvst = _stateActivityRepository.GetAll().Where(x => x.ActivityId == activityid && x.StateId == stateid).FirstOrDefault();
            if (actvst == null)
            {
                return null;
            }

            return ObjectMapper.Map<wfStateActivityDto>(actvst);
        }

        public async Task<wfActionTargetDto> ActionTargetById(int processid, int actionid, int targetid)
        {
            var actntgt = _actionTargetRepository.GetAll().Where(x => x.ActionId == actionid && x.TargetId == targetid).FirstOrDefault();
            if (actntgt == null)
            {
                return null;
            }

            return ObjectMapper.Map<wfActionTargetDto>(actntgt);
        }

        public async Task<wfActivityTargetDto> ActivityTargetById(int processid, int activityid, int targetid)
        {
            var actvttgt = _activityTargetRepository.GetAll().Where(x => x.ActivityId == activityid && x.TargetId == targetid).FirstOrDefault();
            if (actvttgt == null)
            {
                return null;
            }

            return ObjectMapper.Map<wfActivityTargetDto>(actvttgt);
        }

        public async Task<wfRequestDto> RequestById(int processid, int requestid)
        {
            var req = _requestRepository.GetAll().Where(x => x.ProcessId == requestid && x.Id == requestid).FirstOrDefault();
            if (req == null)
            {
                return null;
            }

            return ObjectMapper.Map<wfRequestDto>(req);
        }

        public async Task<wfRequestDataDto> RequestDataById(int requestdataid)
        {
            var req = _requestDataRepository.GetAll().Where(x => x.Id == requestdataid).FirstOrDefault();
            if (req == null)
            {
                return null;
            }

            return ObjectMapper.Map<wfRequestDataDto>(req);
        }

        public async Task<wfRequestNoteDto> RequestNoteById(int requestnoteid)
        {
            var req = _requestNoteRepository.GetAll().Where(x => x.Id == requestnoteid).FirstOrDefault();
            if (req == null)
            {
                return null;
            }

            return ObjectMapper.Map<wfRequestNoteDto>(req);
        }

        public async Task<wfRequestFileDto> RequestFileById(int requestfileid)
        {
            var req = _requestFileRepository.GetAll().Where(x => x.Id == requestfileid).FirstOrDefault();
            if (req == null)
            {
                return null;
            }

            return ObjectMapper.Map<wfRequestFileDto>(req);
        }

        public async Task<wfRequestStakeholderDto> RequestStakeHolderById(int requestid, int stakeholderid)
        {
            var req = _requestStakeholderRepository.GetAll().Where(x => x.RequestId == requestid && x.UserId == stakeholderid).FirstOrDefault();
            if (req == null)
            {
                return null;
            }

            return ObjectMapper.Map<wfRequestStakeholderDto>(req);
        }

        public async Task<PagedResultDto<PagedUserRequestActivities>> GetRequestActivitiesForUser (int UserId, string groups)
        {
            groups = groups.Replace(" ", "");
            var grouplist = groups.Split(",");
            grouplist = grouplist.Skip(1).ToArray();
            var reqactusr = (from req in _requestRepository.GetAll()
                join stat in _stateRepository.GetAll() on req.CurrentStateId equals stat.Id
                join proc in _processRepository.GetAll() on req.ProcessId equals proc.Id
                join stata in _stateActivityRepository.GetAll() on stat.Id equals stata.StateId
                join act in _activityRepository.GetAll() on stata.ActivityId equals act.Id
                join acttarg in _activityTargetRepository.GetAll() on act.Id equals acttarg.ActivityId

            select new
            {
                ProcessId = proc.Id,
                ProcessName = proc.Name,
                ActivityName = act.Name,
                ActivityId = act.Id,
                RequestId = req.Id,
                RequestTitle = req.Title,
                RequestPath = req.RequestPath,
                CurrentStateId = req.CurrentStateId,
                CurrentState = stat.Description,
                NextState = stat.Description,
                Username = req.Username,
                GroupId = acttarg.GroupId,
                DateRequested = req.DateRequested,
                MgUsr = "0",
                DGUser = "0"
            })
            .Where(a => a.ProcessName == "Blank")
            .Distinct()
            .ToList();

            var reqactusr2 = reqactusr;

            //if (ValueInArray(grouplist, " 5") != -1)
            if (Array.Exists(grouplist, element => element == "5"))
                {
                reqactusr = (from req in _requestRepository.GetAll()
                             join stat in _stateRepository.GetAll() on req.CurrentStateId equals stat.Id
                             join proc in _processRepository.GetAll() on req.ProcessId equals proc.Id
                             join stata in _stateActivityRepository.GetAll() on stat.Id equals stata.StateId
                             join act in _activityRepository.GetAll() on stata.ActivityId equals act.Id
                             join acttarg in _activityTargetRepository.GetAll() on act.Id equals acttarg.ActivityId
                             join dat1 in _requestDataRepository.GetAll().Where(a => a.Name == "ProjectId") on req.Id equals dat1.RequestId into dt1
                             from dat1s in dt1.DefaultIfEmpty()
                             join dat2 in _requestDataRepository.GetAll().Where(a => a.Name == "ApplicationId") on req.Id equals dat2.RequestId into dt2
                             from dat2s in dt2.DefaultIfEmpty()
                             join proj in _projRepository.GetAll().Where(a => a.RSAId == UserId) on dat1s.Value equals proj.Id.ToString() into p
                             from projs in p.DefaultIfEmpty()
                             join mg in _mgRepository.GetAll().Where(a => a.RSAId == UserId) on dat2s.Value equals mg.Id.ToString() into m
                             from mgs in m.DefaultIfEmpty()
                             select new
                             {
                                 ProcessId = proc.Id,
                                 ProcessName = proc.Name,
                                 ActivityName = act.Name,
                                 ActivityId = act.Id,
                                 RequestId = req.Id,
                                 RequestTitle = req.Title,
                                 RequestPath = req.RequestPath,
                                 CurrentStateId = req.CurrentStateId,
                                 CurrentState = stat.Description,
                                 NextState = stat.Description,
                                 Username = req.Username,
                                 GroupId = acttarg.GroupId,
                                 DateRequested = req.DateRequested,
                                 MgUsr = mgs.RSAId.ToString(),
                                 DGUser = projs.RSAId.ToString()
                             })
                .Where(a => ((a.ProcessName == "Discretionary Grants" && a.DGUser == UserId.ToString()) || (a.ProcessName == "Mandatory Grants" && a.MgUsr == UserId.ToString())) && groups.Contains(a.GroupId.ToString()))
                .Distinct()
                .ToList();

            }
            else
            {
                if (Array.Exists(grouplist, element => element == "1006"))
                {
                    reqactusr = (from req in _requestRepository.GetAll()
                                 join stat in _stateRepository.GetAll() on req.CurrentStateId equals stat.Id
                                 join proc in _processRepository.GetAll() on req.ProcessId equals proc.Id
                                 join stata in _stateActivityRepository.GetAll() on stat.Id equals stata.StateId
                                 join act in _activityRepository.GetAll() on stata.ActivityId equals act.Id
                                 join acttarg in _activityTargetRepository.GetAll() on act.Id equals acttarg.ActivityId
                                 join dat1 in _requestDataRepository.GetAll().Where(a => a.Name == "ProjectId") on req.Id equals dat1.RequestId 
                                 join proj in _projRepository.GetAll() on dat1.Value equals proj.Id.ToString() 
                                 join rsa in _rsaRepository.GetAll() on proj.RSAId equals rsa.UserID
                                 join rm in _rmRepository.GetAll().Where(a => a.UserId == UserId) on rsa.RegionID equals rm.RegionId
                                 select new
                                 {
                                     ProcessId = proc.Id,
                                     ProcessName = proc.Name,
                                     ActivityName = act.Name,
                                     ActivityId = act.Id,
                                     RequestId = req.Id,
                                     RequestTitle = req.Title,
                                     RequestPath = req.RequestPath,
                                     CurrentStateId = req.CurrentStateId,
                                     CurrentState = stat.Description,
                                     NextState = stat.Description,
                                     Username = req.Username,
                                     GroupId = acttarg.GroupId,
                                     DateRequested = req.DateRequested,
                                     MgUsr = "0",
                                     DGUser = rm.UserId.ToString()
                                 })
                    .Where(a => (a.ProcessName == "Discretionary Grants" && a.DGUser == UserId.ToString()) && groups.Contains(a.GroupId.ToString()))
                    .Distinct()
                    .ToList();
                }
                else
                {
                    if (Array.Exists(grouplist, element => element == "1009"))
                        {
                        reqactusr = (from req in _requestRepository.GetAll()
                                     join stat in _stateRepository.GetAll() on req.CurrentStateId equals stat.Id
                                     join proc in _processRepository.GetAll() on req.ProcessId equals proc.Id
                                     join stata in _stateActivityRepository.GetAll() on stat.Id equals stata.StateId
                                     join act in _activityRepository.GetAll() on stata.ActivityId equals act.Id
                                     join acttarg in _activityTargetRepository.GetAll() on act.Id equals acttarg.ActivityId
                                     join dat1 in _requestDataRepository.GetAll().Where(a => a.Name == "ProjectId") on req.Id equals dat1.RequestId
                                     join proj in _projRepository.GetAll() on dat1.Value equals proj.Id.ToString()
                                     join sp in _specRepository.GetAll().Where(a => a.UserId == UserId) on proj.ProjectTypeId equals sp.ProjectTypeId
                                     select new
                                     {
                                         ProcessId = proc.Id,
                                         ProcessName = proc.Name,
                                         ActivityName = act.Name,
                                         ActivityId = act.Id,
                                         RequestId = req.Id,
                                         RequestTitle = req.Title,
                                         RequestPath = req.RequestPath,
                                         CurrentStateId = req.CurrentStateId,
                                         CurrentState = stat.Description,
                                         NextState = stat.Description,
                                         Username = req.Username,
                                         GroupId = acttarg.GroupId,
                                         DateRequested = req.DateRequested,
                                         MgUsr = "0",
                                         DGUser = UserId.ToString()
                                     })
                       .Where(a => (a.ProcessName == "Discretionary Grants") && groups.Contains(a.GroupId.ToString()))
                       .Distinct()
                       .ToList();

                        reqactusr2 = (from req in _requestRepository.GetAll()
                                     join stat in _stateRepository.GetAll() on req.CurrentStateId equals stat.Id
                                     join proc in _processRepository.GetAll() on req.ProcessId equals proc.Id
                                     join stata in _stateActivityRepository.GetAll() on stat.Id equals stata.StateId
                                     join act in _activityRepository.GetAll() on stata.ActivityId equals act.Id
                                     join acttarg in _activityTargetRepository.GetAll() on act.Id equals acttarg.ActivityId
                                     select new
                                     {
                                         ProcessId = proc.Id,
                                         ProcessName = proc.Name,
                                         ActivityName = act.Name,
                                         ActivityId = act.Id,
                                         RequestId = req.Id,
                                         RequestTitle = req.Title,
                                         RequestPath = req.RequestPath,
                                         CurrentStateId = req.CurrentStateId,
                                         CurrentState = stat.Description,
                                         NextState = stat.Description,
                                         Username = req.Username,
                                         GroupId = acttarg.GroupId,
                                         DateRequested = req.DateRequested,
                                         MgUsr = "0",
                                         DGUser = req.UserId.ToString()
                                     })
                        .Where(a => (a.ProcessName == "Discretionary Grants" && a.DGUser == UserId.ToString()) && groups.Contains(a.GroupId.ToString()))
                        .Distinct()
                        .ToList();

                        reqactusr2.ForEach(p => reqactusr.Add(p));

                    } else
                    {

                        reqactusr = (from req in _requestRepository.GetAll()
                                     join stat in _stateRepository.GetAll() on req.CurrentStateId equals stat.Id
                                     join proc in _processRepository.GetAll() on req.ProcessId equals proc.Id
                                     join stata in _stateActivityRepository.GetAll() on stat.Id equals stata.StateId
                                     join act in _activityRepository.GetAll() on stata.ActivityId equals act.Id
                                     join acttarg in _activityTargetRepository.GetAll() on act.Id equals acttarg.ActivityId
                                     select new
                                     {
                                         ProcessId = proc.Id,
                                         ProcessName = proc.Name,
                                         ActivityName = act.Name,
                                         ActivityId = act.Id,
                                         RequestId = req.Id,
                                         RequestTitle = req.Title,
                                         RequestPath = req.RequestPath,
                                         CurrentStateId = req.CurrentStateId,
                                         CurrentState = stat.Description,
                                         NextState = stat.Description,
                                         Username = req.Username,
                                         GroupId = acttarg.GroupId,
                                         DateRequested = req.DateRequested,
                                         MgUsr = "0",
                                         DGUser = "0"
                                     })
                        .Where(a => (a.ProcessName == "Discretionary Grants" || a.ProcessName == "Mandatory Grants") && groups.Contains("," + a.GroupId.ToString())) // || (a.TargetName == "Requester" && a.UserId == UserId)))
                        .Distinct()
                        .ToList();
                    }
                }
            }

            var reqacts = from o in reqactusr
                select new PagedUserRequestActivities()
                {
                    UserRequestActivities = new UserRequestActivitiesDto
                    {
                        ProcessId = o.ProcessId,
                        ProcessName = o.ProcessName,
                        ActivityId = o.ActivityId,
                        ActivityName = o.ActivityName,
                        RequestId = o.RequestId,
                        RequestTitle = o.RequestTitle,
                        RequestPath = o.RequestPath,
                        CurrentStateId = o.CurrentStateId,
                        CurrentState = o.CurrentState,
                        NextState = o.NextState,
                        Username = o.Username,
                        GroupId = o.GroupId,
                        DateRequested = o.DateRequested,
                    },

                    RequestData = ObjectMapper.Map <List<wfRequestDataDto>>(_requestDataRepository.GetAll().Where(x => x.RequestId == o.RequestId))
                };

            var totalCount = reqacts.Count();

            return new PagedResultDto<PagedUserRequestActivities>(
                totalCount,
                reqacts.ToList()
            );
        }

        public async Task<PagedResultDto<PagedUserRequestActions>> GetRequestActionsForUser(int RequestId, int UserId, string groups)
        {
            var reqactusr = await (from req in _requestRepository.GetAll()
                                   join proc in _processRepository.GetAll() on req.ProcessId equals proc.Id
                                   join reqact in _requestActionRepository.GetAll() on req.Id equals reqact.RequestId
                                   join act in _actionRepository.GetAll() on reqact.ActionId equals act.Id
                                   join acttarg in _actionTargetRepository.GetAll() on act.Id equals acttarg.ActionId
                                   select new
                                   {
                                       ProcessId = proc.Id,
                                       ProcessName = proc.Name,
                                       ActionName = act.Name,
                                       ActionId = act.Id,
                                       RequestId = req.Id,
                                       RequestTitle = req.Title,
                                       CurrentStateId = req.CurrentStateId,
                                       UserId = req.UserId,
                                       GroupId = acttarg.GroupId,
                                       ActiveYN = reqact.IsActive
                                   })
                     .Where(a => a.RequestId == RequestId && groups.Contains(", " + a.GroupId.ToString()) && a.ActiveYN)
                     .Distinct()
                     .ToListAsync();

            var reqacts = from o in reqactusr
                          select new PagedUserRequestActions()
                          {
                              UserRequestActions = new UserRequestActionsDto
                              {
                                  ProcessId = o.ProcessId,
                                  ProcessName = o.ProcessName,
                                  ActionId = o.ActionId,
                                  ActionName = o.ActionName ,
                                  RequestId = o.RequestId,
                                  RequestTitle = o.RequestTitle,
                                  UserId = o.UserId,
                              },

                              RequestData = ObjectMapper.Map<List<wfRequestDataDto>>(_requestDataRepository.GetAll().Where(x => x.RequestId == RequestId)),
                              RequestNote = ObjectMapper.Map<List<wfRequestNoteDto>>(_requestNoteRepository.GetAll().Where(x => x.RequestId == RequestId))
                          };

            var totalCount = reqacts.Count();

            return new PagedResultDto<PagedUserRequestActions>(
                totalCount,
                reqacts.ToList()
            );
        }

        public async Task<PagedResultDto<wfRequestCommentsPaged>> GetRequestComments(int RequestId)
        {
            var reqactusr = await (from req in _requestRepository.GetAll()
                                   join dat in _requestDataRepository.GetAll().Where(a=>a.Name == "Comment") on req.Id equals dat.RequestId
                                   join u in _userRepository.GetAll() on dat.UserId equals u.Id
                                   select new
                                   {
                                       Comment = dat.Value,
                                       User = u.FullName,
                                       DateCreated = dat.DateCreated,
                                       RequestId = req.Id
                                   })
                     .Where(a => a.RequestId == RequestId)
                     .Distinct()
                     .OrderBy(a => a.DateCreated)
                     .ToListAsync();

            var reqacts = from o in reqactusr
                          select new wfRequestCommentsPaged()
                          {
                              RequestComments = new wfRequestComments
                              {
                                  Comment = o.Comment,
                                  User = o.User,
                                  DateCreated = o.DateCreated
                              },
                          };

            var totalCount = reqacts.Count();

            return new PagedResultDto<wfRequestCommentsPaged>(
                totalCount,
                reqacts.ToList()
            );
        }

        public async Task<PagedResultDto<wfRequestApprovalsPaged>> GetRequestApprovals(int RequestId)
        {
            var reqactusr = await (from req in _requestRepository.GetAll()
                                   join ract in _requestActionRepository.GetAll().Where(a=>a.IsComplete == true) on req.Id equals ract.RequestId
                                   join trans in _transitionRepository.GetAll() on ract.TransitionId equals trans.Id
                                   join st in _stateRepository.GetAll() on trans.NextStateId equals st.Id
                                   join u in _userRepository.GetAll() on (long)ract.UserActioned equals u.Id
                                   select new
                                   {
                                       Status = st.Description,
                                       User = u.FullName,
                                       DateActioned = ract.DateActioned,
                                       RequestId = req.Id
                                   })
                     .Where(a => a.RequestId == RequestId)
                     .Distinct()
                     .OrderBy(a=>a.DateActioned)
                     .ToListAsync();

            var reqappr = from o in reqactusr
                          select new wfRequestApprovalsPaged()
                          {
                              RequestApprovals = new wfRequestApprovals
                              {
                                  Status = o.Status,
                                  User = o.User,
                                  DateActioned = o.DateActioned
                              },
                          };

            var totalCount = reqappr.Count();

            return new PagedResultDto<wfRequestApprovalsPaged>(
                totalCount,
                reqappr.ToList()
            );
        }
        public async Task PerformRequestAction(int RequestId, int ActionId, int UserId, string message)
        {
            var rq = _requestRepository.Get(RequestId);
            var reqact = _requestActionRepository.GetAll().Where(a=>a.RequestId == RequestId && a.ActionId == ActionId && a.IsActive == true).FirstOrDefault();
            reqact.IsActive = false;
            reqact.UserActioned = UserId;
            reqact.DateActioned= DateTime.Now;
            reqact.IsComplete = true;
            var tupd = ObjectMapper.Map<wfRequestAction>(reqact);
            await _requestActionRepository.UpdateAsync(tupd);

            var pnd = _requestActionRepository.GetAll().Where(a => a.RequestId == RequestId && a.TransitionId == reqact.TransitionId && a.Id != reqact.Id & a.IsActive == true).Count();

            if (pnd == 0)
            { 
                //Change remain to inactive
                var rem = _requestActionRepository.GetAll().Where(a=>a.RequestId == RequestId && a.Id != reqact.Id && a.IsActive== true).ToList();
                foreach(var r in rem)
                {
                    r.IsActive = false;
                    var rpd = ObjectMapper.Map<wfRequestAction>(r);
                    await _requestActionRepository.UpdateAsync(rpd);
                }

                //Change status of the Request
                var trans = _transitionRepository.Get(reqact.TransitionId);
                rq.CurrentStateId = trans.NextStateId;
                var rupd = ObjectMapper.Map<wfRequest>(rq);
                await _requestRepository.UpdateAsync(rupd);

                var trans2 = _transitionRepository.GetAll().Where(a => a.CurrentStateId == trans.NextStateId).ToList();

                foreach (var t in trans2)
                {
                    var transact = _transitionActionRepository.GetAll().Where(x => x.TransitionId == t.Id).ToList();
                    foreach (var tract in transact.ToList())
                    {
                        var rqact = new wfRequestActionDto();
                        rqact.DateCreated = DateTime.Now;
                        rqact.RequestId = RequestId;
                        rqact.ActionId = tract.ActionId;
                        rqact.DateActioned = DateTime.Now;
                        rqact.IsActive = true;
                        rqact.IsComplete = false;
                        rqact.UserActioned = UserId;
                        rqact.UserId = UserId;
                        rqact.TransitionId = tract.TransitionId; 

                        var ra = ObjectMapper.Map<wfRequestAction>(rqact);
                        await _requestActionRepository.InsertAsync(ra);
                    }
                }
            }

            if (message != null)
            {
                var rd = new wfRequestDataDto();

                rd.Name = "Comment";
                rd.Value = message;
                rd.UserId = UserId;
                rd.RequestId = RequestId; // request.Id;
                rd.DateCreated = DateTime.Now;

                var rqdata = ObjectMapper.Map<wfRequestData>(rd);
                await _requestDataRepository.InsertAsync(rqdata);
            }
        }

        public async Task<wfTargetDto> TargetById(int targetid)
        {
            var targ = _targetRepository.GetAll().Where(x => x.Id == targetid).FirstOrDefault();
            if (targ == null)
            {
                return null;
            }

            return ObjectMapper.Map<wfTargetDto>(targ);
        }

        public async Task<wfTargetDto> TargetByName(string input)
        {
            var targ = _targetRepository.GetAll().Where(x => x.Name == input).FirstOrDefault();
            if (targ == null)
            {
                return null;
            }

            return ObjectMapper.Map<wfTargetDto>(targ);
        }

        public async Task<wfTimerDto> TimerById(int processid, int requestid, int transitionid)
        {
            var tmr = _timerRepository.GetAll().Where(x => x.ProcessId == processid && x.RequestId == requestid && x.TransitionId == transitionid).FirstOrDefault();
            if (tmr == null)
            {
                return null;
            }

            return ObjectMapper.Map<wfTimerDto>(tmr);
        }

        public async Task<wfTimerDurationDto> TimerDurationById(int timerdurid)
        {
            var tmrd = _timerDurationRepository.GetAll().Where(x => x.Id == timerdurid).FirstOrDefault();
            if (tmrd == null)
            {
                return null;
            }

            return ObjectMapper.Map<wfTimerDurationDto>(tmrd);
        }

        public async Task<wfTimerDurationDto> TimerDurationByName(string input)
        {
            var tmrd = _timerDurationRepository.GetAll().Where(x => x.DurationType == input).FirstOrDefault();
            if (tmrd == null)
            {
                return null;
            }

            return ObjectMapper.Map<wfTimerDurationDto>(tmrd);
        }

        public async Task<wfTimerResultDto> TimerResultById(int timerresid)
        {
            var tmrres = _timerResultRepository.GetAll().Where(x => x.Id == timerresid).FirstOrDefault();
            if (tmrres == null)
            {
                return null;
            }

            return ObjectMapper.Map<wfTimerResultDto>(tmrres);
        }

        public async Task<wfTimerResultDto> TimerResultByName(string input)
        {
            var tmrd = _timerResultRepository.GetAll().Where(x => x.TimerResult == input).FirstOrDefault();
            if (tmrd == null)
            {
                return null;
            }

            return ObjectMapper.Map<wfTimerResultDto>(tmrd);
        }

        public async Task<wfTransitionDto> TransitionById(int transid)
        {
            var trans = _transitionRepository.GetAll().Where(x => x.Id == transid).FirstOrDefault();
            if (trans == null)
            {
                return null;
            }

            return ObjectMapper.Map<wfTransitionDto>(trans);
        }

        public async Task<wfTransitionActionDto> TransitionActionById(int tranactid)
        {
            var transact = _transitionActionRepository.GetAll().Where(x => x.Id == tranactid).FirstOrDefault();
            if (transact == null)
            {
                return null;
            }

            return ObjectMapper.Map<wfTransitionActionDto>(transact);
        }

        public async Task<wfTransitionTimerDto> TransitionTimerById(int trantmrid)
        {
            var transact = _transitionTimerRepository.GetAll().Where(x => x.Id == trantmrid).FirstOrDefault();
            if (transact == null)
            {
                return null;
            }

            return ObjectMapper.Map<wfTransitionTimerDto>(transact);
        }

        public async Task<wfTransitionTimerDto> TransitionTimerByTransId(int tranid)
        {
            var transact = _transitionTimerRepository.GetAll().Where(x => x.TransitionId == tranid).FirstOrDefault();
            if (transact == null)
            {
                return null;
            }

            return ObjectMapper.Map<wfTransitionTimerDto>(transact);
        }

        public async Task<PagedResultDto<TransitionActionPagedDto>> TransitionActionsById(int transid)
        {
            var transact = _transitionActionRepository.GetAll().Where(x => x.TransitionId == transid);
            if (transact == null)
            {
                return null;
            }

            var transactdat = from o in transact
                         select new TransitionActionPagedDto()
                         {
                             TransitionAction = new wfTransitionActionDto
                             {
                                 Id = o.Id,
                                 ActionId = o.ActionId,
                                 TransitionId = o.TransitionId,
                                 UserId = o.UserId,
                                 DateCreated = o.DateCreated
                             }
                         };

            var totalCount = transactdat.Count();
            return new PagedResultDto<TransitionActionPagedDto>(
                totalCount,
                transactdat.ToList()
            );
        }

        public async Task<wfTransitionActivityDto> TransitionActivityById(int tranactid)
        {
            var transact = _transitionActivityRepository.GetAll().Where(x => x.Id == tranactid).FirstOrDefault();
            if (transact == null)
            {
                return null;
            }

            return ObjectMapper.Map<wfTransitionActivityDto>(transact);
        }

        public async Task<PagedResultDto<TransitionActivityPagedDto>> TransitionActivitiesById(int transid)
        {
            var transact = _transitionActivityRepository.GetAll().Where(x => x.TransitionId == transid);
            if (transact == null)
            {
                return null;
            }

            var transactdat = from o in transact
                              select new TransitionActivityPagedDto()
                              {
                                  TransitionActivity = new wfTransitionActivityDto
                                  {
                                      Id = o.Id,
                                      ActivityId = o.ActivityId,
                                      TransitionId = o.TransitionId,
                                      UserId = o.UserId,
                                      DateCreated = o.DateCreated
                                  }
                              };

            var totalCount = transactdat.Count();
            return new PagedResultDto<TransitionActivityPagedDto>(
                totalCount,
                transactdat.ToList()
            );
        }

        public async Task<PagedResultDto<RequestDataPagedDto>> RequestDataListById(int requestid)
        {
            var req = _requestDataRepository.GetAll().Where(x => x.RequestId == requestid);
            if (req == null)
            {
                return null;
            }

            var reqdat = from o in req
                         select new RequestDataPagedDto()
                         {
                             RequestDataDto = new wfRequestDataDto
                             {
                                 Id = o.Id,
                                 UserId = o.UserId,
                                 Name = o.Name,
                                 Value = o.Value,
                                 RequestId = o.RequestId,
                                 DateCreated = o.DateCreated
                             }
                         };

            var totalCount = reqdat.Count();
            return new PagedResultDto<RequestDataPagedDto>(
                totalCount,
                reqdat.ToList()
            );
        }

        public async Task<wfRequestDto> RequestByRequestData(string name, string value)
        {
            var reqd = _requestDataRepository.GetAll().Where(x => x.Name == name && x.Value == value).OrderByDescending(x=>x.Id).FirstOrDefault();
            wfRequestDataDto req;

            if (reqd == null)
            {
                return null;
            }

            var treq = _requestRepository.GetAll().Where(x=>x.Id == reqd.RequestId).FirstOrDefault();

            return ObjectMapper.Map<wfRequestDto>(treq);

        }

        public async Task<PagedResultDto<RequestFilePagedDto>> RequestFileListById(int requestid)
        {
            var req = _requestFileRepository.GetAll().Where(x => x.RequestId == requestid);
            if (req == null)
            {
                return null;
            }

            var reqfile = from o in req
                         select new RequestFilePagedDto()
                         {
                             RequestFile = new wfRequestFileDto
                             {
                                 Id = o.Id,
                                 UserId = o.UserId,
                                 RequestId = o.RequestId,
                                 DateUploaded = o.DateUploaded,
                                 FileName = o.FileName,
                                 MIMEType = o.MIMEType,
                                 Filelocation = o.Filelocation
                             }
                         };

            var totalCount = reqfile.Count();
            return new PagedResultDto<RequestFilePagedDto>(
                totalCount,
                reqfile.ToList()
            );
        }

        public async Task<PagedResultDto<RequestNotePagedDto>> RequestNoteListById(int requestid)
        {
            var req = _requestNoteRepository.GetAll().Where(x => x.RequestId == requestid);
            if (req == null)
            {
                return null;
            }

            var reqnote = from o in req
                          select new RequestNotePagedDto()
                          {
                              RequestNote = new wfRequestNoteDto
                              {
                                  Id = o.Id,
                                  UserId = o.UserId,
                                  RequestId = o.RequestId,
                                  Note = o.Note,
                                  DateCreated = o.DateCreated
                              }
                          };

            var totalCount = reqnote.Count();
            return new PagedResultDto<RequestNotePagedDto>(
                totalCount,
                reqnote.ToList()
            );
        }

        public async Task<PagedResultDto<UserRequestsPagedDto>> RequestsByUserId(int processid, int userid)
        {
            var req = _requestRepository.GetAll().Where(x => x.ProcessId == processid && x.UserId == userid);
            if (req == null)
            {
                return null;
            }

            var reqsts = from o in req
                              select new UserRequestsPagedDto()
                              {
                                  UserRequest = new wfRequestDto
                                  {
                                      Id = o.Id,
                                      UserId = o.UserId,
                                      ProcessId = o.ProcessId,
                                      Title = o.Title,
                                      DateRequested = o.DateRequested,
                                      Username = o.Username,
                                      CurrentStateId = o.CurrentStateId,
                                      DateCreated = o.DateCreated
                                  }
                              };

            var totalCount = reqsts.Count();
            return new PagedResultDto<UserRequestsPagedDto>(
                totalCount,
                reqsts.ToList()
            );
        }

        public async Task<wfRequestActionDto> RequestActionById(int requestid, int actionid)
        {
            var reqact = _requestActionRepository.GetAll().Where(x => x.RequestId == requestid && x.ActionId == actionid).FirstOrDefault();
            if (reqact == null)
            {
                return null;
            }

            return ObjectMapper.Map<wfRequestActionDto>(reqact);
        }

        public async Task<PagedResultDto<RequestActionsPagedDto>> ActionsForRequestById(int requestid)
        {
            var req = _requestActionRepository.GetAll().Where(x => x.RequestId == requestid);
            if (req == null)
            {
                return null;
            }

            var acts = from o in req
                         select new RequestActionsPagedDto()
                         {
                             RequestAction = new wfRequestActionDto
                             {
                                 Id = o.Id,
                                 UserId = o.UserId,
                                 DateCreated = o.DateCreated
                             }
                         };

            var totalCount = acts.Count();
            return new PagedResultDto<RequestActionsPagedDto>(
                totalCount,
                acts.ToList()
            );
        }

        public async Task CreateActivityAsync(CreateActivityDto input)
        {
            input.DateCreated = DateTime.Now;

            var actvt = ObjectMapper.Map<wfActivity>(input);

            await _activityRepository.InsertAsync(actvt);
        }

        public async Task CreateActionAsync(CreateActionDto input)
        {
            input.DateCreated = DateTime.Now;

            var action = ObjectMapper.Map<wfAction>(input);

            await _actionRepository.InsertAsync(action);
        }

        public async Task CreateStateAsync(CreateStateDto input)
        {
            input.DateCreated = DateTime.Now;

            var state = ObjectMapper.Map<wfState>(input);

            await _stateRepository.InsertAsync(state);
        }

        public async Task<int> CreateNewRequest(CreateRequestDto input)
        {
            var req = new wfRequestDto();

            req.Title = input.Title;
            req.RequestPath = input.RequestPath;
            req.ProcessId = input.ProcessId;
            req.CurrentStateId = input.CurrentStateId;
            req.UserId = input.UserId;
            req.Username = input.Username;
            req.DateCreated = DateTime.Now;
            req.DateRequested = DateTime.Now;
       
            var rq = ObjectMapper.Map<wfRequest>(req);
            var reqid = _requestRepository.GetAll().OrderByDescending(x => x.Id).FirstOrDefault().Id + 1;
            var request = _requestRepository.Insert(rq);

            var trns = _transitionRepository.GetAll().Where(x => x.CurrentStateId == input.CurrentStateId);
            foreach (var trn in trns.ToList())
            {
                var transact = _transitionActionRepository.GetAll().Where(x => x.TransitionId == trn.Id);
                foreach (var tract in transact.ToList())
                {
                    var reqact = new wfRequestActionDto();
                    reqact.DateCreated = DateTime.Now;
                    reqact.RequestId = reqid; // request.Id;
                    reqact.ActionId = tract.ActionId;
                    reqact.DateActioned = DateTime.Now;
                    reqact.IsActive = true;
                    reqact.IsComplete = false;
                    reqact.UserActioned = input.UserId;
                    reqact.UserId = input.UserId;
                    reqact.TransitionId = trn.Id;

                    var ra = ObjectMapper.Map<wfRequestAction>(reqact);
                    await _requestActionRepository.InsertAsync(ra);
                }

                var timer = new wfTimerDto();
                timer.ProcessId = input.ProcessId;
                timer.StartDate = DateTime.Now;
                timer.TransitionId = trn.Id;
                timer.DurationType = "Days";
                timer.Duration = 14;
                timer.RequestId = reqid; // request.Id;
                timer.DateCreated = DateTime.Now;
                timer.UserId = input.UserId;

                var tim = ObjectMapper.Map<wfTimer>(timer);
                await _timerRepository.InsertAsync(tim);
            }

            foreach (var reqdata in input.RequestData)
            {
                var chkrd = _requestDataRepository.GetAll().Where(a => a.Name == reqdata.Name && a.Value == reqdata.Value).Count();
                if (chkrd == 0)
                {
                    var rd = new wfRequestDataDto();

                    rd.Name = reqdata.Name;
                    rd.Value = reqdata.Value;
                    rd.UserId = input.UserId;
                    rd.RequestId = reqid; // request.Id;
                    rd.DateCreated = DateTime.Now;

                    var rqdata = ObjectMapper.Map<wfRequestData>(rd);
                    await _requestDataRepository.InsertAsync(rqdata);
                }
            }
            return reqid;// request.Id;
        }

        public async Task<int> CreateRequestData(CreateRequestDto input)
        {
            var reqid = _requestRepository.GetAll().Where(a => a.UserId == input.UserId).Max(b=>b.Id);
            var req = new wfRequestDto();

            var trns = _transitionRepository.GetAll().Where(x => x.CurrentStateId == input.CurrentStateId);
            foreach (var trn in trns.ToList())
            {
                var transact = _transitionActionRepository.GetAll().Where(x => x.TransitionId == trn.Id);
                foreach (var tract in transact.ToList())
                {
                    var reqact = new wfRequestActionDto();
                    reqact.DateCreated = DateTime.Now;
                    reqact.RequestId = reqid; // request.Id;
                    reqact.ActionId = tract.ActionId;
                    reqact.DateActioned = DateTime.Now;
                    reqact.IsActive = true;
                    reqact.IsComplete = false;
                    reqact.UserActioned = input.UserId;
                    reqact.UserId = input.UserId;
                    reqact.TransitionId = trn.Id;

                    var ra = ObjectMapper.Map<wfRequestAction>(reqact);
                    await _requestActionRepository.InsertAsync(ra);
                }

                var timer = new wfTimerDto();
                timer.ProcessId = input.ProcessId;
                timer.StartDate = DateTime.Now;
                timer.TransitionId = trn.Id;
                timer.DurationType = "Days";
                timer.Duration = 14;
                timer.RequestId = reqid; // request.Id;
                timer.DateCreated = DateTime.Now;
                timer.UserId = input.UserId;

                var tim = ObjectMapper.Map<wfTimer>(timer);
                await _timerRepository.InsertAsync(tim);
            }

            foreach (var reqdata in input.RequestData)
            {
                var chkrd = _requestDataRepository.GetAll().Where(a => a.RequestId == reqid && a.Name == reqdata.Name && a.Value == reqdata.Value).Count();
                if (chkrd == 0)
                {
                    var rd = new wfRequestDataDto();

                    rd.Name = reqdata.Name;
                    rd.Value = reqdata.Value;
                    rd.UserId = input.UserId;
                    rd.RequestId = reqid; // request.Id;
                    rd.DateCreated = DateTime.Now;

                    var rqdata = ObjectMapper.Map<wfRequestData>(rd);
                    await _requestDataRepository.InsertAsync(rqdata);
                }
            }
            return reqid;// request.Id;
        }

        public async Task AddRequestData(wfRequestDataDto input)
        {
            input.DateCreated = DateTime.Now;

            var reqdat = ObjectMapper.Map<wfRequestData>(input);

            await _requestDataRepository.InsertAsync(reqdat);
        }

        public async Task AddRequestNote(wfRequestNoteDto input)
        {
            input.DateCreated = DateTime.Now;

            var reqnote = ObjectMapper.Map<wfRequestNote>(input);

            await _requestNoteRepository.InsertAsync(reqnote);
        }

        public async Task AddRequestFile(wfRequestFileDto input)
        {
            input.DateUploaded = DateTime.Now;

            var reqfile = ObjectMapper.Map<wfRequestFile>(input);

            await _requestFileRepository.InsertAsync(reqfile);
        }

        public async Task AddRequestStakeholder(wfRequestStakeholderDto input)
        {
            input.DateCreated = DateTime.Now;

            var reqsh = ObjectMapper.Map<wfRequestStakeholder>(input);

            await _requestStakeholderRepository.InsertAsync(reqsh);
        }

        public async Task AddRequestTimer(wfTimerDto input)
        {
            input.DateCreated = DateTime.Now;

            var tmr = ObjectMapper.Map<wfTimer>(input);

            await _timerRepository.InsertAsync(tmr);
        }

    }
}
