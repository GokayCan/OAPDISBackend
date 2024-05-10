using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entities.Concrete;
using Core.Utilities.Result.Abstract;

namespace Business.Repositories.FormActivityCategoryRepository
{
    public interface IFormActivityCategoryService
    {
        Task<IResult> Add(FormActivityCategory formActivityCategory);
        Task<IResult> Update(FormActivityCategory formActivityCategory);
        Task<IResult> Delete(FormActivityCategory formActivityCategory);
        Task<IDataResult<List<FormActivityCategory>>> GetList();
        Task<IDataResult<FormActivityCategory>> GetById(int id);
    }
}
