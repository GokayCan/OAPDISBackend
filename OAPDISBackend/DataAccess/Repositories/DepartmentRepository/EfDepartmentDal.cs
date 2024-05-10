using Core.DataAccess.EntityFramework;
using DataAccess.Context.EntityFramework;
using Entities.Concrete;
using Entities.Dtos;
using Microsoft.EntityFrameworkCore;

namespace DataAccess.Repositories.DepartmentRepository
{
    public class EfDepartmentDal : EfEntityRepositoryBase<Department, SimpleContextDb>, IDepartmentDal
    {
        public async Task<List<DepartmentListDto>> GetListDto()
        {
            using (var context = new SimpleContextDb())
            {
                var result = from department in context.Departments
                             join faculty in context.Faculties
                             on department.FacultyId equals faculty.Id
                             select new DepartmentListDto
                             {
                                 Id = department.Id,
                                 FacultyId = department.FacultyId,
                                 FacultyName = faculty.Name,
                                 Name = department.Name,
                             };
                return await result.ToListAsync();
            }
        }

        public async Task<DepartmentListDto> GetMostProductiveDepartment()
        {
            using var context = new SimpleContextDb();

            var departmentProjectCounts = await context.TeacherProjects
                .Join(context.Teachers, tp => tp.TeacherId, t => t.Id, (tp, t) => new { tp.ProjectId, t.DepartmentId })
                .GroupBy(x => x.DepartmentId)
                .Select(g => new { DepartmentId = g.Key, ProjectCount = g.Count() })
                .ToListAsync();

            if (departmentProjectCounts.Count == 0)
            {
                throw new Exception("No projects found.");
            }

            var mostProductiveDepartment = departmentProjectCounts.OrderByDescending(dpc => dpc.ProjectCount)
                                                                 .FirstOrDefault();

            if (mostProductiveDepartment == null)
            {
                throw new Exception("Most productive department not found.");
            }

            var departmentId = mostProductiveDepartment.DepartmentId;

            var department = await (from d in context.Departments
                                    join f in context.Faculties on d.FacultyId equals f.Id
                                    where d.Id == departmentId
                                    select new DepartmentListDto
                                    {
                                        Id = d.Id,
                                        Name = d.Name,
                                        FacultyId = f.Id,
                                        FacultyName = f.Name,
                                        ProjectCount = mostProductiveDepartment.ProjectCount.ToString()
                                    })
                                    .FirstOrDefaultAsync();

            if (department == null)
            {
                throw new Exception("Department not found.");
            }

            return department;
        }
    }
}