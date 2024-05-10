using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entities.Concrete;
using Core.Utilities.Result.Abstract;

namespace Business.Repositories.ArticleRepository
{
    public interface IArticleService
    {
        Task<IDataResult<Article>> Add(Article article);

        Task<IResult> Update(Article article);

        Task<IResult> Delete(Article article);

        Task<IDataResult<List<Article>>> GetList();

        Task<IDataResult<Article>> GetById(int id);
    }
}