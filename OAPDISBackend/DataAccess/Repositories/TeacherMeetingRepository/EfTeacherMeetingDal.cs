using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.DataAccess.EntityFramework;
using Entities.Concrete;
using DataAccess.Repositories.TeacherMeetingRepository;
using DataAccess.Context.EntityFramework;
using Entities.Dtos;
using Microsoft.EntityFrameworkCore;
using System.IO.Compression;

namespace DataAccess.Repositories.TeacherMeetingRepository
{
    public class EfTeacherMeetingDal : EfEntityRepositoryBase<TeacherMeeting, SimpleContextDb>, ITeacherMeetingDal
    {
        public async Task<List<TeacherMeetingListDto>> GetListDto()
        {
            using (var context = new SimpleContextDb())
            {
                var result = from teacherMeeting in context.TeacherMeetings
                             join teacher in context.Teachers
                             on teacherMeeting.TeacherId equals teacher.Id
                             join user in context.Users
                             on teacher.UserId equals user.Id
                             join meeting in context.Meetings
                             on teacherMeeting.MeetingId equals meeting.Id
                             join department in context.Departments
                             on teacher.DepartmentId equals department.Id
                             select new TeacherMeetingListDto
                             {
                                 Id = teacherMeeting.Id,
                                 TeacherId = teacherMeeting.TeacherId,
                                 MeetingId = teacherMeeting.MeetingId,
                                 FirstName = user.FirstName,
                                 LastName = user.LastName,
                                 Department = department.Name,
                                 Title = meeting.Title,
                                 Date = meeting.Date,
                             };
                return await result.ToListAsync();
            }
        }

        public async Task<TeacherMeetingDto> GetDto(int id)
        {
            using (var context = new SimpleContextDb())
            {
                var result = from teacherMeeting in context.TeacherMeetings.Where(x => x.Id == id)
                             join teacher in context.Teachers on teacherMeeting.TeacherId equals teacher.Id
                             join meeting in context.Meetings on teacherMeeting.MeetingId equals meeting.Id
                             select new TeacherMeetingDto
                             {
                                 Id = teacherMeeting.Id,
                                 TeacherId = teacherMeeting.TeacherId,
                                 MeetingId = teacherMeeting.MeetingId,
                                 Title = meeting.Title,
                                 Description = meeting.Description,
                                 Date = meeting.Date,
                             };
                return await result.FirstOrDefaultAsync();
            }
        }

        public async Task<List<int>> GetTeacherIdsByMeetingIds(List<int> meetingIds)
        {
            using (var context = new SimpleContextDb())
            {
                var result = from teacherMeeting in context.TeacherMeetings
                             where meetingIds.Contains(teacherMeeting.MeetingId)
                             select teacherMeeting.TeacherId;
                return await result.ToListAsync();
            }
        }

        public async Task<List<TeacherMeetingListDto>> GetListByUserId(int UserId)
        {
            using (var context = new SimpleContextDb())
            {
                var result = from teacherMeeting in context.TeacherMeetings
                             join teacher in context.Teachers on teacherMeeting.TeacherId equals teacher.Id
                             join user in context.Users on teacher.UserId equals user.Id
                             join meeting in context.Meetings on teacherMeeting.MeetingId equals meeting.Id
                             join department in context.Departments on teacher.DepartmentId equals department.Id
                             where user.Id == UserId
                             select new TeacherMeetingListDto
                             {
                                 Id = teacherMeeting.Id,
                                 TeacherId = teacherMeeting.TeacherId,
                                 MeetingId = teacherMeeting.MeetingId,
                                 FirstName = user.FirstName,
                                 LastName = user.LastName,
                                 Department = department.Name,
                                 Title = meeting.Title,
                                 Date = meeting.Date,
                             };
                return await result.ToListAsync();
            }
        }
    }
}