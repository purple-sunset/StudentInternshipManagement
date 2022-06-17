using Hangfire;
using Hangfire.Dashboard;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin;
using Microsoft.Owin.Security;
using Microsoft.Owin.Security.Cookies;
using StudentInternshipManagement.Models.Contexts;
using StudentInternshipManagement.Models.Entities;
using StudentInternshipManagement.Repositories.Implements;
using StudentInternshipManagement.Services.Implements;
using System;
using System.Data.Entity;
using System.Web;
using Unity;
using Unity.AspNet.Mvc;
using Unity.Injection;
using Unity.Lifetime;

namespace StudentInternshipManagement.Web
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
            //container.RegisterType<WebContext>(manager);
            container.RegisterType<DbContext, WebContext>(new PerRequestLifetimeManager());

            container.RegisterFactory<IAuthenticationManager>(c => HttpContext.Current?.GetOwinContext()?.Authentication, new PerRequestLifetimeManager());
            container.RegisterType<IUserStore<ApplicationUser>, UserStore<ApplicationUser>>(new PerRequestLifetimeManager());
            container.RegisterType<ApplicationSignInManager>(new PerRequestLifetimeManager());
            container.RegisterType<ApplicationUserManager>(new PerRequestLifetimeManager());

            container.RegisterInstance<CookieAuthenticationOptions>(new CookieAuthenticationOptions
            {
                CookieSameSite = Microsoft.Owin.SameSiteMode.Lax,
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
            });

            //container.RegisterType<IDashboardAuthorizationFilter, HangfireAuthorizationFilter>(new SingletonLifetimeManager());
            //container.RegisterInstance<DashboardOptions>(new DashboardOptions()
            //{
            //    Authorization = new[] { container.Resolve<IDashboardAuthorizationFilter>() }
            //}, new SingletonLifetimeManager());

            container.RegisterType<IUserRepository, UserRepository>(new PerRequestLifetimeManager());
            container.RegisterType(typeof(IGenericRepository<>), typeof(GenericRepository<>), new PerRequestLifetimeManager());
            container.RegisterType<IUnitOfWork, UnitOfWork>(new PerRequestLifetimeManager());
            container.RegisterType(typeof(IGenericService<>), typeof(GenericService<>), new PerRequestLifetimeManager());
            container.RegisterType<IUserService, UserService>(new PerRequestLifetimeManager());
            container.RegisterType<IAdminService, AdminService>(new PerRequestLifetimeManager());
            container.RegisterType<ICompanyTrainingMajorService, CompanyTrainingMajorService>(new PerRequestLifetimeManager());
            container.RegisterType<ICompanyService, CompanyService>(new PerRequestLifetimeManager());
            container.RegisterType<ITrainingMajorService, TrainingMajorService>(new PerRequestLifetimeManager());
            container.RegisterType<IDepartmentService, DepartmentService>(new PerRequestLifetimeManager());
            container.RegisterType<IEmailHistoryService, EmailHistoryService>(new PerRequestLifetimeManager());
            container.RegisterType<IEmailTemplateService, EmailTemplateService>(new PerRequestLifetimeManager());
            container.RegisterType<IGroupService, GroupService>(new PerRequestLifetimeManager());
            container.RegisterType<ISemesterService, SemesterService>(new PerRequestLifetimeManager());
            container.RegisterType<ILearningClassService, LearningClassService>(new PerRequestLifetimeManager());
            container.RegisterType<ILearningClassStudentService, LearningClassStudentService>(new PerRequestLifetimeManager());
            container.RegisterType<IMessageService, MessageService>(new PerRequestLifetimeManager());
            container.RegisterType<INewsService, NewsService>(new PerRequestLifetimeManager());
            container.RegisterType<INotificationService, NotificationService>(new PerRequestLifetimeManager());
            container.RegisterType<IStatisticService, StatisticService>(new PerRequestLifetimeManager());
            container.RegisterType<IStudentClassService, StudentClassService>(new PerRequestLifetimeManager());
            container.RegisterType<IStudentService, StudentService>(new PerRequestLifetimeManager());
            container.RegisterType<ISubjectService, SubjectService>(new PerRequestLifetimeManager());
            container.RegisterType<ITeacherService, TeacherService>(new PerRequestLifetimeManager());
            container.RegisterType<IInternshipService, InternshipService>(new PerRequestLifetimeManager());
            container.RegisterType<IEmailService, EmailService>(new PerRequestLifetimeManager());
        }
    }
}