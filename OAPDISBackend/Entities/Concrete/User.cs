namespace Entities.Concrete
{
    public class User
    {
        public int Id { get; set; }
        public string Email { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string PhoneNumber { get; set; }
        public int TypeId { get; set; }
        public string Title { get; set; }
        public string Task { get; set; }
        public string ImageUrl { get; set; }
        public byte[] PasswordSalt { get; set; }
        public byte[] PasswordHash { get; set; }
        public string ConfirmValue { get; set; }
        public bool IsConfirm { get; set; }
        public string ForgotPasswordValue { get; set; }
        public DateTime? ForgotPasswordRequestDate { get; set; }
        public bool IsForgotPasswordComplete { get; set; }
    }
}