using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.DataAccess;
using Entities.Concrete;
using Entities.Dtos;

namespace DataAccess.Repositories.TeacherArticleRepository
{
    public interface ITeacherArticleDal : IEntityRepository<TeacherArticle>
    {
        Task<List<TeacherArticleListDto>> GetListDto();

        Task<List<TeacherArticleListDto>> GetListByUserId(int UserId);

        Task<TeacherArticleDto> GetDto(int Id);
    }
}