using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using DustInTheWind.ArchitecturePills.Domain;
using Newtonsoft.Json;

namespace DustInTheWind.ArchitecturePills
{
    public class MainViewModel : BaseViewModel
    {
        private readonly List<Inflation> inflations;
        private string? selectedStartKey;
        private string? selectedEndKey;
        private float inputValue;
        private float? outputValue;

        public List<string> StartKeys { get; }

        public List<string> EndKeys { get; }

        public string? SelectedStartKey
        {
            get => selectedStartKey;
            set
            {
                selectedStartKey = value;
                CalculateOutputValue();
            }
        }

        public string? SelectedEndKey
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
            inflations = LoadInflations();

            List<string> listValues = inflations
                .Select(x => x.Key!)
                .ToList();

            StartKeys = listValues;
            SelectedStartKey = listValues.LastOrDefault();

            EndKeys = listValues;
            SelectedEndKey = listValues.LastOrDefault();

            InputValue = 100;
        }

        private static List<Inflation> LoadInflations()
        {
            Assembly assembly = Assembly.GetExecutingAssembly();
            using Stream stream = assembly.GetManifestResourceStream("DustInTheWind.ArchitecturePills.Data.inflation-yearly.json")!;
            using StreamReader streamReader = new(stream);
            string json = streamReader.ReadToEnd();
            return JsonConvert.DeserializeObject<List<Inflation>>(json) ?? new List<Inflation>();
        }

        private void CalculateOutputValue()
        {
            Calculator calculator = new()
            {
                Inflations = inflations,
                InputValue = inputValue,
                StartKey = selectedStartKey,
                EndKey = selectedEndKey
            };

            calculator.Calculate();
            OutputValue = calculator.OutputValue;
        }
    }
}