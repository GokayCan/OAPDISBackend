using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Business.Repositories.ArticleRepository;
using Entities.Concrete;
using Business.Aspects.Secured;
using Core.Aspects.Validation;
using Core.Aspects.Caching;
using Core.Aspects.Performance;
using Business.Repositories.ArticleRepository.Validation;
using Business.Repositories.ArticleRepository.Constants;
using Core.Utilities.Result.Abstract;
using Core.Utilities.Result.Concrete;
using DataAccess.Repositories.ArticleRepository;

namespace Business.Repositories.ArticleRepository
{
    public class ArticleManager : IArticleService
    {
        private readonly IArticleDal _articleDal;

        public ArticleManager(IArticleDal articleDal)
        {
            _articleDal = articleDal;
        }

        //[SecuredAspect()]
        [ValidationAspect(typeof(ArticleValidator))]
        [RemoveCacheAspect("IArticleService.Get")]
        public async Task<IDataResult<Article>> Add(Article article)
        {
            try
            {
                return new SuccessDataResult<Article>(await _articleDal.AddArticle(article), ArticleMessages.Added);
            }
            catch
            {
                return new ErrorDataResult<Article>(ArticleMessages.NotAdded);
            }
        }

        //[SecuredAspect()]
        [ValidationAspect(typeof(ArticleValidator))]
        [RemoveCacheAspect("IArticleService.Get")]
        public async Task<IResult> Update(Article article)
        {
            try
            {
                await _articleDal.Update(article);
                return new SuccessResult(ArticleMessages.Updated);
            }
            catch
            {
                return new ErrorResult(ArticleMessages.NotUpdated);
            }
        }

        //[SecuredAspect()]
        [RemoveCacheAspect("IArticleService.Get")]
        public async Task<IResult> Delete(Article article)
        {
            try
            {
                await _articleDal.Delete(article);
                return new SuccessResult(ArticleMessages.Deleted);
            }
            catch
            {
                return new ErrorResult(ArticleMessages.NotDeleted);
            }
        }

        //[SecuredAspect()]
        [CacheAspect()]
        [PerformanceAspect()]
        public async Task<IDataResult<List<Article>>> GetList()
        {
            return new SuccessDataResult<List<Article>>(await _articleDal.GetAll());
        }

        //[SecuredAspect()]
        public async Task<IDataResult<Article>> GetById(int id)
        {
            return new SuccessDataResult<Article>(await _articleDal.Get(p => p.Id == id));
        }
    }
}