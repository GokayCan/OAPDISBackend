using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.DataAccess;
using Entities.Concrete;
using Entities.Dtos;

namespace DataAccess.Repositories.ArticleRepository
{
    public interface IArticleDal : IEntityRepository<Article>
    {
        Task<Article> AddArticle(Article article);
        Task<int> GetCount();
        Task<List<MonthlyCountDto>> GetMonthlyCount();
    }
}