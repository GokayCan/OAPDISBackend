using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.DataAccess;
using Entities.Concrete;
using Entities.Dtos;

namespace DataAccess.Repositories.ProjectRepository
{
    public interface IProjectDal : IEntityRepository<Project>
    {
        Task<Project> AddProject(Project project);
        Task<int> GetCount();
        Task<List<MonthlyCountDto>> GetMonthlyCount();
    }
}