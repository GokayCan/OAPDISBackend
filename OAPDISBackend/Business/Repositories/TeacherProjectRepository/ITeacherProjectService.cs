using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entities.Concrete;
using Core.Utilities.Result.Abstract;
using Entities.Dtos;

namespace Business.Repositories.TeacherProjectRepository
{
    public interface ITeacherProjectService
    {
        Task<IResult> Add(TeacherProjectDto teacherProjectDto);

        Task<IResult> Update(TeacherProjectDto teacherProjectDto);

        Task<IResult> Delete(TeacherProject teacherProject);

        Task<IDataResult<List<TeacherProjectListDto>>> GetList();

        Task<IDataResult<List<TeacherProjectListDto>>> GetListByUserId(int UserId);

        Task<IDataResult<TeacherProjectDto>> GetById(int id);
    }
}