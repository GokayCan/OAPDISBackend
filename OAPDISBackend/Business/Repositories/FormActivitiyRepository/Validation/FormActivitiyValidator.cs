using System;
using System.Collections.Generic;
using FluentValidation;
using System.Text;
using System.Threading.Tasks;
using Entities.Concrete;

namespace Business.Repositories.FormActivitiyRepository.Validation
{
    public class FormActivitiyValidator : AbstractValidator<FormActivitiy>
    {
        public FormActivitiyValidator()
        {
        }
    }
}
