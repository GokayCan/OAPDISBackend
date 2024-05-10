using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.DataAccess.EntityFramework;
using Entities.Concrete;
using DataAccess.Repositories.ProjectRepository;
using DataAccess.Context.EntityFramework;
using Microsoft.EntityFrameworkCore;
using Entities.Dtos;

namespace DataAccess.Repositories.ProjectRepository
{
    public class EfProjectDal : EfEntityRepositoryBase<Project, SimpleContextDb>, IProjectDal
    {
        public async Task<Project> AddProject(Project project)
        {
            using (var context = new SimpleContextDb())
            {
                var data = context.Entry(project);
                data.State = Microsoft.EntityFrameworkCore.EntityState.Added;
                await context.SaveChangesAsync();
                return data.Entity;
            }
        }

        public async Task<int> GetCount()
        {
            using (var context = new SimpleContextDb())
            {
                return await context.Projects.CountAsync();
            }
        }

        public async Task<List<MonthlyCountDto>> GetMonthlyCount()
        {
            using (var context = new SimpleContextDb())
            {
                var today = DateTime.Today;
                var last12Months = Enumerable.Range(0, 12)
                    .Select(i => today.AddMonths(-i).Month)
                    .ToList();

                var result = await context.Projects
                    .Where(article => last12Months.Contains(article.Date.Month))
                    .GroupBy(article => article.Date.Month)
                    .Select(groupedArticles => new MonthlyCountDto
                    {
                        Month = groupedArticles.Key,
                        Count = groupedArticles.Count()
                    })
                    .ToListAsync();

                // Eksik ayları ekle
                var missingMonths = last12Months.Except(result.Select(dto => dto.Month)).ToList();
                foreach (var missingMonth in missingMonths)
                {
                    result.Add(new MonthlyCountDto
                    {
                        Month = missingMonth,
                        Count = 0
                    });
                }

                // last12Months sırasına göre yeniden düzenle
                var sortedResult = last12Months
                    .Select(month => result.FirstOrDefault(dto => dto.Month == month) ?? new MonthlyCountDto { Month = month, Count = 0 })
                    .ToList();

                return sortedResult;
            }
        }
    }
}