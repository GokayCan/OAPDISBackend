using Entities.Concrete;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace DataAccess.Context.EntityFramework
{
    public class SimpleContextDb : DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            IConfigurationRoot configuration = new ConfigurationBuilder()
                .SetBasePath(AppDomain.CurrentDomain.BaseDirectory)
                .AddJsonFile("appsettings.json")
                .Build();
            
            optionsBuilder.UseSqlServer(configuration.GetConnectionString("Default"));
        }

        public DbSet<User> Users { get; set; }
        public DbSet<OperationClaim> OperationClaims { get; set; }
        public DbSet<UserOperationClaim> UserOperationClaims { get; set; }
        public DbSet<EmailParameter> EmailParameters { get; set; }
        public DbSet<Faculty> Faculties { get; set; }
        public DbSet<Department> Departments { get; set; }
        public DbSet<UserType> UserTypes { get; set; }
        public DbSet<Teacher> Teachers { get; set; }
        public DbSet<Article> Articles { get; set; }
        public DbSet<Meeting> Meetings { get; set; }
        public DbSet<Project> Projects { get; set; }
        public DbSet<TeacherProject> TeacherProjects { get; set; }
        public DbSet<TeacherMeeting> TeacherMeetings { get; set; }
        public DbSet<TeacherArticle> TeacherArticles { get; set; }
        public DbSet<Form> Forms { get; set; }
        public DbSet<FormActivitiy> FormActivitiies { get; set; }
        public DbSet<FormActivityCategory> FormActivityCategories { get; set; }
    }
}