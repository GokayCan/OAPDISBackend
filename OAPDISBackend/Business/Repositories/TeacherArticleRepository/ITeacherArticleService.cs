using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entities.Concrete;
using Core.Utilities.Result.Abstract;
using Entities.Dtos;

namespace Business.Repositories.TeacherArticleRepository
{
    public interface ITeacherArticleService
    {
        Task<IResult> Add(TeacherArticleDto teacherArticleDto);

        Task<IResult> Update(TeacherArticleDto teacherArticleDto);

        Task<IResult> Delete(TeacherArticle teacherArticle);

        Task<IDataResult<List<TeacherArticleListDto>>> GetList();

        Task<IDataResult<List<TeacherArticleListDto>>> GetListByUserId(int UserId);

        Task<IDataResult<TeacherArticleDto>> GetById(int id);
    }
}