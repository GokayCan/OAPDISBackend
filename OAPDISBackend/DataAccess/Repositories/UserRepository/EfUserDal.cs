using Core.DataAccess.EntityFramework;
using DataAccess.Context.EntityFramework;
using Entities.Concrete;
using Microsoft.EntityFrameworkCore;

namespace DataAccess.Repositories.UserRepository
{
    public class EfUserDal : EfEntityRepositoryBase<User, SimpleContextDb>, IUserDal
    {
        public async Task<User> AddUser(User user)
        {
            using (var context = new SimpleContextDb())
            {
                var result = context.Entry(user);
                result.State = EntityState.Added;
                await context.SaveChangesAsync();
                return result.Entity;
            }
        }

        public async Task<List<OperationClaim>> GetUserOperatinonClaims(int userId)
        {
            using (var context = new SimpleContextDb())
            {
                var result = from userOperationClaim in context.UserOperationClaims.Where(p => p.UserId == userId)
                             join operationClaim in context.OperationClaims on userOperationClaim.OperationClaimId equals operationClaim.Id
                             select new OperationClaim
                             {
                                 Id = operationClaim.Id,
                                 Name = operationClaim.Name
                             };
                return await result.OrderBy(p => p.Name).ToListAsync();
            }
        }

        public async Task<int> GetCount()
        {
            using (var context = new SimpleContextDb())
            {
                return await context.Users.CountAsync();
            }
        }

        public async Task<List<string>> GetTeacherEmailsByIds(List<int> teacherIds)
        {
            using (var context = new SimpleContextDb())
            {
                return await context.Users.Where(p => teacherIds.Contains(p.Id)).Select(p => p.Email).ToListAsync();
            }
        }
    }
}