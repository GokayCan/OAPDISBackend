using System.Security.Claims;

namespace Core.Extensions
{
    public static class ClaimsExtensions
    {
        public static void AddEmail(this ICollection<Claim> claims, string email)
        {
            claims.Add(new Claim(ClaimTypes.Email, email));
        }

        public static void AddId(this ICollection<Claim> claims, int id)
        {
            claims.Add(new Claim(ClaimTypes.NameIdentifier, id.ToString()));
        }

        public static void AddTeacherId(this ICollection<Claim> claims, int teacherId)
        {
            claims.Add(new Claim(ClaimTypes.Actor, teacherId.ToString()));
        }

        public static void AddTypeId(this ICollection<Claim> claims, int typeId)
        {
            claims.Add(new Claim(ClaimTypes.UserData, typeId.ToString()));
        }

        public static void AddName(this ICollection<Claim> claims, string name)
        {
            claims.Add(new Claim(ClaimTypes.Name, name));
        }

        public static void AddImageUrl(this ICollection<Claim> claims, string imageUrl)
        {
            claims.Add(new Claim(ClaimTypes.UserData, imageUrl));
        }

        public static void AddRoles(this ICollection<Claim> claims, string[] roles)
        {
            roles.ToList().ForEach(roles => claims.Add(new Claim(ClaimTypes.Role, roles)));
        }
    }
}