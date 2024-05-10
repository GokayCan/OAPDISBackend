using System;
using System.Collections.Generic;
using FluentValidation;
using System.Text;
using System.Threading.Tasks;
using Entities.Concrete;

namespace Business.Repositories.TeacherArticleRepository.Validation
{
    public class TeacherArticleValidator : AbstractValidator<TeacherArticle>
    {
        public TeacherArticleValidator()
        {
        }
    }
}
