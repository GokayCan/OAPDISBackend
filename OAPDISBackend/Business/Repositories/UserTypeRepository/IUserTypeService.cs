using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entities.Concrete;
using Core.Utilities.Result.Abstract;

namespace Business.Repositories.UserTypeRepository
{
    public interface IUserTypeService
    {
        Task<IResult> Add(UserType userType);
        Task<IResult> Update(UserType userType);
        Task<IResult> Delete(UserType userType);
        Task<IDataResult<List<UserType>>> GetList();
        Task<IDataResult<UserType>> GetById(int id);
    }
}
