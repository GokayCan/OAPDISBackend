using Business.Utilities.Email;
using DataAccess.Repositories.MeetingRepository;
using DataAccess.Repositories.TeacherMeetingRepository;
using DataAccess.Repositories.TeacherRepository;
using DataAccess.Repositories.UserRepository;
using Entities.Concrete;
using Microsoft.EntityFrameworkCore;

namespace Business.Utilities.Hangfire;

public class HangfireJobs
{
    private readonly IEmailService _emailService;
    private readonly IMeetingDal _meetingDal;
    private readonly ITeacherMeetingDal _teacherMeetingDal;
    private readonly ITeacherDal _teacherDal;
    private readonly IUserDal _userDal;

    public HangfireJobs(IEmailService emailService, IMeetingDal meetingDal, ITeacherDal teacherDal, IUserDal userDal, ITeacherMeetingDal teacherMeetingDal)
    {
        _emailService = emailService;
        _meetingDal = meetingDal;
        _teacherDal = teacherDal;
        _userDal = userDal;
        _teacherMeetingDal = teacherMeetingDal;
    }

    public async Task SendMeetingReminderEmail()
    {
        var meetingIds = await _meetingDal.GetMeetingsForReminder();
        // bu meetingslerin teacherMeetting ile eslesenlerini getir ve sonrada teacherIdleri yakala
        //sonra teacherIdler ile teacher tablosundan teacherlari getir
        if (meetingIds.Count > 0)
        {
            var teacherIds = await _teacherMeetingDal.GetTeacherIdsByMeetingIds(meetingIds);
            if (teacherIds.Count > 0)
            {
                teacherIds = teacherIds.Distinct().ToList();
                var userIds = await _teacherDal.GetUserIdsByTeacherIds(teacherIds);
                if (userIds.Count > 0)
                {
                    var userEmails = await _userDal.GetTeacherEmailsByIds(userIds);
                    userEmails = userEmails.Distinct().ToList();
                    await _emailService.SendEmailAsync("maviay7852@gmail.com", "Meeting Reminder", "You have a meeting tomorrow");
                }
            }
        }

        // foreach (var email in userEmails)
        // {
        //     await _emailService.SendEmailAsync(email, "Meeting Reminder", "You have a meeting tomorrow");
        // }
    }
}