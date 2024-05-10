using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entities.Concrete;
using Core.Utilities.Result.Abstract;
using Entities.Dtos;

namespace Business.Repositories.DashboardRepository
{
    public interface IDashboardService
    {
        Task<IDataResult<DashboardDto>> GetDashboardData();
    }
}