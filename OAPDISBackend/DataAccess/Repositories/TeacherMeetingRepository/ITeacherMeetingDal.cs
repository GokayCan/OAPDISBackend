using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.DataAccess;
using Entities.Concrete;
using Entities.Dtos;

namespace DataAccess.Repositories.TeacherMeetingRepository
{
    public interface ITeacherMeetingDal : IEntityRepository<TeacherMeeting>
    {
        Task<List<TeacherMeetingListDto>> GetListDto();

        Task<List<TeacherMeetingListDto>> GetListByUserId(int UserId);

        Task<TeacherMeetingDto> GetDto(int id);
        
        Task<List<int>> GetTeacherIdsByMeetingIds(List<int> meetingIds);
    }
}