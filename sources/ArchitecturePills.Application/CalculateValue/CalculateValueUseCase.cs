using System.Collections.Generic;
using System.IO;
using System.Reflection;
using DustInTheWind.ArchitecturePills.Domain;
using Newtonsoft.Json;

namespace DustInTheWind.ArchitecturePills.Application.CalculateValue
{
    public class CalculateValueUseCase
    {
        public CalculateValueResponse Execute(CalculateValueRequest request)
        {
            List<Inflation> inflations = LoadInflations();

            Calculator calculator = new()
            {
                Inflations = inflations,
                InputValue = request.InputValue,
                StartTime = request.StartTime,
                EndTime = request.EndTime
            };

            calculator.Calculate();
            float? outputValue = calculator.OutputValue;

            return new CalculateValueResponse()
            {
                OutputValue = outputValue
            };
        }

        private static List<Inflation> LoadInflations()
        {
            Assembly assembly = Assembly.GetExecutingAssembly();
            using Stream stream = assembly.GetManifestResourceStream("DustInTheWind.ArchitecturePills.Application.Data.inflation-yearly.json");
            using StreamReader streamReader = new(stream);
            string json = streamReader.ReadToEnd();
            return JsonConvert.DeserializeObject<List<Inflation>>(json);
        }
    }
}