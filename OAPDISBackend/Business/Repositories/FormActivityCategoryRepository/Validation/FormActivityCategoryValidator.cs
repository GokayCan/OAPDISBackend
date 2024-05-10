using System;
using System.Collections.Generic;
using FluentValidation;
using System.Text;
using System.Threading.Tasks;
using Entities.Concrete;

namespace Business.Repositories.FormActivityCategoryRepository.Validation
{
    public class FormActivityCategoryValidator : AbstractValidator<FormActivityCategory>
    {
        public FormActivityCategoryValidator()
        {
        }
    }
}
