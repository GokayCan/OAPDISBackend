using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;
using Business.Repositories.TeacherMeetingRepository;
using Entities.Concrete;
using Business.Aspects.Secured;
using Core.Aspects.Validation;
using Core.Aspects.Caching;
using Core.Aspects.Performance;
using Business.Repositories.TeacherMeetingRepository.Validation;
using Business.Repositories.TeacherMeetingRepository.Constants;
using Core.Utilities.Result.Abstract;
using Core.Utilities.Result.Concrete;
using DataAccess.Repositories.TeacherMeetingRepository;
using Entities.Dtos;
using Business.Repositories.MeetingRepository;

namespace Business.Repositories.TeacherMeetingRepository
{
    public class TeacherMeetingManager : ITeacherMeetingService
    {
        private readonly ITeacherMeetingDal _teacherMeetingDal;
        private readonly IMeetingService _meetingService;

        public TeacherMeetingManager(ITeacherMeetingDal teacherMeetingDal, IMeetingService meetingService)
        {
            _teacherMeetingDal = teacherMeetingDal;
            _meetingService = meetingService;
        }

        //[SecuredAspect()]
        [ValidationAspect(typeof(TeacherMeetingValidator))]
        [RemoveCacheAspect("ITeacherMeetingService.Get")]
        public async Task<IResult> Add(TeacherMeetingDto teacherMeetingDto)
        {
            using var scope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled);
            try
            {
                Meeting meeting = new Meeting();

                meeting.Title = teacherMeetingDto.Title;
                meeting.Description = teacherMeetingDto.Description;
                meeting.Date = teacherMeetingDto.Date;

                var addedMeeting = await _meetingService.Add(meeting);

                TeacherMeeting teacherMeeting = new TeacherMeeting();

                teacherMeeting.TeacherId = teacherMeetingDto.TeacherId;
                teacherMeeting.MeetingId = addedMeeting.Data.Id;

                await _teacherMeetingDal.Add(teacherMeeting);
                scope.Complete();
                return new SuccessResult(TeacherMeetingMessages.Added);
            }
            catch
            {
                scope.Dispose();
                return new ErrorResult(TeacherMeetingMessages.NotAdded);
            }
        }

        //[SecuredAspect()]
        [ValidationAspect(typeof(TeacherMeetingValidator))]
        [RemoveCacheAspect("ITeacherMeetingService.Get")]
        public async Task<IResult> Update(TeacherMeetingDto teacherMeetingDto)
        {
            using var scope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled);
            try
            {
                Meeting meeting = _meetingService.GetById(teacherMeetingDto.MeetingId).Result.Data;

                meeting.Title = teacherMeetingDto.Title;
                meeting.Description = teacherMeetingDto.Description;
                meeting.Date = teacherMeetingDto.Date;

                await _meetingService.Update(meeting);

                TeacherMeeting teacherMeeting = await _teacherMeetingDal.Get(p => p.MeetingId == teacherMeetingDto.MeetingId);

                teacherMeeting.TeacherId = teacherMeetingDto.TeacherId;
                teacherMeeting.MeetingId = teacherMeetingDto.MeetingId;

                await _teacherMeetingDal.Update(teacherMeeting);
                scope.Complete();
                return new SuccessResult(TeacherMeetingMessages.Updated);
            }
            catch
            {
                scope.Dispose();
                return new ErrorResult(TeacherMeetingMessages.NotUpdated);
            }
        }

        //[SecuredAspect()]
        [RemoveCacheAspect("ITeacherMeetingService.Get")]
        public async Task<IResult> Delete(TeacherMeeting teacherMeeting)
        {
            using var scope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled);
            try
            {
                await _teacherMeetingDal.Delete(teacherMeeting);
                await _meetingService.Delete(new Meeting() { Id = teacherMeeting.MeetingId });
                scope.Complete();
                return new SuccessResult(TeacherMeetingMessages.Deleted);
            }
            catch
            {
                scope.Dispose();
                return new ErrorResult(TeacherMeetingMessages.NotDeleted);
            }
        }

        //[SecuredAspect()]
        [CacheAspect()]
        [PerformanceAspect()]
        public async Task<IDataResult<List<TeacherMeetingListDto>>> GetList()
        {
            return new SuccessDataResult<List<TeacherMeetingListDto>>(await _teacherMeetingDal.GetListDto());
        }

        // [SecuredAspect()]
        public async Task<IDataResult<TeacherMeetingDto>> GetById(int id)
        {
            return new SuccessDataResult<TeacherMeetingDto>(await _teacherMeetingDal.GetDto(id));
        }

        public async Task<IDataResult<List<TeacherMeetingListDto>>> GetListByUserId(int UserId)
        {
            return new SuccessDataResult<List<TeacherMeetingListDto>>(await _teacherMeetingDal.GetListByUserId(UserId));
        }
    }
}