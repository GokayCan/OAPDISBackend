namespace Business.Utilities.SignalR;

public interface IDashboardHub
{
    Task SendDashboardData();
    Task SendCalendarData(DateTime date);
}