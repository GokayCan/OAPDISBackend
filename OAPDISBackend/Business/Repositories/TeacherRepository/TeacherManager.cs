using Business.Abstract;
using Business.Repositories.TeacherRepository.Constants;
using Business.Repositories.TeacherRepository.Validation;
using Business.Repositories.UserRepository;
using Core.Aspects.Caching;
using Core.Aspects.Performance;
using Core.Aspects.Validation;
using Core.Utilities.Result.Abstract;
using Core.Utilities.Result.Concrete;
using DataAccess.Repositories.TeacherRepository;
using Entities.Concrete;
using Entities.Dtos;

namespace Business.Repositories.TeacherRepository
{
    public class TeacherManager : ITeacherService
    {
        private readonly ITeacherDal _teacherDal;
        private readonly IUserService _userService;
        private readonly IFileService _fileService;

        public TeacherManager(ITeacherDal teacherDal, IUserService userService, IFileService fileService)
        {
            _teacherDal = teacherDal;
            _userService = userService;
            _fileService = fileService;
        }

        ////[SecuredAspect()]
        [ValidationAspect(typeof(TeacherValidator))]
        [RemoveCacheAspect("ITeacherService.Get")]
        public async Task<IResult> Add(TeacherDto teacherDto)
        {
            try
            {
                UserDto userDto = new UserDto()
                {
                    Email = teacherDto.Email,
                    Password = teacherDto.Password,
                    FirstName = teacherDto.FirstName,
                    PhoneNumber = teacherDto.PhoneNumber,
                    LastName = teacherDto.LastName,
                    Title = teacherDto.Title,
                    Task = teacherDto.Task,
                    TypeId = 2,
                    Image = teacherDto.Image,
                };

                var addedUser = await _userService.Add(userDto);

                Teacher teacher = new()
                {
                    UserId = addedUser.Data.Id,
                    DepartmentId = teacherDto.DepartmentId,
                };

                await _teacherDal.Add(teacher);
                return new SuccessResult(TeacherMessages.Added);
            }
            catch
            {
                return new ErrorResult(TeacherMessages.NotAdded);
            }
        }

        ////[SecuredAspect()]
        [ValidationAspect(typeof(TeacherValidator))]
        [RemoveCacheAspect("ITeacherService.Get")]
        public async Task<IResult> Update(TeacherDto teacherDto)
        {
            try
            {
                var teacher = await _teacherDal.Get(p => p.Id == teacherDto.Id);

                UserDto user = new()
                {
                    Id = teacherDto.UserId,
                    FirstName = teacherDto.FirstName,
                    LastName = teacherDto.LastName,
                    Email = teacherDto.Email,
                    PhoneNumber = teacherDto.PhoneNumber,
                    Title = teacherDto.Title,
                    Task = teacherDto.Task,
                    Image = teacherDto.Image,
                    Password = teacherDto.Password,
                    TypeId = 2,
                };

                await _userService.Update(user);

                teacher.DepartmentId = teacherDto.DepartmentId;

                await _teacherDal.Update(teacher);
                return new SuccessResult(TeacherMessages.Updated);
            }
            catch
            {
                return new ErrorResult(TeacherMessages.NotUpdated);
            }
        }

        ////[SecuredAspect()]
        [RemoveCacheAspect("ITeacherService.Get")]
        public async Task<IResult> Delete(Teacher teacher)
        {
            try
            {
                await _teacherDal.Delete(teacher);
                return new SuccessResult(TeacherMessages.Deleted);
            }
            catch
            {
                return new ErrorResult(TeacherMessages.NotDeleted);
            }
        }

        ////[SecuredAspect()]
        [CacheAspect()]
        [PerformanceAspect()]
        public async Task<IDataResult<List<TeacherListDto>>> GetList()
        {
            return new SuccessDataResult<List<TeacherListDto>>(await _teacherDal.GetListDto());
        }

        ////[SecuredAspect()]
        public async Task<IDataResult<TeacherDto>> GetById(int id)
        {
            return new SuccessDataResult<TeacherDto>(await _teacherDal.GetDto(id));
        }

        public async Task<IDataResult<TeacherDto>> GetByUserId(int id)
        {
            return new SuccessDataResult<TeacherDto>(await _teacherDal.GetByUserId(id));
        }

        ////[SecuredAspect()]
        [CacheAspect()]
        [PerformanceAspect()]
        public async Task<IDataResult<int[]>> GetDashboardStatistics(int userId)
        {
            return new SuccessDataResult<int[]>(await _teacherDal.GetDashboardStatistics(userId));
        }

        public async Task<IDataResult<TeacherDto>> GetMostProductiveTeacher()
        {
            return new SuccessDataResult<TeacherDto>(await _teacherDal.GetMostProductiveTeacher());
        }
    }
}