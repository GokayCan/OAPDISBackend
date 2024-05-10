using Core.Utilities.Result.Abstract;
using Core.Utilities.Result.Concrete;
using DataAccess.Repositories.ArticleRepository;
using DataAccess.Repositories.DepartmentRepository;
using DataAccess.Repositories.MeetingRepository;
using DataAccess.Repositories.ProjectRepository;
using DataAccess.Repositories.TeacherRepository;
using DataAccess.Repositories.UserRepository;
using Entities.Dtos;

namespace Business.Repositories.DashboardRepository
{
    public class DashboardManager : IDashboardService
    {

        private readonly IUserDal _userDal;
        private readonly IProjectDal _projectDal;
        private readonly IMeetingDal _meetingDal;
        private readonly IArticleDal _articleDal;
        private readonly ITeacherDal _teacherDal;
        private readonly IDepartmentDal _departmentDal;

        public DashboardManager(IUserDal userDal, IProjectDal projectDal, IMeetingDal meetingDal, IArticleDal articleDal,ITeacherDal teacherDal,IDepartmentDal departmentDal)
        {
            _userDal = userDal;
            _projectDal = projectDal;
            _meetingDal = meetingDal;
            _articleDal = articleDal;
            _teacherDal = teacherDal;
            _departmentDal = departmentDal;
        }

        public async Task<IDataResult<DashboardDto>> GetDashboardData()
        {
            DashboardDto dashboardDto = new DashboardDto();
            dashboardDto.TotalUsers = await _teacherDal.GetCount();
            dashboardDto.TotalProjects = await _projectDal.GetCount();
            dashboardDto.TotalMeetings = await _meetingDal.GetCount();
            dashboardDto.TotalArticles = await _articleDal.GetCount();

            dashboardDto.MonthlyProjectCounts = await _projectDal.GetMonthlyCount();
            dashboardDto.MonthlyMeetingCounts = await _meetingDal.GetMonthlyCount();
            dashboardDto.MonthlyArticleCounts = await _articleDal.GetMonthlyCount();

            dashboardDto.MostProductiveTeacher = await _teacherDal.GetMostProductiveTeacher();
            dashboardDto.MostProductiveDepartment = await _departmentDal.GetMostProductiveDepartment();
            return new SuccessDataResult<DashboardDto>(dashboardDto);
        }
    }
}