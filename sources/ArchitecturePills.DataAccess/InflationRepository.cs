using System.Collections.Generic;
using System.IO;
using System.Reflection;
using DustInTheWind.ArchitecturePills.Domain;
using DustInTheWind.ArchitecturePills.Ports.DataAccess;
using Newtonsoft.Json;

namespace DustInTheWind.ArchitecturePills.DataAccess
{
    public class InflationRepository : IInflationRepository
    {
        public List<Inflation> GetAll()
        {
            Assembly assembly = Assembly.GetExecutingAssembly();
            using Stream stream = assembly.GetManifestResourceStream("DustInTheWind.ArchitecturePills.DataAccess.Data.inflation-yearly.json");
            using StreamReader streamReader = new(stream);
            string json = streamReader.ReadToEnd();
            return JsonConvert.DeserializeObject<List<Inflation>>(json);
        }
    }
}