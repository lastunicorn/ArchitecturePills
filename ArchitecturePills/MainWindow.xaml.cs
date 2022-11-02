using System.Windows;
using DustInTheWind.ArchitecturePills.DataAccess;

namespace DustInTheWind.ArchitecturePills
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            InflationRepository inflationRepository = new();
            DataContext = new MainViewModel(inflationRepository);
        }
    }
}