using Business.Repositories.DepartmentRepository.Constants;
using Business.Repositories.DepartmentRepository.Validation;
using Core.Aspects.Caching;
using Core.Aspects.Performance;
using Core.Aspects.Validation;
using Core.Utilities.Result.Abstract;
using Core.Utilities.Result.Concrete;
using DataAccess.Repositories.DepartmentRepository;
using Entities.Concrete;
using Entities.Dtos;

namespace Business.Repositories.DepartmentRepository
{
    public class DepartmentManager : IDepartmentService
    {
        private readonly IDepartmentDal _departmentDal;

        public DepartmentManager(IDepartmentDal departmentDal)
        {
            _departmentDal = departmentDal;
        }

        ////[SecuredAspect()]
        [ValidationAspect(typeof(DepartmentValidator))]
        [RemoveCacheAspect("IDepartmentService.Get")]
        public async Task<IResult> Add(Department department)
        {
            try
            {
                await _departmentDal.Add(department);
                return new SuccessResult(DepartmentMessages.Added);
            }
            catch
            {
                return new ErrorResult(DepartmentMessages.NotAdded);
            }
        }

        ////[SecuredAspect()]
        [ValidationAspect(typeof(DepartmentValidator))]
        [RemoveCacheAspect("IDepartmentService.Get")]
        public async Task<IResult> Update(Department department)
        {
            try
            {
                await _departmentDal.Update(department);
                return new SuccessResult(DepartmentMessages.Updated);
            }
            catch
            {
                return new ErrorResult(DepartmentMessages.NotUpdated);
            }
        }

        ////[SecuredAspect()]
        [RemoveCacheAspect("IDepartmentService.Get")]
        public async Task<IResult> Delete(Department department)
        {
            try
            {
                await _departmentDal.Delete(department);
                return new SuccessResult(DepartmentMessages.Deleted);
            }
            catch
            {
                return new ErrorResult(DepartmentMessages.NotDeleted);
            }
        }

        ////[SecuredAspect()]
        [CacheAspect()]
        [PerformanceAspect()]
        public async Task<IDataResult<List<DepartmentListDto>>> GetList()
        {
            return new SuccessDataResult<List<DepartmentListDto>>(await _departmentDal.GetListDto());
        }

        ////[SecuredAspect()]
        public async Task<IDataResult<Department>> GetById(int id)
        {
            return new SuccessDataResult<Department>(await _departmentDal.Get(p => p.Id == id));
        }

        public async Task<IDataResult<DepartmentListDto>> GetMostProductiveDepartment()
        {
            return new SuccessDataResult<DepartmentListDto>(await _departmentDal.GetMostProductiveDepartment());
        }
    }
}