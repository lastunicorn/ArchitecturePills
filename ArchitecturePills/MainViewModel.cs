using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using DustInTheWind.ArchitecturePills.Application.CalculateValue;
using DustInTheWind.ArchitecturePills.Application.Initialize;
using MediatR;

namespace DustInTheWind.ArchitecturePills
{
    public class MainViewModel : BaseViewModel
    {
        private readonly IMediator mediator;
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
                _ = CalculateOutputValue();
            }
        }

        public string SelectedEndTime
        {
            get => selectedEndTime;
            set
            {
                selectedEndTime = value;
                _ = CalculateOutputValue();
            }
        }

        public float InputValue
        {
            get => inputValue;
            set
            {
                inputValue = value;
                _ = CalculateOutputValue();
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

        public MainViewModel(IMediator mediator)
        {
            this.mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));

            _ = Initialize();
        }

        private async Task Initialize()
        {
            InitializeRequest request = new();
            InitializeResponse response = await mediator.Send(request);

            StartTimes = response.StartTimes;
            SelectedStartTime = response.SelectedStartTime;
            EndTimes = response.EndTimes;
            SelectedEndTime = response.SelectedEndTime;
            InputValue = response.InputValue;
        }

        private async Task CalculateOutputValue()
        {
            CalculateValueRequest request = new()
            {
                InputValue = inputValue,
                StartTime = selectedStartTime,
                EndTime = selectedEndTime
            };

            CalculateValueResponse response = await mediator.Send(request);

            OutputValue = response.OutputValue;
        }
    }
}