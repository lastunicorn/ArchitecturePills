using System.Collections.Generic;
using DustInTheWind.ArchitecturePills.Application.CalculateValue;
using DustInTheWind.ArchitecturePills.Application.Initialize;

namespace DustInTheWind.ArchitecturePills
{
    public class MainViewModel : BaseViewModel
    {
        private string selectedStartKey;
        private string selectedEndKey;
        private float inputValue;
        private float? outputValue;

        public List<string> StartKeys { get; private set; }

        public List<string> EndKeys { get; private set; }

        public string SelectedStartKey
        {
            get => selectedStartKey;
            set
            {
                selectedStartKey = value;
                CalculateOutputValue();
            }
        }

        public string SelectedEndKey
        {
            get => selectedEndKey;
            set
            {
                selectedEndKey = value;
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

        public MainViewModel()
        {
            Initialize();
        }

        private void Initialize()
        {
            InitializeUseCase useCase = new();
            InitializeResponse response = useCase.Execute();

            StartKeys = response.StartTimes;
            SelectedStartKey = response.SelectedStartTime;
            EndKeys = response.EndTimes;
            SelectedEndKey = response.SelectedEndTime;
            InputValue = response.InputValue;
        }

        private void CalculateOutputValue()
        {
            CalculateValueRequest request = new()
            {
                InputValue = inputValue,
                StartTime = selectedStartKey,
                EndTime = selectedEndKey
            };

            CalculateValueUseCase useCase = new();
            CalculateValueResponse response = useCase.Execute(request);

            OutputValue = response.OutputValue;
        }
    }
}