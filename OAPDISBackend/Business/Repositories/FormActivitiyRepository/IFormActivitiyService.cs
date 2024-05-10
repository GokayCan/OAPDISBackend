using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entities.Concrete;
using Core.Utilities.Result.Abstract;

namespace Business.Repositories.FormActivitiyRepository
{
    public interface IFormActivitiyService
    {
        Task<IResult> Add(FormActivitiy formActivitiy);
        Task<IResult> Update(FormActivitiy formActivitiy);
        Task<IResult> Delete(FormActivitiy formActivitiy);
        Task<IDataResult<List<FormActivitiy>>> GetList();
        Task<IDataResult<FormActivitiy>> GetById(int id);
    }
}
