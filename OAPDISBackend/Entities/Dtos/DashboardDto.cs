using Entities.Concrete;

namespace Entities.Dtos
{
    public class DashboardDto
    {
        public int TotalUsers { get; set; }
        public int TotalProjects { get; set; }
        public int TotalMeetings { get; set; }
        public int TotalArticles { get; set; }

        public List<MonthlyCountDto> MonthlyProjectCounts { get; set; }
        public List<MonthlyCountDto> MonthlyMeetingCounts { get; set; }
        public List<MonthlyCountDto> MonthlyArticleCounts { get; set; }

        public TeacherDto MostProductiveTeacher { get; set; }
        public DepartmentListDto MostProductiveDepartment { get; set; }
    }
}