using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Business.Repositories.FormRepository;
using Entities.Concrete;
using Business.Aspects.Secured;
using Core.Aspects.Validation;
using Core.Aspects.Caching;
using Core.Aspects.Performance;
using Business.Repositories.FormRepository.Validation;
using Business.Repositories.FormRepository.Constants;
using Core.Utilities.Result.Abstract;
using Core.Utilities.Result.Concrete;
using DataAccess.Repositories.FormRepository;

namespace Business.Repositories.FormRepository
{
    public class FormManager : IFormService
    {
        private readonly IFormDal _formDal;

        public FormManager(IFormDal formDal)
        {
            _formDal = formDal;
        }

        [SecuredAspect()]
        [ValidationAspect(typeof(FormValidator))]
        [RemoveCacheAspect("IFormService.Get")]

        public async Task<IResult> Add(Form form)
        {
            await _formDal.Add(form);
            return new SuccessResult(FormMessages.Added);
        }

        [SecuredAspect()]
        [ValidationAspect(typeof(FormValidator))]
        [RemoveCacheAspect("IFormService.Get")]

        public async Task<IResult> Update(Form form)
        {
            await _formDal.Update(form);
            return new SuccessResult(FormMessages.Updated);
        }

        [SecuredAspect()]
        [RemoveCacheAspect("IFormService.Get")]

        public async Task<IResult> Delete(Form form)
        {
            await _formDal.Delete(form);
            return new SuccessResult(FormMessages.Deleted);
        }

        [SecuredAspect()]
        [CacheAspect()]
        [PerformanceAspect()]
        public async Task<IDataResult<List<Form>>> GetList()
        {
            return new SuccessDataResult<List<Form>>(await _formDal.GetAll());
        }

        [SecuredAspect()]
        public async Task<IDataResult<Form>> GetById(int id)
        {
            return new SuccessDataResult<Form>(await _formDal.Get(p => p.Id == id));
        }

    }
}
