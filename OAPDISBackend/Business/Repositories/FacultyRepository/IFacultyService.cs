using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entities.Concrete;
using Core.Utilities.Result.Abstract;

namespace Business.Repositories.FacultyRepository
{
    public interface IFacultyService
    {
        Task<IResult> Add(Faculty faculty);
        Task<IResult> Update(Faculty faculty);
        Task<IResult> Delete(Faculty faculty);
        Task<IDataResult<List<Faculty>>> GetList();
        Task<IDataResult<Faculty>> GetById(int id);
    }
}
