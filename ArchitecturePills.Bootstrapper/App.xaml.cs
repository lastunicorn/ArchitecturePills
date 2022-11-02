using System.Windows;
using DustInTheWind.ArchitecturePills.DataAccess;
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
            InflationRepository inflationRepository = new();
            MainViewModel viewModel = new(inflationRepository);

            MainWindow mainWindow = new()
            {
                DataContext = viewModel
            };

            MainWindow = mainWindow;
            MainWindow.Show();

            base.OnStartup(e);
        }
    }
}