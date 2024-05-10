using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Business.Repositories.MeetingRepository;
using Entities.Concrete;
using Business.Aspects.Secured;
using Core.Aspects.Validation;
using Core.Aspects.Caching;
using Core.Aspects.Performance;
using Business.Repositories.MeetingRepository.Validation;
using Business.Repositories.MeetingRepository.Constants;
using Core.Utilities.Result.Abstract;
using Core.Utilities.Result.Concrete;
using DataAccess.Repositories.MeetingRepository;
using Entities.Dtos;

namespace Business.Repositories.MeetingRepository
{
    public class MeetingManager : IMeetingService
    {
        private readonly IMeetingDal _meetingDal;

        public MeetingManager(IMeetingDal meetingDal)
        {
            _meetingDal = meetingDal;
        }

        //[SecuredAspect()]
        [ValidationAspect(typeof(MeetingValidator))]
        [RemoveCacheAspect("IMeetingService.Get")]
        public async Task<IDataResult<Meeting>> Add(Meeting meeting)
        {
            try
            {
                return new SuccessDataResult<Meeting>(await _meetingDal.AddMeeting(meeting), MeetingMessages.Added);
            }
            catch
            {
                return new ErrorDataResult<Meeting>(MeetingMessages.NotAdded);
            }
        }

        //[SecuredAspect()]
        [ValidationAspect(typeof(MeetingValidator))]
        [RemoveCacheAspect("IMeetingService.Get")]
        public async Task<IResult> Update(Meeting meeting)
        {
            try
            {
                await _meetingDal.Update(meeting);
                return new SuccessResult(MeetingMessages.Updated);
            }
            catch
            {
                return new ErrorResult(MeetingMessages.NotUpdated);
            }
        }

        //[SecuredAspect()]
        [RemoveCacheAspect("IMeetingService.Get")]
        public async Task<IResult> Delete(Meeting meeting)
        {
            try
            {
                await _meetingDal.Delete(meeting);
                return new SuccessResult(MeetingMessages.Deleted);
            }
            catch
            {
                return new ErrorResult(MeetingMessages.NotDeleted);
            }
        }

        //[SecuredAspect()]
        [CacheAspect()]
        [PerformanceAspect()]
        public async Task<IDataResult<List<Meeting>>> GetList()
        {
            return new SuccessDataResult<List<Meeting>>(await _meetingDal.GetAll());
        }

        //[SecuredAspect()]
        public async Task<IDataResult<Meeting>> GetById(int id)
        {
            return new SuccessDataResult<Meeting>(await _meetingDal.Get(p => p.Id == id));
        }

        public async Task<IDataResult<List<MeetingDailyCountDto>>> GetDailyCount()
        {
            return new SuccessDataResult<List<MeetingDailyCountDto>>(await _meetingDal.GetDailyCount());
        }
    }
}