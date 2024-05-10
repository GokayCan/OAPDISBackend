using DataAccess.Repositories.ArticleRepository;
using DataAccess.Repositories.DepartmentRepository;
using DataAccess.Repositories.MeetingRepository;
using DataAccess.Repositories.ProjectRepository;
using DataAccess.Repositories.TeacherRepository;
using Entities.Dtos;
using Microsoft.AspNetCore.SignalR;

namespace Business.Utilities.SignalR;

public class DashboardHub : Hub, IDashboardHub
{
    private readonly IProjectDal _projectDal;
    private readonly IMeetingDal _meetingDal;
    private readonly IArticleDal _articleDal;
    private readonly ITeacherDal _teacherDal;
    private readonly IDepartmentDal _departmentDal;
    
    public DashboardHub(IProjectDal projectDal, IMeetingDal meetingDal, IArticleDal articleDal,ITeacherDal teacherDal,IDepartmentDal departmentDal)
    {
        _projectDal = projectDal;
        _meetingDal = meetingDal;
        _articleDal = articleDal;
        _teacherDal = teacherDal;
        _departmentDal = departmentDal;
    }

    public async Task SendDashboardData()
    {
        var dashboardDto = new DashboardDto
        {
            TotalUsers = await _teacherDal.GetCount(),
            TotalProjects = await _projectDal.GetCount(),
            TotalMeetings = await _meetingDal.GetCount(),
            TotalArticles = await _articleDal.GetCount(),
            MonthlyProjectCounts = await _projectDal.GetMonthlyCount(),
            MonthlyMeetingCounts = await _meetingDal.GetMonthlyCount(),
            MonthlyArticleCounts = await _articleDal.GetMonthlyCount(),
            MostProductiveTeacher = await _teacherDal.GetMostProductiveTeacher(),
            MostProductiveDepartment = await _departmentDal.GetMostProductiveDepartment()
        };

        await Clients.All.SendAsync("ReceiveDashboardData", dashboardDto);
    }

    public async Task SendCalendarData(DateTime date)
    {
        var result = await _meetingDal.GetCountByDate(date);
        await Clients.All.SendAsync("ReceiveCalendarData", result);
    }
}