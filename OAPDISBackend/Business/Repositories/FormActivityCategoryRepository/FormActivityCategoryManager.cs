using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Business.Repositories.FormActivityCategoryRepository;
using Entities.Concrete;
using Business.Aspects.Secured;
using Core.Aspects.Validation;
using Core.Aspects.Caching;
using Core.Aspects.Performance;
using Business.Repositories.FormActivityCategoryRepository.Validation;
using Business.Repositories.FormActivityCategoryRepository.Constants;
using Core.Utilities.Result.Abstract;
using Core.Utilities.Result.Concrete;
using DataAccess.Repositories.FormActivityCategoryRepository;

namespace Business.Repositories.FormActivityCategoryRepository
{
    public class FormActivityCategoryManager : IFormActivityCategoryService
    {
        private readonly IFormActivityCategoryDal _formActivityCategoryDal;

        public FormActivityCategoryManager(IFormActivityCategoryDal formActivityCategoryDal)
        {
            _formActivityCategoryDal = formActivityCategoryDal;
        }

        [SecuredAspect()]
        [ValidationAspect(typeof(FormActivityCategoryValidator))]
        [RemoveCacheAspect("IFormActivityCategoryService.Get")]

        public async Task<IResult> Add(FormActivityCategory formActivityCategory)
        {
            await _formActivityCategoryDal.Add(formActivityCategory);
            return new SuccessResult(FormActivityCategoryMessages.Added);
        }

        [SecuredAspect()]
        [ValidationAspect(typeof(FormActivityCategoryValidator))]
        [RemoveCacheAspect("IFormActivityCategoryService.Get")]

        public async Task<IResult> Update(FormActivityCategory formActivityCategory)
        {
            await _formActivityCategoryDal.Update(formActivityCategory);
            return new SuccessResult(FormActivityCategoryMessages.Updated);
        }

        [SecuredAspect()]
        [RemoveCacheAspect("IFormActivityCategoryService.Get")]

        public async Task<IResult> Delete(FormActivityCategory formActivityCategory)
        {
            await _formActivityCategoryDal.Delete(formActivityCategory);
            return new SuccessResult(FormActivityCategoryMessages.Deleted);
        }

        [SecuredAspect()]
        [CacheAspect()]
        [PerformanceAspect()]
        public async Task<IDataResult<List<FormActivityCategory>>> GetList()
        {
            return new SuccessDataResult<List<FormActivityCategory>>(await _formActivityCategoryDal.GetAll());
        }

        [SecuredAspect()]
        public async Task<IDataResult<FormActivityCategory>> GetById(int id)
        {
            return new SuccessDataResult<FormActivityCategory>(await _formActivityCategoryDal.Get(p => p.Id == id));
        }

    }
}
