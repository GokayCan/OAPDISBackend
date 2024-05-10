using System;
using System.Collections.Generic;
using FluentValidation;
using System.Text;
using System.Threading.Tasks;
using Entities.Concrete;

namespace Business.Repositories.TeacherProjectRepository.Validation
{
    public class TeacherProjectValidator : AbstractValidator<TeacherProject>
    {
        public TeacherProjectValidator()
        {
        }
    }
}
