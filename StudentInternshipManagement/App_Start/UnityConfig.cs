using Repositories.Implements;
using Repositories.Interfaces;
using System;
using Models;
using Models.Contexts;
using Services.Implements;
using Services.Interfaces;
using Unity;
using Unity.Lifetime;

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

            // TODO: Register your type's mappings here.
            // container.RegisterType<IProductRepository, ProductRepository>();

            container.RegisterType<WebContext, WebContext>(manager);
            container.RegisterType<IUserRepository, UserRepository>(manager);
            container.RegisterType(typeof(IGenericRepository<>), typeof(GenericRepository<>), manager);
            container.RegisterType<IUnitOfWork, UnitOfWork>(manager);
            container.RegisterType(typeof(IGenericService<>), typeof(GenericService<>), manager);
            container.RegisterType<IAdminService, AdminService>(manager);
            container.RegisterType<ICompanyService, CompanyService>(manager);
        }
    }
}