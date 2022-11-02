using System;
using System.Windows;
using DustInTheWind.ArchitecturePills.Application.CalculateValue;
using DustInTheWind.ArchitecturePills.Application.Initialize;
using DustInTheWind.ArchitecturePills.DataAccess;
using MediatR;

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

            ServiceFactory serviceFactory = ServiceFactory;
            Mediator mediator = new(serviceFactory);

            DataContext = new MainViewModel(mediator);
        }

        private object ServiceFactory(Type servicetype)
        {
            if (servicetype == typeof(IRequestHandler<CalculateValueRequest, CalculateValueResponse>))
            {
                InflationRepository inflationRepository = new();
                return new CalculateValueUseCase(inflationRepository);
            }

            if (servicetype == typeof(IRequestHandler<InitializeRequest, InitializeResponse>))
            {
                InflationRepository inflationRepository = new();
                return new InitializeUseCase(inflationRepository);
            }

            return null;
        }
    }
}