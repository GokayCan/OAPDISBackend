using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;
using Business.Repositories.TeacherProjectRepository;
using Entities.Concrete;
using Business.Aspects.Secured;
using Core.Aspects.Validation;
using Core.Aspects.Caching;
using Core.Aspects.Performance;
using Business.Repositories.TeacherProjectRepository.Validation;
using Business.Repositories.TeacherProjectRepository.Constants;
using Core.Utilities.Result.Abstract;
using Core.Utilities.Result.Concrete;
using DataAccess.Repositories.TeacherProjectRepository;
using Entities.Dtos;
using Business.Repositories.ProjectRepository;

namespace Business.Repositories.TeacherProjectRepository
{
    public class TeacherProjectManager : ITeacherProjectService
    {
        private readonly ITeacherProjectDal _teacherProjectDal;
        private readonly IProjectService _projectService;

        public TeacherProjectManager(ITeacherProjectDal teacherProjectDal, IProjectService projectService)
        {
            _teacherProjectDal = teacherProjectDal;
            _projectService = projectService;
        }

        //[SecuredAspect()]
        [ValidationAspect(typeof(TeacherProjectValidator))]
        [RemoveCacheAspect("ITeacherProjectService.Get")]
        public async Task<IResult> Add(TeacherProjectDto teacherProjectDto)
        {
            using var scope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled);
            try
            {
                Project project = new Project();
                project.Id = teacherProjectDto.Id;
                project.Title = teacherProjectDto.Title;
                project.Description = teacherProjectDto.Description;
                project.Date = teacherProjectDto.Date;
                var addedProject = await _projectService.Add(project);

                TeacherProject teacherProject = new TeacherProject();
                teacherProject.TeacherId = teacherProjectDto.TeacherId;
                teacherProject.ProjectId = addedProject.Data.Id;

                await _teacherProjectDal.Add(teacherProject);
                scope.Complete();
                return new SuccessResult(TeacherProjectMessages.Added);
            }
            catch
            {
                scope.Dispose();
                return new ErrorResult(TeacherProjectMessages.NotAdded);
            }
        }

        //[SecuredAspect()]
        [ValidationAspect(typeof(TeacherProjectValidator))]
        [RemoveCacheAspect("ITeacherProjectService.Get")]
        public async Task<IResult> Update(TeacherProjectDto teacherProjectDto)
        {
            using var scope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled);
            try
            {
                Project Project = _projectService.GetById(teacherProjectDto.Id).Result.Data;

                Project.Title = teacherProjectDto.Title;
                Project.Description = teacherProjectDto.Description;
                Project.Date = teacherProjectDto.Date;

                await _projectService.Update(Project);

                TeacherProject teacherProject = await _teacherProjectDal.Get(p => p.Id == teacherProjectDto.Id);

                teacherProject.TeacherId = teacherProjectDto.TeacherId;
                teacherProject.ProjectId = Project.Id;

                await _teacherProjectDal.Update(teacherProject);
                scope.Complete();
                return new SuccessResult(TeacherProjectMessages.Updated);
            }
            catch
            {
                scope.Dispose();
                return new ErrorResult(TeacherProjectMessages.NotUpdated);
            }
        }

        //[SecuredAspect()]
        [RemoveCacheAspect("ITeacherProjectService.Get")]
        public async Task<IResult> Delete(TeacherProject teacherProject)
        {
            using var scope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled);
            try
            {
                await _teacherProjectDal.Delete(teacherProject);
                await _projectService.Delete(new Project() { Id = teacherProject.ProjectId });
                scope.Complete();
                return new SuccessResult(TeacherProjectMessages.Deleted);
            }
            catch
            {
                scope.Dispose();
                return new ErrorResult(TeacherProjectMessages.NotDeleted);
            }
        }

        //[SecuredAspect()]
        [CacheAspect()]
        [PerformanceAspect()]
        public async Task<IDataResult<List<TeacherProjectListDto>>> GetList()
        {
            return new SuccessDataResult<List<TeacherProjectListDto>>(await _teacherProjectDal.GetListDto());
        }

        //[SecuredAspect()]
        public async Task<IDataResult<TeacherProjectDto>> GetById(int id)
        {
            return new SuccessDataResult<TeacherProjectDto>(await _teacherProjectDal.GetDto(id));
        }

        public async Task<IDataResult<List<TeacherProjectListDto>>> GetListByUserId(int UserId)
        {
            return new SuccessDataResult<List<TeacherProjectListDto>>(await _teacherProjectDal.GetListByUserId(UserId));
        }
    }
}