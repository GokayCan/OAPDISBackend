using Core.DataAccess;
using Core.Utilities.Result.Abstract;
using Entities.Concrete;
using Entities.Dtos;

namespace DataAccess.Repositories.TeacherRepository
{
    public interface ITeacherDal : IEntityRepository<Teacher>
    {
        Task<TeacherDto> GetDto(int Id);

        Task<TeacherDto> GetByUserId(int Id);

        Task<List<TeacherListDto>> GetListDto();

        Task<int[]> GetDashboardStatistics(int UserId);

        Task<TeacherDto> GetMostProductiveTeacher();

        Task<int> GetCount();

        Task<List<int>> GetUserIdsByTeacherIds(List<int> teacherIds);
    }
}