using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.DataAccess.EntityFramework;
using Entities.Concrete;
using DataAccess.Repositories.TeacherProjectRepository;
using DataAccess.Context.EntityFramework;
using Entities.Dtos;
using Microsoft.EntityFrameworkCore;

namespace DataAccess.Repositories.TeacherProjectRepository
{
    public class EfTeacherProjectDal : EfEntityRepositoryBase<TeacherProject, SimpleContextDb>, ITeacherProjectDal
    {
        public async Task<TeacherProjectDto> GetDto(int id)
        {
            using (var context = new SimpleContextDb())
            {
                var result = from teacherProject in context.TeacherProjects.Where(x => x.Id == id)
                             join teacher in context.Teachers on teacherProject.TeacherId equals teacher.Id
                             join project in context.Projects on teacherProject.ProjectId equals project.Id
                             select new TeacherProjectDto
                             {
                                 Id = teacherProject.Id,
                                 TeacherId = teacherProject.TeacherId,
                                 ProjectId = teacherProject.ProjectId,
                                 Title = project.Title,
                                 Description = project.Description,
                                 Date = project.Date,
                             };
                return await result.FirstOrDefaultAsync();
            }
        }

        public async Task<List<TeacherProjectListDto>> GetListByUserId(int UserId)
        {
            using (var context = new SimpleContextDb())
            {
                var result = from teacherProject in context.TeacherProjects
                             join teacher in context.Teachers on teacherProject.TeacherId equals teacher.Id
                             join user
                             in context.Users on teacher.UserId equals user.Id
                             join department in context.Departments on teacher.DepartmentId equals department.Id
                             join project in context.Projects on teacherProject.ProjectId equals project.Id
                             where user.Id == UserId
                             select new TeacherProjectListDto
                             {
                                 Id = teacherProject.Id,
                                 TeacherId = teacherProject.TeacherId,
                                 ProjectId = teacherProject.ProjectId,
                                 FirstName = user.FirstName,
                                 LastName = user.LastName,
                                 Department = department.Name,
                                 Title = project.Title,
                                 Description = project.Description,
                                 Date = project.Date,
                             };
                return await result.ToListAsync();
            }
        }

        public async Task<List<TeacherProjectListDto>> GetListDto()
        {
            using (var context = new SimpleContextDb())
            {
                var result = from teacherProject in context.TeacherProjects
                             join teacher in context.Teachers on teacherProject.TeacherId equals teacher.Id
                             join user in context.Users on teacher.UserId equals user.Id
                             join department in context.Departments on teacher.DepartmentId equals department.Id
                             join project in context.Projects on teacherProject.ProjectId equals project.Id
                             select new TeacherProjectListDto
                             {
                                 Id = teacherProject.Id,
                                 TeacherId = teacherProject.TeacherId,
                                 ProjectId = teacherProject.ProjectId,
                                 Title = project.Title,
                                 FirstName = user.FirstName,
                                 LastName = user.LastName,
                                 Department = department.Name,
                                 Date = project.Date,
                             };
                return await result.ToListAsync();
            }
        }
    }
}