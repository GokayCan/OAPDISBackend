using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entities.Concrete;
using Core.Utilities.Result.Abstract;

namespace Business.Repositories.FormRepository
{
    public interface IFormService
    {
        Task<IResult> Add(Form form);
        Task<IResult> Update(Form form);
        Task<IResult> Delete(Form form);
        Task<IDataResult<List<Form>>> GetList();
        Task<IDataResult<Form>> GetById(int id);
    }
}
