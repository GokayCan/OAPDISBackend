using Autofac;
using Autofac.Extras.DynamicProxy;
using Business.Abstract;
using Business.Authentication;
using Business.Repositories.EmailParameterRepository;
using Business.Repositories.OperationClaimRepository;
using Business.Repositories.UserOperationClaimRepository;
using Business.Repositories.UserRepository;
using Core.Utilities.Interceptors;
using Core.Utilities.Security.JWT;
using DataAccess.Repositories.EmailParameterRepository;
using DataAccess.Repositories.OperationClaimRepository;
using DataAccess.Repositories.UserOperationClaimRepository;
using Business.Repositories.FacultyRepository;
using DataAccess.Repositories.FacultyRepository;
using Business.Repositories.DepartmentRepository;
using DataAccess.Repositories.DepartmentRepository;
using Business.Repositories.UserTypeRepository;
using DataAccess.Repositories.UserTypeRepository;
using Business.Repositories.TeacherRepository;
using DataAccess.Repositories.TeacherRepository;
using Business.Repositories.ArticleRepository;
using DataAccess.Repositories.ArticleRepository;
using Business.Repositories.MeetingRepository;
using DataAccess.Repositories.MeetingRepository;
using Business.Repositories.ProjectRepository;
using DataAccess.Repositories.ProjectRepository;
using Business.Repositories.TeacherProjectRepository;
using DataAccess.Repositories.TeacherProjectRepository;
using Business.Repositories.TeacherMeetingRepository;
using DataAccess.Repositories.TeacherMeetingRepository;
using Business.Repositories.TeacherArticleRepository;
using DataAccess.Repositories.TeacherArticleRepository;
using Business.Repositories.FormRepository;
using DataAccess.Repositories.FormRepository;
using Business.Repositories.FormActivitiyRepository;
using DataAccess.Repositories.FormActivitiyRepository;
using Business.Repositories.FormActivityCategoryRepository;
using DataAccess.Repositories.FormActivityCategoryRepository;
using DataAccess.Repositories.UserRepository;

namespace Business.DependencyResolvers.Autofac
{
    public class AutofacBusinessModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<OperationClaimManager>().As<IOperationClaimService>();
            builder.RegisterType<EfOperationClaimDal>().As<IOperationClaimDal>();

            builder.RegisterType<UserManager>().As<IUserService>();
            builder.RegisterType<EfUserDal>().As<IUserDal>();

            builder.RegisterType<UserOperationClaimManager>().As<IUserOperationClaimService>();
            builder.RegisterType<EfUserOperationClaimDal>().As<IUserOperationClaimDal>();

            builder.RegisterType<EmailParameterManager>().As<IEmailParameterService>();
            builder.RegisterType<EfEmailParameterDal>().As<IEmailParameterDal>();

            builder.RegisterType<AuthManager>().As<IAuthService>();

            builder.RegisterType<TokenHandler>().As<ITokenHandler>();

            builder.RegisterType<FacultyManager>().As<IFacultyService>().SingleInstance();
            builder.RegisterType<EfFacultyDal>().As<IFacultyDal>().SingleInstance();

            builder.RegisterType<DepartmentManager>().As<IDepartmentService>().SingleInstance();
            builder.RegisterType<EfDepartmentDal>().As<IDepartmentDal>().SingleInstance();

            builder.RegisterType<UserTypeManager>().As<IUserTypeService>().SingleInstance();
            builder.RegisterType<EfUserTypeDal>().As<IUserTypeDal>().SingleInstance();

            builder.RegisterType<TeacherManager>().As<ITeacherService>().SingleInstance();
            builder.RegisterType<EfTeacherDal>().As<ITeacherDal>().SingleInstance();

            builder.RegisterType<ArticleManager>().As<IArticleService>().SingleInstance();
            builder.RegisterType<EfArticleDal>().As<IArticleDal>().SingleInstance();

            builder.RegisterType<MeetingManager>().As<IMeetingService>().SingleInstance();
            builder.RegisterType<EfMeetingDal>().As<IMeetingDal>().SingleInstance();

            builder.RegisterType<ProjectManager>().As<IProjectService>().SingleInstance();
            builder.RegisterType<EfProjectDal>().As<IProjectDal>().SingleInstance();

            builder.RegisterType<TeacherProjectManager>().As<ITeacherProjectService>().SingleInstance();
            builder.RegisterType<EfTeacherProjectDal>().As<ITeacherProjectDal>().SingleInstance();

            builder.RegisterType<TeacherMeetingManager>().As<ITeacherMeetingService>().SingleInstance();
            builder.RegisterType<EfTeacherMeetingDal>().As<ITeacherMeetingDal>().SingleInstance();

            builder.RegisterType<TeacherArticleManager>().As<ITeacherArticleService>().SingleInstance();
            builder.RegisterType<EfTeacherArticleDal>().As<ITeacherArticleDal>().SingleInstance();

            builder.RegisterType<FormManager>().As<IFormService>().SingleInstance();
            builder.RegisterType<EfFormDal>().As<IFormDal>().SingleInstance();

            builder.RegisterType<FormActivitiyManager>().As<IFormActivitiyService>().SingleInstance();
            builder.RegisterType<EfFormActivitiyDal>().As<IFormActivitiyDal>().SingleInstance();

            builder.RegisterType<FormActivityCategoryManager>().As<IFormActivityCategoryService>().SingleInstance();
            builder.RegisterType<EfFormActivityCategoryDal>().As<IFormActivityCategoryDal>().SingleInstance();
            
            var assembly = System.Reflection.Assembly.GetExecutingAssembly();

            builder.RegisterAssemblyTypes(assembly).AsImplementedInterfaces().EnableInterfaceInterceptors(new Castle.DynamicProxy.ProxyGenerationOptions()
            {
                Selector = new AspectInterceptorSelector()
            }).SingleInstance();
        }
    }
}
