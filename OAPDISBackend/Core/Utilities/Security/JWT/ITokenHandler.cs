using Entities.Concrete;

namespace Core.Utilities.Security.JWT
{
    public interface ITokenHandler
    {
        Token CreateToken(User user, List<string> operationClaims,int teacherId);
    }
}
