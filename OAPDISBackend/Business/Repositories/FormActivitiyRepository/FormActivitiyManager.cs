using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Business.Repositories.FormActivitiyRepository;
using Entities.Concrete;
using Business.Aspects.Secured;
using Core.Aspects.Validation;
using Core.Aspects.Caching;
using Core.Aspects.Performance;
using Business.Repositories.FormActivitiyRepository.Validation;
using Business.Repositories.FormActivitiyRepository.Constants;
using Core.Utilities.Result.Abstract;
using Core.Utilities.Result.Concrete;
using DataAccess.Repositories.FormActivitiyRepository;

namespace Business.Repositories.FormActivitiyRepository
{
    public class FormActivitiyManager : IFormActivitiyService
    {
        private readonly IFormActivitiyDal _formActivitiyDal;

        public FormActivitiyManager(IFormActivitiyDal formActivitiyDal)
        {
            _formActivitiyDal = formActivitiyDal;
        }

        [SecuredAspect()]
        [ValidationAspect(typeof(FormActivitiyValidator))]
        [RemoveCacheAspect("IFormActivitiyService.Get")]

        public async Task<IResult> Add(FormActivitiy formActivitiy)
        {
            await _formActivitiyDal.Add(formActivitiy);
            return new SuccessResult(FormActivitiyMessages.Added);
        }

        [SecuredAspect()]
        [ValidationAspect(typeof(FormActivitiyValidator))]
        [RemoveCacheAspect("IFormActivitiyService.Get")]

        public async Task<IResult> Update(FormActivitiy formActivitiy)
        {
            await _formActivitiyDal.Update(formActivitiy);
            return new SuccessResult(FormActivitiyMessages.Updated);
        }

        [SecuredAspect()]
        [RemoveCacheAspect("IFormActivitiyService.Get")]

        public async Task<IResult> Delete(FormActivitiy formActivitiy)
        {
            await _formActivitiyDal.Delete(formActivitiy);
            return new SuccessResult(FormActivitiyMessages.Deleted);
        }

        [SecuredAspect()]
        [CacheAspect()]
        [PerformanceAspect()]
        public async Task<IDataResult<List<FormActivitiy>>> GetList()
        {
            return new SuccessDataResult<List<FormActivitiy>>(await _formActivitiyDal.GetAll());
        }

        [SecuredAspect()]
        public async Task<IDataResult<FormActivitiy>> GetById(int id)
        {
            return new SuccessDataResult<FormActivitiy>(await _formActivitiyDal.Get(p => p.Id == id));
        }

    }
}
