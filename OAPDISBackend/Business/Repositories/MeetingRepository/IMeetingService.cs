using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entities.Concrete;
using Core.Utilities.Result.Abstract;
using Entities.Dtos;

namespace Business.Repositories.MeetingRepository
{
    public interface IMeetingService
    {
        Task<IDataResult<Meeting>> Add(Meeting meeting);

        Task<IResult> Update(Meeting meeting);

        Task<IResult> Delete(Meeting meeting);

        Task<IDataResult<List<Meeting>>> GetList();

        Task<IDataResult<Meeting>> GetById(int id);

        Task<IDataResult<List<MeetingDailyCountDto>>> GetDailyCount();
    }
}