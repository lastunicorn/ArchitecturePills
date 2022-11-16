using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using DustInTheWind.ArchitecturePills.Domain;
using Newtonsoft.Json;

namespace DustInTheWind.ArchitecturePills.Application.Initialize
{
    public class InitializeUseCase
    {
        public InitializeResponse Execute()
        {
            List<Inflation> inflations = LoadInflations();

            List<string> listValues = inflations
                .Select(x => x.Key)
                .ToList();

            return new InitializeResponse
            {
                StartTimes = listValues,
                SelectedStartTime = listValues.LastOrDefault(),
                EndTimes = listValues,
                SelectedEndTime = listValues.LastOrDefault(),
                InputValue = 100
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