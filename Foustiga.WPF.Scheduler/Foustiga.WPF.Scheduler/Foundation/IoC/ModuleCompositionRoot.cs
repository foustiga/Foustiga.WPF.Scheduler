using Autofac;

namespace Foustiga.WPF.Scheduler.Foundation.IoC
{
    /// Keep track of Module-owned Autofac Container.
    /// </summary>
    public static class ModuleCompositionRoot 
    {
        private static IContainer _container;


        public static ILifetimeScope BeginLifetimeScope()
        {
            /* Typical usage:
             Foundation.IoC.ModuleCompositionRoot.BeginLifetimeScope().Resolve<Ixxx>();
            */

            if (_container is null) { SetIoCRegistration(); }
            return _container.BeginLifetimeScope();
        }

        private static void SetIoCRegistration()
        {
            ContainerBuilder containerBuilder = new ContainerBuilder(); //Use of own IOC to make the module as independant as possible.

            //Inner modules Autofac-IoC registration
            containerBuilder.RegisterModule(new Configuration.AutofacModule());

            //Autofac inner projects registration finished. Build the container and keep it.
            IContainer tmpcontainer = containerBuilder.Build();
            SetContainer(tmpcontainer);

        }
        private static void SetContainer(IContainer container) //To be called from Module project startup.
        {
            _container = container;
        }

    }
}