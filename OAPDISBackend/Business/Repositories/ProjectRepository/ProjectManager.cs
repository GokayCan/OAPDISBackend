using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Business.Repositories.ProjectRepository;
using Entities.Concrete;
using Business.Aspects.Secured;
using Core.Aspects.Validation;
using Core.Aspects.Caching;
using Core.Aspects.Performance;
using Business.Repositories.ProjectRepository.Validation;
using Business.Repositories.ProjectRepository.Constants;
using Core.Utilities.Result.Abstract;
using Core.Utilities.Result.Concrete;
using DataAccess.Repositories.ProjectRepository;

namespace Business.Repositories.ProjectRepository
{
    public class ProjectManager : IProjectService
    {
        private readonly IProjectDal _projectDal;

        public ProjectManager(IProjectDal projectDal)
        {
            _projectDal = projectDal;
        }

        //[SecuredAspect()]
        [ValidationAspect(typeof(ProjectValidator))]
        [RemoveCacheAspect("IProjectService.Get")]
        public async Task<IDataResult<Project>> Add(Project project)
        {
            try
            {
                return new SuccessDataResult<Project>(await _projectDal.AddProject(project), ProjectMessages.Added);
            }
            catch
            {
                return new ErrorDataResult<Project>(ProjectMessages.NotAdded);
            }
        }

        //[SecuredAspect()]
        [ValidationAspect(typeof(ProjectValidator))]
        [RemoveCacheAspect("IProjectService.Get")]
        public async Task<IResult> Update(Project project)
        {
            try
            {
                await _projectDal.Update(project);
                return new SuccessResult(ProjectMessages.Updated);
            }
            catch
            {
                return new ErrorResult(ProjectMessages.NotUpdated);
            }
        }

        //[SecuredAspect()]
        [RemoveCacheAspect("IProjectService.Get")]
        public async Task<IResult> Delete(Project project)
        {
            try
            {
                await _projectDal.Delete(project);
                return new SuccessResult(ProjectMessages.Deleted);
            }
            catch
            {
                return new ErrorResult(ProjectMessages.NotDeleted);
            }
        }

        //[SecuredAspect()]
        [CacheAspect()]
        [PerformanceAspect()]
        public async Task<IDataResult<List<Project>>> GetList()
        {
            return new SuccessDataResult<List<Project>>(await _projectDal.GetAll());
        }

        //[SecuredAspect()]
        public async Task<IDataResult<Project>> GetById(int id)
        {
            return new SuccessDataResult<Project>(await _projectDal.Get(p => p.Id == id));
        }
    }
}