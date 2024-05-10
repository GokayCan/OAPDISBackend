using Core.DataAccess;
using Entities.Concrete;

namespace DataAccess.Repositories.UserRepository
{
    public interface IUserDal : IEntityRepository<User>
    {
        Task<List<OperationClaim>> GetUserOperatinonClaims(int userId);

        Task<User> AddUser(User user);

        Task<int> GetCount();

        Task<List<string>> GetTeacherEmailsByIds(List<int> teacherIds);
    }
}