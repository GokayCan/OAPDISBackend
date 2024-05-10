using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;
using Business.Repositories.TeacherArticleRepository;
using Entities.Concrete;
using Business.Aspects.Secured;
using Core.Aspects.Validation;
using Core.Aspects.Caching;
using Core.Aspects.Performance;
using Business.Repositories.TeacherArticleRepository.Validation;
using Business.Repositories.TeacherArticleRepository.Constants;
using Core.Utilities.Result.Abstract;
using Core.Utilities.Result.Concrete;
using DataAccess.Repositories.TeacherArticleRepository;
using Entities.Dtos;
using Business.Abstract;
using Business.Repositories.ArticleRepository;

namespace Business.Repositories.TeacherArticleRepository
{
    public class TeacherArticleManager : ITeacherArticleService
    {
        private readonly ITeacherArticleDal _teacherArticleDal;
        private readonly IFileService _fileService;
        private readonly IArticleService _articleService;

        public TeacherArticleManager(ITeacherArticleDal teacherArticleDal, IFileService fileService, IArticleService articleService)
        {
            _teacherArticleDal = teacherArticleDal;
            _fileService = fileService;
            _articleService = articleService;
        }

        //[SecuredAspect()]
        [ValidationAspect(typeof(TeacherArticleValidator))]
        [RemoveCacheAspect("ITeacherArticleService.Get")]
        public async Task<IResult> Add(TeacherArticleDto teacherArticleDto)
        {
            using var scope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled);
            try
            {
                Article article = new Article();
                article.Title = teacherArticleDto.Title;
                article.Description = teacherArticleDto.Description;
                article.Date = teacherArticleDto.Date;
                if (teacherArticleDto.File != null)
                {
                    article.FileUrl = _fileService.FileSaveToFtp(teacherArticleDto.File, "articles");
                }
                var addedArticle = await _articleService.Add(article);

                TeacherArticle teacherArticle = new TeacherArticle();

                teacherArticle.TeacherId = teacherArticleDto.TeacherId;
                teacherArticle.ArticleId = addedArticle.Data.Id;

                await _teacherArticleDal.Add(teacherArticle);
                scope.Complete();
                return new SuccessResult(TeacherArticleMessages.Added);
            }
            catch
            {
                scope.Dispose();
                return new ErrorDataResult<TeacherArticle>(TeacherArticleMessages.NotAdded);
            }
        }

        //[SecuredAspect()]
        [ValidationAspect(typeof(TeacherArticleValidator))]
        [RemoveCacheAspect("ITeacherArticleService.Get")]
        public async Task<IResult> Update(TeacherArticleDto teacherArticleDto)
        {
            using var scope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled);
            try
            {
                Article article = _articleService.GetById(teacherArticleDto.ArticleId).Result.Data;

                article.Title = teacherArticleDto.Title;
                article.Description = teacherArticleDto.Description;
                article.Date = teacherArticleDto.Date;

                if (teacherArticleDto.File != null)
                {
                    article.FileUrl = _fileService.FileSaveToFtp(teacherArticleDto.File, "articles");
                }

                await _articleService.Update(article);

                TeacherArticle teacherArticle = await _teacherArticleDal.Get(p => p.Id == teacherArticleDto.Id);

                teacherArticle.TeacherId = teacherArticleDto.TeacherId;
                teacherArticle.ArticleId = teacherArticleDto.ArticleId;

                await _teacherArticleDal.Update(teacherArticle);
                scope.Complete();
                return new SuccessResult(TeacherArticleMessages.Updated);
            }
            catch
            {
                scope.Dispose();
                return new ErrorResult(TeacherArticleMessages.NotUpdated);
            }
        }

        //[SecuredAspect()]
        [RemoveCacheAspect("ITeacherArticleService.Get")]
        public async Task<IResult> Delete(TeacherArticle teacherArticle)
        {
            using var scope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled);
            try
            {
                await _teacherArticleDal.Delete(teacherArticle);
                await _articleService.Delete(new Article() { Id = teacherArticle.ArticleId });
                scope.Complete();
                return new SuccessResult(TeacherArticleMessages.Deleted);
            }
            catch
            {
                scope.Dispose();
                return new ErrorResult(TeacherArticleMessages.NotDeleted);
            }
        }

        //[SecuredAspect()]
        [CacheAspect()]
        [PerformanceAspect()]
        public async Task<IDataResult<List<TeacherArticleListDto>>> GetList()
        {
            return new SuccessDataResult<List<TeacherArticleListDto>>(await _teacherArticleDal.GetListDto());
        }

        //[SecuredAspect()]
        public async Task<IDataResult<TeacherArticleDto>> GetById(int id)
        {
            return new SuccessDataResult<TeacherArticleDto>(await _teacherArticleDal.GetDto(id));
        }

        public async Task<IDataResult<List<TeacherArticleListDto>>> GetListByUserId(int UserId)
        {
            return new SuccessDataResult<List<TeacherArticleListDto>>(await _teacherArticleDal.GetListByUserId(UserId));
        }
    }
}