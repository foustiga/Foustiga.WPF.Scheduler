using Autofac;

namespace Foustiga.WPF.Scheduler.Configuration
{
    //ACC.UserInterface project interfaces implementation declaration. Launched by ACC.API project within its AutofacModule.
    public class AutofacModule : Autofac.Module
    {
        //Register in Autofac Container all types from this Project.
        protected override void Load(ContainerBuilder builder)
        {
            //register all classes in this namespace, implementing an interface
            builder.RegisterAssemblyTypes(this.GetType().Assembly).AsImplementedInterfaces();

            //Controller classes to be registered as Singleton.
            //builder.RegisterType<CurrencyController>().AsImplementedInterfaces().SingleInstance();
            //builder.RegisterType<CountryController>().AsImplementedInterfaces().SingleInstance();
            ////builder.RegisterType<TransactionController>().AsImplementedInterfaces().SingleInstance();

            //builder.RegisterAssemblyTypes(this.GetType().Assembly)
            //   .Where(t => t.Name.EndsWith("Controller"))
            //   .AsImplementedInterfaces().SingleInstance();

        }

    }
}
