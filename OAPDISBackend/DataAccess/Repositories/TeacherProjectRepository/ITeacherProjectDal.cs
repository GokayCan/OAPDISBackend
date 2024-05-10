using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.DataAccess;
using Entities.Concrete;
using Entities.Dtos;

namespace DataAccess.Repositories.TeacherProjectRepository
{
    public interface ITeacherProjectDal : IEntityRepository<TeacherProject>
    {
        Task<List<TeacherProjectListDto>> GetListDto();

        Task<List<TeacherProjectListDto>> GetListByUserId(int UserId);

        Task<TeacherProjectDto> GetDto(int id);
    }
}