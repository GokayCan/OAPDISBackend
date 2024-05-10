using Entities.Concrete;
using FluentValidation;

namespace Business.Repositories.UserRepository.Validation
{
    public class UserValidator : AbstractValidator<User>
    {
        public UserValidator()
        {
            RuleFor(p => p.FirstName).NotEmpty().WithMessage("Kullanıcı adı boş olamaz");
            RuleFor(p => p.LastName).NotEmpty().WithMessage("Kullanıcı soyadı boş olamaz");
            RuleFor(p => p.PhoneNumber).NotEmpty().WithMessage("Kullanıcı telefon numarası boş olamaz");
            RuleFor(p => p.Email).NotEmpty().WithMessage("Mail adresi boş olamaz");
            RuleFor(p => p.Email).EmailAddress().WithMessage("Geçerli bir mail adresi yazın");
            RuleFor(p => p.ImageUrl).NotEmpty().WithMessage("Kullanıcı resmi boş olamaz");
        }
    }
}