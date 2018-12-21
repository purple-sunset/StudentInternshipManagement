using Repositories.Implements;
using Repositories.Interfaces;
using System;
using Models.Contexts;
using Services.Implements;
using Services.Interfaces;
using Unity;
using Unity.Lifetime;
using Microsoft.AspNet.Identity;
using Unity.Injection;
using System.Web;
using Models.Entities;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using Hangfire.Dashboard;
using Hangfire;
using Microsoft.Owin.Security.Cookies;
using Microsoft.Owin;

namespace StudentInternshipManagement
{
    /// <summary>
    /// Specifies the Unity configuration for the main container.
    /// </summary>
    public static class UnityConfig
    {
        #region Unity Container
        private static Lazy<IUnityContainer> container =
          new Lazy<IUnityContainer>(() =>
          {
              var container = new UnityContainer();
              RegisterTypes(container);
              return container;
          });

        private static readonly ContainerControlledLifetimeManager manager = new ContainerControlledLifetimeManager(); 

        /// <summary>
        /// Configured Unity Container.
        /// </summary>
        public static IUnityContainer Container => container.Value;
        #endregion

        /// <summary>
        /// Registers the type mappings with the Unity container.
        /// </summary>
        /// <param name="container">The unity container to configure.</param>
        /// <remarks>
        /// There is no need to register concrete types such as controllers or
        /// API controllers (unless you want to change the defaults), as Unity
        /// allows resolving a concrete type even if it was not previously
        /// registered.
        /// </remarks>
        public static void RegisterTypes(IUnityContainer container)
        {
            // NOTE: To load from web.config uncomment the line below.
            // Make sure to add a Unity.Configuration to the using statements.
            // container.LoadConfiguration();

            container.RegisterType<WebContext, WebContext>(manager);
            container.RegisterType<ApplicationSignInManager>(manager);
            container.RegisterType<ApplicationUserManager>(manager);
            container.RegisterType<IAuthenticationManager>(
                new InjectionFactory(c => HttpContext.Current.GetOwinContext().Authentication));
            container.RegisterType<IUserStore<ApplicationUser>, UserStore<ApplicationUser>>(
                new InjectionConstructor(typeof(ApplicationDbContext)));
            container.RegisterType(typeof(IdentityFactoryOptions<>),
                new InjectionFactory(c => Startup.DataProtectionProvider));

            container.RegisterInstance<CookieAuthenticationOptions>(new CookieAuthenticationOptions
            {
                AuthenticationType = DefaultAuthenticationTypes.ApplicationCookie,
                LoginPath = new PathString("/Account/Login"),
                Provider = new CookieAuthenticationProvider
                {
                    // Enables the application to validate the security stamp when the user logs in.
                    // This is a security feature which is used when you change a password or add an external login to your account.  
                    OnValidateIdentity = SecurityStampValidator.OnValidateIdentity<ApplicationUserManager, ApplicationUser>(
                        validateInterval: TimeSpan.FromMinutes(30),
                        regenerateIdentity: (manager, user) => user.GenerateUserIdentityAsync(manager))
                }
            }, manager);

            container.RegisterType<IDashboardAuthorizationFilter, HangfireAuthorizationFilter>();
            container.RegisterInstance<DashboardOptions>(new DashboardOptions()
            {
                Authorization = new[] {container.Resolve<IDashboardAuthorizationFilter>()}
            }, manager);

            container.RegisterType<IUserRepository, UserRepository>(manager);
            container.RegisterType(typeof(IGenericRepository<>), typeof(GenericRepository<>), manager);
            container.RegisterType<IUnitOfWork, UnitOfWork>(manager);
            container.RegisterType(typeof(IGenericService<>), typeof(GenericService<>), manager);
            container.RegisterType<IUserService, UserService>(manager);
            container.RegisterType<IAdminService, AdminService>(manager);
            container.RegisterType<ICompanyTrainingMajorService, CompanyTrainingMajorService>(manager);
            container.RegisterType<ICompanyService, CompanyService>(manager);
            container.RegisterType<ITrainingMajorService, TrainingMajorService>(manager);
            container.RegisterType<IDepartmentService, DepartmentService>(manager);
            container.RegisterType<IEmailHistoryService, EmailHistoryService>(manager);
            container.RegisterType<IEmailTemplateService, EmailTemplateService>(manager);
            container.RegisterType<IGroupService, GroupService>(manager);
            container.RegisterType<ISemesterService, SemesterService>(manager);
            container.RegisterType<ILearningClassService, LearningClassService>(manager);
            container.RegisterType<ILearningClassStudentService, LearningClassStudentService>(manager);
            container.RegisterType<IMessageService, MessageService>(manager);
            container.RegisterType<INewsService, NewsService>(manager);
            container.RegisterType<INotificationService, NotificationService>(manager);
            container.RegisterType<IStatisticService, StatisticService>(manager);
            container.RegisterType<IStudentClassService, StudentClassService>(manager);
            container.RegisterType<IStudentService, StudentService>(manager);
            container.RegisterType<ISubjectService, SubjectService>(manager);
            container.RegisterType<ITeacherService, TeacherService>(manager);
            container.RegisterType<IInternshipService, InternshipService>(manager);
            container.RegisterType<IEmailService, EmailService>(manager);
        }
    }
}