using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.DataAccess.EntityFramework;
using Entities.Concrete;
using DataAccess.Repositories.MeetingRepository;
using DataAccess.Context.EntityFramework;
using Microsoft.EntityFrameworkCore;
using Entities.Dtos;

namespace DataAccess.Repositories.MeetingRepository
{
    public class EfMeetingDal : EfEntityRepositoryBase<Meeting, SimpleContextDb>, IMeetingDal
    {
        public async Task<Meeting> AddMeeting(Meeting meeting)
        {
            using (var context = new SimpleContextDb())
            {
                var data = context.Entry(meeting);
                data.State = Microsoft.EntityFrameworkCore.EntityState.Added;
                await context.SaveChangesAsync();
                return data.Entity;
            }
        }

        public async Task<List<MeetingDailyCountDto>> GetDailyCount()
        {
            using (var context = new SimpleContextDb())
            {
                var thirtyDaysAgo = DateTime.Today.AddDays(-30);

                var result = await (from meeting in context.Meetings
                                    where meeting.Date >= thirtyDaysAgo
                                    group meeting by meeting.Date.Date into groupedMeetings
                                    select new MeetingDailyCountDto
                                    {
                                        Date = groupedMeetings.Key,
                                        Count = groupedMeetings.Count()
                                    }).ToListAsync();

                return result;
            }
        }

        public async Task<int> GetCount()
        {
            using (var context = new SimpleContextDb())
            {
                return await context.Meetings.CountAsync();
            }
        }

        public async Task<List<MonthlyCountDto>> GetMonthlyCount()
        {
            using (var context = new SimpleContextDb())
            {
                var today = DateTime.Today;
                var last12Months = Enumerable.Range(0, 12)
                    .Select(i => today.AddMonths(-i).Month)
                    .ToList();

                var result = await context.Meetings
                    .Where(article => last12Months.Contains(article.Date.Month))
                    .GroupBy(article => article.Date.Month)
                    .Select(groupedArticles => new MonthlyCountDto
                    {
                        Month = groupedArticles.Key,
                        Count = groupedArticles.Count()
                    })
                    .ToListAsync();

                // Eksik ayları ekle
                var missingMonths = last12Months.Except(result.Select(dto => dto.Month)).ToList();
                foreach (var missingMonth in missingMonths)
                {
                    result.Add(new MonthlyCountDto
                    {
                        Month = missingMonth,
                        Count = 0
                    });
                }

                // last12Months sırasına göre yeniden düzenle
                var sortedResult = last12Months
                    .Select(month => result.FirstOrDefault(dto => dto.Month == month) ?? new MonthlyCountDto { Month = month, Count = 0 })
                    .ToList();

                return sortedResult;
            }
        }

        public async Task<MeetingDailyCountDto> GetCountByDate(DateTime time)
        {
            using (var context = new SimpleContextDb())
            {
                var result = await context.Meetings
                    .Where(article => article.Date.Date == time.Date) // Belirli bir tarihe ait verileri filtrele
                    .GroupBy(article => article.Date.Date)
                    .Select(groupedArticles => new MeetingDailyCountDto
                    {
                        Date = groupedArticles.Key,
                        Count = groupedArticles.Count()
                    })
                    .FirstOrDefaultAsync();

                return result ?? new MeetingDailyCountDto { Date = time, Count = 0 }; // Eğer veri bulunamazsa varsayılan değeri döndür
            }
        }

        public async Task<List<int>> GetMeetingsForReminder()
        {
            using (var context = new SimpleContextDb())
            {
                return await context.Meetings
                    .Where(m => m.Date.Date == DateTime.Today.AddDays(1)).Select(m => m.Id).ToListAsync();
            }
        }
    }
}