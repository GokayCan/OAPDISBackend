using Core.Utilities.Result.Abstract;
using Entities.Concrete;
using Entities.Dtos;

namespace Business.Repositories.TeacherRepository
{
    public interface ITeacherService
    {
        Task<IResult> Add(TeacherDto teacher);

        Task<IResult> Update(TeacherDto teacher);

        Task<IResult> Delete(Teacher teacher);

        Task<IDataResult<List<TeacherListDto>>> GetList();

        Task<IDataResult<TeacherDto>> GetById(int id);

        Task<IDataResult<TeacherDto>> GetByUserId(int id);

        Task<IDataResult<int[]>> GetDashboardStatistics(int userId);

        Task<IDataResult<TeacherDto>> GetMostProductiveTeacher();
    }
}