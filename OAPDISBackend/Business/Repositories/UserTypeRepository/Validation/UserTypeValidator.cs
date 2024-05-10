using System;
using System.Collections.Generic;
using FluentValidation;
using System.Text;
using System.Threading.Tasks;
using Entities.Concrete;

namespace Business.Repositories.UserTypeRepository.Validation
{
    public class UserTypeValidator : AbstractValidator<UserType>
    {
        public UserTypeValidator()
        {
        }
    }
}
