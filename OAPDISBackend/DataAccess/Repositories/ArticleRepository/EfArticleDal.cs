using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.DataAccess.EntityFramework;
using Entities.Concrete;
using DataAccess.Repositories.ArticleRepository;
using DataAccess.Context.EntityFramework;
using Microsoft.EntityFrameworkCore;
using Entities.Dtos;

namespace DataAccess.Repositories.ArticleRepository
{
    public class EfArticleDal : EfEntityRepositoryBase<Article, SimpleContextDb>, IArticleDal
    {
        public async Task<Article> AddArticle(Article article)
        {
            using (var context = new SimpleContextDb())
            {
                var data = context.Entry(article);
                data.State = Microsoft.EntityFrameworkCore.EntityState.Added;
                await context.SaveChangesAsync();
                return data.Entity;
            }
        }

        public async Task<int> GetCount()
        {
            using (var context = new SimpleContextDb())
            {
                return await context.Articles.CountAsync();
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

                var result = await context.Articles
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