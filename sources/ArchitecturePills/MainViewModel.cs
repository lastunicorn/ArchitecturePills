using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using Newtonsoft.Json;

namespace DustInTheWind.ArchitecturePills
{
    public class MainViewModel : BaseViewModel
    {
        private readonly List<Inflation> inflations;
        private string selectedStartTime;
        private string selectedEndTime;
        private float inputValue;
        private float? outputValue;

        public List<string> StartTimes { get; }

        public List<string> EndTimes { get; }

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

        public MainViewModel()
        {
            inflations = LoadInflations();

            List<string> listValues = inflations
                .Select(x => x.Time)
                .ToList();

            StartTimes = listValues;
            SelectedStartTime = listValues.LastOrDefault();

            EndTimes = listValues;
            SelectedEndTime = listValues.LastOrDefault();

            InputValue = 100;
        }

        private static List<Inflation> LoadInflations()
        {
            Assembly assembly = Assembly.GetExecutingAssembly();
            using Stream stream = assembly.GetManifestResourceStream("DustInTheWind.ArchitecturePills.Data.inflation-yearly.json");
            using StreamReader streamReader = new(stream);
            string json = streamReader.ReadToEnd();
            return JsonConvert.DeserializeObject<List<Inflation>>(json);
        }

        private void CalculateOutputValue()
        {
            // find start index
            int startIndex = inflations.FindIndex(x => x.Time == selectedStartTime);

            // find end index
            int endIndex = inflations.FindIndex(x => x.Time == selectedEndTime);

            if (startIndex == -1 || endIndex == -1)
            {
                OutputValue = null;
            }
            else
            {
                // iterate from start index to end index and adjust the calculated value based on the inflation rate of each step.

                float calculatedValue = inputValue;

                if (startIndex < endIndex)
                {
                    for (int i = startIndex + 1; i <= endIndex; i++)
                    {
                        float inflationRate = inflations[i].InflationRate;
                        float inflationValue = 1 + inflationRate / 100;
                        calculatedValue *= inflationValue;
                    }
                }
                else if (startIndex > endIndex)
                {
                    for (int i = startIndex; i > endIndex; i--)
                    {
                        float inflationRate = inflations[i].InflationRate;
                        float inflationValue = 1 + inflationRate / 100;
                        calculatedValue /= inflationValue;
                    }
                }

                OutputValue = calculatedValue;
            }
        }
    }
}