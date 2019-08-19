using StudentDormitoryManagementSystem.Contracts.Identity;

[assembly: WebActivatorEx.PreApplicationStartMethod(typeof(StudentDormitoryManagementSystem.App_Start.NinjectWebCommon), "Start")]
[assembly: WebActivatorEx.ApplicationShutdownMethodAttribute(typeof(StudentDormitoryManagementSystem.App_Start.NinjectWebCommon), "Stop")]

namespace StudentDormitoryManagementSystem.App_Start
{
    using System;
    using System.Data.Entity;
    using System.Web;
    using AutoMapper;
    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.EntityFramework;
    using Microsoft.Owin.Security;
    using Microsoft.Web.Infrastructure.DynamicModuleHelper;

    using Ninject;
    using Ninject.Web.Common;
    using Ninject.Web.Common.WebHost;
    using StudentDormitoryManagementSystem.Data;
    using StudentDormitoryManagementSystem.Data.Model.Models;
    using StudentDormitoryManagementSystem.Data.Repositories;
    using StudentDormitoryManagementSystem.Data.SaveContext;
    using StudentDormitoryManagementSystem.Identity;
    using StudentDormitoryManagementSystem.Services;
    using StudentDormitoryManagementSystem.Services.Abstracts;
    using StudentDormitoryManagementSystem.Services.Contracts;

    public static class NinjectWebCommon 
    {
        private static readonly Bootstrapper bootstrapper = new Bootstrapper();

        /// <summary>
        /// Starts the application
        /// </summary>
        public static void Start() 
        {
            DynamicModuleUtility.RegisterModule(typeof(OnePerRequestHttpModule));
            DynamicModuleUtility.RegisterModule(typeof(NinjectHttpModule));
            bootstrapper.Initialize(CreateKernel);
        }
        
        /// <summary>
        /// Stops the application.
        /// </summary>
        public static void Stop()
        {
            bootstrapper.ShutDown();
        }
        
        /// <summary>
        /// Creates the kernel that will manage your application.
        /// </summary>
        /// <returns>The created kernel.</returns>
        private static IKernel CreateKernel()
        {
            var kernel = new StandardKernel();
            try
            {
                kernel.Bind<Func<IKernel>>().ToMethod(ctx => () => new Bootstrapper().Kernel);
                kernel.Bind<IHttpModule>().To<HttpApplicationInitializationHttpModule>();
                RegisterServices(kernel);
                return kernel;
            }
            catch
            {
                kernel.Dispose();
                throw;
            }
        }

        /// <summary>
        /// Load your modules or register your services here!
        /// </summary>
        /// <param name="kernel">The kernel.</param>
        private static void RegisterServices(IKernel kernel)
        {
            //kernel.Bind(x =>
            //{
            //    x.FromThisAssembly()
            //     .SelectAllClasses()
            //     .BindDefaultInterface();
            //});

            kernel.Bind(typeof(IEfRepository<>)).To(typeof(EfRepository<>));
            kernel.Bind(typeof(DbContext), typeof(MsSqlDbContext)).To<MsSqlDbContext>().InSingletonScope();
            kernel.Bind<ISaveContext>().To<SaveContext>();
            kernel.Bind<IMapper>().ToMethod(x => Mapper.Instance);

            // Identity
            kernel.Bind<IUserStore<User>>().To<UserStore<User>>().InRequestScope();
            kernel.Bind<IAuthenticationManager>().ToMethod(c => HttpContext.Current.GetOwinContext().Authentication).InRequestScope();
            kernel.Bind(typeof(IApplicationUserManager)).To(typeof(ApplicationUserManager)).InRequestScope();
            kernel.Bind(typeof(IApplicationSignInManager)).To(typeof(ApplicationSignInManager)).InRequestScope();
            kernel.Bind(typeof(IService<>)).To(typeof(Service<>));
            kernel.Bind<IItemsService>().To<ItemsService>().InRequestScope();
            kernel.Bind<IRoomsService>().To<RoomsService>().InRequestScope();
            kernel.Bind<IStudentsService>().To<StudentsService>().InRequestScope();
            kernel.Bind<IItemCategoriesService>().To<ItemCategoriesService>().InRequestScope();
            kernel.Bind<IInventoriesService>().To<InventoriesService>().InRequestScope();
        }
    }
}