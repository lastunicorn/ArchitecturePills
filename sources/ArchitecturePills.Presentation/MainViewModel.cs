using System;
using System.Collections.Generic;
using DustInTheWind.ArchitecturePills.Application.CalculateValue;
using DustInTheWind.ArchitecturePills.Application.Initialize;
using DustInTheWind.ArchitecturePills.Ports.DataAccess;

namespace DustInTheWind.ArchitecturePills.Presentation
{
    public class MainViewModel : BaseViewModel
    {
        private readonly IInflationRepository inflationRepository;
        private string selectedStartTime;
        private string selectedEndTime;
        private float inputValue;
        private float? outputValue;

        public List<string> StartTimes { get; set; }

        public List<string> EndTimes { get; set; }

        public string SelectedStartTime
        {
            get => selectedStartTime;
            set
            {
                selectedStartTime = value;
                CalculateOutputValue();
            }
        }

        public string SelectedEndTime
        {
            get => selectedEndTime;
            set
            {
                selectedEndTime = value;
                CalculateOutputValue();
            }
        }

        public float InputValue
        {
            get => inputValue;
            set
            {
                inputValue = value;
                CalculateOutputValue();
            }
        }

        public float? OutputValue
        {
            get => outputValue;
            private set
            {
                outputValue = value;
                OnPropertyChanged();
            }
        }

        public MainViewModel(IInflationRepository inflationRepository)
        {
            this.inflationRepository = inflationRepository ?? throw new ArgumentNullException(nameof(inflationRepository));

            Initialize();
        }

        private void Initialize()
        {
            InitializeUseCase useCase = new(inflationRepository);
            InitializeResponse response = useCase.Execute();

            StartTimes = response.StartTimes;
            SelectedStartTime = response.SelectedStartTime;
            EndTimes = response.EndTimes;
            SelectedEndTime = response.SelectedEndTime;
            InputValue = response.InputValue;
        }

        private void CalculateOutputValue()
        {
            CalculateValueRequest request = new()
            {
                InputValue = inputValue,
                StartTime = selectedStartTime,
                EndTime = selectedEndTime
            };
            
            CalculateValueUseCase useCase = new(inflationRepository);
            CalculateValueResponse response = useCase.Execute(request);

            OutputValue = response.OutputValue;
        }
    }
}