using System.Windows;
using Autofac;
using DustInTheWind.ArchitecturePills.Presentation;

namespace DustInTheWind.ArchitecturePills.Bootstrapper
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : System.Windows.Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            IContainer container = CreateContainer();

            MainWindow = container.Resolve<MainWindow>();
            MainWindow.Show();

            base.OnStartup(e);
        }

        private static IContainer CreateContainer()
        {
            ContainerBuilder containerBuilder = new();
            SetupServices.Configure(containerBuilder);
            return containerBuilder.Build();
        }
    }
}