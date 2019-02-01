using Autofac;
using Autofac.Extras.CommonServiceLocator;
using CommonServiceLocator;
using XFNutsAndBolts.Settings;
using XFNutsAndBolts.ViewModels;

namespace XFNutsAndBolts
{
    public sealed class Bootstrapper
    {
        public static void Initialize(AppSettingsTest settings)
        {
            ContainerBuilder builder = new ContainerBuilder();
            builder.RegisterType<MainPageViewModel>().AsSelf();
            builder.RegisterInstance(settings).AsImplementedInterfaces();

            IContainer container = builder.Build();

            AutofacServiceLocator asl = new AutofacServiceLocator(container);
            ServiceLocator.SetLocatorProvider(() => asl);
        }
    }
}
