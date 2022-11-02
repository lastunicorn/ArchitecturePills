using Autofac;
using DustInTheWind.ArchitecturePills.DataAccess;
using DustInTheWind.ArchitecturePills.Ports.DataAccess;
using DustInTheWind.ArchitecturePills.Presentation;

namespace DustInTheWind.ArchitecturePills.Bootstrapper
{
    internal static class SetupServices
    {
        public static void Configure(ContainerBuilder containerBuilder)
        {
            containerBuilder.RegisterType<InflationRepository>().As<IInflationRepository>();

            containerBuilder.RegisterType<MainWindow>().AsSelf();
            containerBuilder.RegisterType<MainViewModel>().AsSelf();
        }
    }
}