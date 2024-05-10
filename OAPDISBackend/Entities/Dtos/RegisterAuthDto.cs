using Microsoft.AspNetCore.Http;

namespace Entities.Dtos
{
    public class RegisterAuthDto
    {
        public string FirstName { get; set; }
        public int TypeId { get; set; }
        public string Title { get; set; }
        public string Task { get; set; }
        public string LastName { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public IFormFile Image { get; set; }
    }
}