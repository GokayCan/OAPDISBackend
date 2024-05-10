using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entities.Concrete;
using Core.Utilities.Result.Abstract;
using Entities.Dtos;

namespace Business.Repositories.DepartmentRepository
{
    public interface IDepartmentService
    {
        Task<IResult> Add(Department department);

        Task<IResult> Update(Department department);

        Task<IResult> Delete(Department department);

        Task<IDataResult<List<DepartmentListDto>>> GetList();

        Task<IDataResult<DepartmentListDto>> GetMostProductiveDepartment();

        Task<IDataResult<Department>> GetById(int id);
    }
}