using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.DataAccess;
using Entities.Concrete;
using Entities.Dtos;

namespace DataAccess.Repositories.MeetingRepository
{
    public interface IMeetingDal : IEntityRepository<Meeting>
    {
        Task<Meeting> AddMeeting(Meeting meeting);

        Task<List<MeetingDailyCountDto>> GetDailyCount();

        Task<int> GetCount();

        Task<List<MonthlyCountDto>> GetMonthlyCount();

        Task<MeetingDailyCountDto> GetCountByDate(DateTime time);

        Task<List<int>> GetMeetingsForReminder();
    }
}