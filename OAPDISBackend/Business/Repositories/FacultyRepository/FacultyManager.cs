using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Business.Repositories.FacultyRepository;
using Entities.Concrete;
using Business.Aspects.Secured;
using Core.Aspects.Validation;
using Core.Aspects.Caching;
using Core.Aspects.Performance;
using Business.Repositories.FacultyRepository.Validation;
using Business.Repositories.FacultyRepository.Constants;
using Core.Utilities.Result.Abstract;
using Core.Utilities.Result.Concrete;
using DataAccess.Repositories.FacultyRepository;

namespace Business.Repositories.FacultyRepository
{
    public class FacultyManager : IFacultyService
    {
        private readonly IFacultyDal _facultyDal;

        public FacultyManager(IFacultyDal facultyDal)
        {
            _facultyDal = facultyDal;
        }

        //[SecuredAspect()]
        [ValidationAspect(typeof(FacultyValidator))]
        [RemoveCacheAspect("IFacultyService.Get")]
        public async Task<IResult> Add(Faculty faculty)
        {
            try
            {
                await _facultyDal.Add(faculty);
                return new SuccessResult(FacultyMessages.Added);
            }
            catch
            {
                return new ErrorResult(FacultyMessages.NotAdded);
            }
        }

        //[SecuredAspect()]
        [ValidationAspect(typeof(FacultyValidator))]
        [RemoveCacheAspect("IFacultyService.Get")]
        public async Task<IResult> Update(Faculty faculty)
        {
            try
            {
                await _facultyDal.Update(faculty);
                return new SuccessResult(FacultyMessages.Updated);
            }
            catch
            {
                return new ErrorResult(FacultyMessages.NotUpdated);
            }
        }

        //[SecuredAspect()]
        [RemoveCacheAspect("IFacultyService.Get")]
        public async Task<IResult> Delete(Faculty faculty)
        {
            try
            {
                await _facultyDal.Delete(faculty);
                return new SuccessResult(FacultyMessages.Deleted);
            }
            catch
            {
                return new ErrorResult(FacultyMessages.NotDeleted);
            }
        }

        //[SecuredAspect()]
        [CacheAspect()]
        [PerformanceAspect()]
        public async Task<IDataResult<List<Faculty>>> GetList()
        {
            return new SuccessDataResult<List<Faculty>>(await _facultyDal.GetAll());
        }

        //[SecuredAspect()]
        public async Task<IDataResult<Faculty>> GetById(int id)
        {
            return new SuccessDataResult<Faculty>(await _facultyDal.Get(p => p.Id == id));
        }
    }
}