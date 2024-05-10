using System;
using System.Collections.Generic;
using FluentValidation;
using System.Text;
using System.Threading.Tasks;
using Entities.Concrete;

namespace Business.Repositories.FormRepository.Validation
{
    public class FormValidator : AbstractValidator<Form>
    {
        public FormValidator()
        {
        }
    }
}
