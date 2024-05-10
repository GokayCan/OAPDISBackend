using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Business.Repositories.UserTypeRepository;
using Entities.Concrete;
using Business.Aspects.Secured;
using Core.Aspects.Validation;
using Core.Aspects.Caching;
using Core.Aspects.Performance;
using Business.Repositories.UserTypeRepository.Validation;
using Business.Repositories.UserTypeRepository.Constants;
using Core.Utilities.Result.Abstract;
using Core.Utilities.Result.Concrete;
using DataAccess.Repositories.UserTypeRepository;

namespace Business.Repositories.UserTypeRepository
{
    public class UserTypeManager : IUserTypeService
    {
        private readonly IUserTypeDal _userTypeDal;

        public UserTypeManager(IUserTypeDal userTypeDal)
        {
            _userTypeDal = userTypeDal;
        }

        //[SecuredAspect()]
        [ValidationAspect(typeof(UserTypeValidator))]
        [RemoveCacheAspect("IUserTypeService.Get")]

        public async Task<IResult> Add(UserType userType)
        {
            await _userTypeDal.Add(userType);
            return new SuccessResult(UserTypeMessages.Added);
        }

        //[SecuredAspect()]
        [ValidationAspect(typeof(UserTypeValidator))]
        [RemoveCacheAspect("IUserTypeService.Get")]

        public async Task<IResult> Update(UserType userType)
        {
            await _userTypeDal.Update(userType);
            return new SuccessResult(UserTypeMessages.Updated);
        }

        //[SecuredAspect()]
        [RemoveCacheAspect("IUserTypeService.Get")]

        public async Task<IResult> Delete(UserType userType)
        {
            await _userTypeDal.Delete(userType);
            return new SuccessResult(UserTypeMessages.Deleted);
        }

        //[SecuredAspect()]
        [CacheAspect()]
        [PerformanceAspect()]
        public async Task<IDataResult<List<UserType>>> GetList()
        {
            return new SuccessDataResult<List<UserType>>(await _userTypeDal.GetAll());
        }

        //[SecuredAspect()]
        public async Task<IDataResult<UserType>> GetById(int id)
        {
            return new SuccessDataResult<UserType>(await _userTypeDal.Get(p => p.Id == id));
        }

    }
}
