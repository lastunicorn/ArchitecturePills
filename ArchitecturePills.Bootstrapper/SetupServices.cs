using System.Reflection;
using Autofac;
using DustInTheWind.ArchitecturePills.Application.Initialize;
using DustInTheWind.ArchitecturePills.DataAccess;
using DustInTheWind.ArchitecturePills.Ports.DataAccess;
using DustInTheWind.ArchitecturePills.Presentation;
using MediatR.Extensions.Autofac.DependencyInjection;

namespace DustInTheWind.ArchitecturePills.Bootstrapper
{
    internal static class SetupServices
    {
        public static void Configure(ContainerBuilder containerBuilder)
        {
            Assembly applicationAssembly = typeof(InitializeRequest).Assembly;
            containerBuilder.RegisterMediatR(applicationAssembly);

            containerBuilder.RegisterType<InflationRepository>().As<IInflationRepository>();

            containerBuilder.RegisterType<MainWindow>().AsSelf();
            containerBuilder.RegisterType<MainViewModel>().AsSelf();
        }
    }
}