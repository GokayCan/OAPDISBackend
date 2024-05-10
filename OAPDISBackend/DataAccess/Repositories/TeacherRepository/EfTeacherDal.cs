using Core.DataAccess.EntityFramework;
using DataAccess.Context.EntityFramework;
using Entities.Concrete;
using Entities.Dtos;
using Microsoft.EntityFrameworkCore;

namespace DataAccess.Repositories.TeacherRepository
{
    public class EfTeacherDal : EfEntityRepositoryBase<Teacher, SimpleContextDb>, ITeacherDal
    {
        public async Task<TeacherDto> GetByUserId(int Id)
        {
            using (var context = new SimpleContextDb())
            {
                var result = from teacher in context.Teachers.Where(x => x.UserId == Id)
                             join user in context.Users on teacher.UserId equals user.Id
                             join department in context.Departments on teacher.DepartmentId equals department.Id
                             select new TeacherDto
                             {
                                 Id = teacher.Id,
                                 DepartmentId = teacher.DepartmentId,
                                 UserId = teacher.UserId,
                                 Title = user.Title,
                                 Task = user.Task,
                                 FirstName = user.FirstName,
                                 LastName = user.LastName,
                                 Email = user.Email,
                                 PhoneNumber = user.PhoneNumber,
                             };
                return await result.FirstOrDefaultAsync();
            }
        }

        public async Task<int[]> GetDashboardStatistics(int UserId)
        {
            using (var context = new SimpleContextDb())
            {
                var userOperationClaimId = (
                    from user in context.Users
                    join userOperationClaim in context.UserOperationClaims on user.Id equals userOperationClaim.UserId
                    where user.Id == UserId
                    select userOperationClaim.OperationClaimId
                ).FirstOrDefault();

                if (userOperationClaimId == 1)
                {
                    var projectCount = await context.TeacherProjects.CountAsync();
                    var articleCount = await context.TeacherArticles.CountAsync();
                    var meetingCount = await context.TeacherMeetings.CountAsync();
                    var teacherCount = await context.Teachers.CountAsync();

                    int[] statistics = { projectCount, articleCount, meetingCount, teacherCount };
                    return statistics;
                }
                else
                {
                    var projectCount = await (
                        from teacher in context.Teachers
                        join teacherProject in context.TeacherProjects on teacher.Id equals teacherProject.TeacherId
                        where teacher.UserId == UserId
                        select teacherProject
                    ).CountAsync();

                    var articleCount = await (
                        from teacher in context.Teachers
                        join teacherArticle in context.TeacherArticles on teacher.Id equals teacherArticle.TeacherId
                        where teacher.UserId == UserId
                        select teacherArticle
                    ).CountAsync();

                    var meetingCount = await (
                        from teacher in context.Teachers
                        join meeting in context.TeacherMeetings on teacher.Id equals meeting.TeacherId
                        where teacher.UserId == UserId
                        select meeting
                    ).CountAsync();

                    var teacherCount = await (
                        from teacher in context.Teachers
                        join department in context.Departments on teacher.DepartmentId equals department.Id
                        where teacher.UserId == UserId
                        select teacher
                    ).CountAsync();

                    int[] statistics = { projectCount, articleCount, meetingCount, teacherCount };
                    return statistics;
                }
            }
        }

        public async Task<TeacherDto> GetDto(int Id)
        {
            using (var context = new SimpleContextDb())
            {
                var result = from teahcer in context.Teachers.Where(x => x.Id == Id)
                             join user in context.Users on teahcer.UserId equals user.Id
                             join department in context.Departments on teahcer.DepartmentId equals department.Id
                             select new TeacherDto
                             {
                                 Id = teahcer.Id,
                                 DepartmentId = teahcer.DepartmentId,
                                 UserId = teahcer.UserId,
                                 Title = user.Title,
                                 Task = user.Task,
                                 FirstName = user.FirstName,
                                 LastName = user.LastName,
                                 Email = user.Email,
                                 PhoneNumber = user.PhoneNumber,
                             };
                return await result.FirstOrDefaultAsync();
            }
        }

        public async Task<List<TeacherListDto>> GetListDto()
        {
            using (var context = new SimpleContextDb())
            {
                var result = from teahcer in context.Teachers
                             join user in context.Users on teahcer.UserId equals user.Id
                             join department in context.Departments on teahcer.DepartmentId equals department.Id
                             select new TeacherListDto
                             {
                                 Id = teahcer.Id,
                                 DepartmentId = teahcer.DepartmentId,
                                 DepartmentName = department.Name,
                                 UserId = teahcer.UserId,
                                 Title = user.Title,
                                 Task = user.Task,
                                 FirstName = user.FirstName,
                                 LastName = user.LastName,
                                 Email = user.Email,
                                 PhoneNumber = user.PhoneNumber,
                             };
                return await result.ToListAsync();
            }
        }

        public async Task<TeacherDto> GetMostProductiveTeacher()
        {
            using var context = new SimpleContextDb();

            var teacherProjectCounts = await context.TeacherProjects
                .GroupBy(tp => tp.TeacherId)
                .Select(g => new { TeacherId = g.Key, ProjectCount = g.Count() })
                .ToListAsync();

            if (teacherProjectCounts.Count == 0)
            {
                throw new Exception("No teacher with projects found.");
            }

            var mostProductiveTeacher = teacherProjectCounts.OrderByDescending(tpc => tpc.ProjectCount)
                                                           .FirstOrDefault();

            if (mostProductiveTeacher == null)
            {
                throw new Exception("Most productive teacher not found.");
            }

            var teacherId = mostProductiveTeacher.TeacherId;

            var teacher = await (from t in context.Teachers
                                 join u in context.Users on t.UserId equals u.Id
                                 join d in context.Departments on t.DepartmentId equals d.Id
                                 where t.Id == teacherId
                                 select new TeacherDto
                                 {
                                     Id = t.Id,
                                     FirstName = u.FirstName,
                                     LastName = u.LastName,
                                     DepartmentId = t.DepartmentId,
                                     DepartmentName = d.Name,
                                     ProjectCount = mostProductiveTeacher.ProjectCount
                                 }).FirstOrDefaultAsync();

            if (teacher == null)
            {
                throw new Exception("Teacher not found.");
            }

            return teacher;
        }

        public async Task<int> GetCount()
        {
            using (var context = new SimpleContextDb())
            {
                return await context.Teachers.CountAsync();
            }
        }

        public async Task<List<int>> GetUserIdsByTeacherIds(List<int> teacherIds)
        {
            using (var context = new SimpleContextDb())
            {
                return await context.Teachers.Where(p => teacherIds.Contains(p.Id)).Select(p => p.UserId)
                    .ToListAsync();
            }
        }
    }
}