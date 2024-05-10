using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entities.Concrete;
using Core.Utilities.Result.Abstract;
using Entities.Dtos;

namespace Business.Repositories.TeacherMeetingRepository
{
    public interface ITeacherMeetingService
    {
        Task<IResult> Add(TeacherMeetingDto teacherMeetingDto);

        Task<IResult> Update(TeacherMeetingDto teacherMeetingDto);

        Task<IResult> Delete(TeacherMeeting teacherMeeting);

        Task<IDataResult<List<TeacherMeetingListDto>>> GetList();

        Task<IDataResult<List<TeacherMeetingListDto>>> GetListByUserId(int UserId);

        Task<IDataResult<TeacherMeetingDto>> GetById(int id);
    }
}