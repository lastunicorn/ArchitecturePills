// Architecture Pills
// Copyright (C) 2022 Dust in the Wind
// 
// This program is free software: you can redistribute it and/or modify
// it under the terms of the GNU General Public License as published by
// the Free Software Foundation, either version 3 of the License, or
// (at your option) any later version.
// 
// This program is distributed in the hope that it will be useful,
// but WITHOUT ANY WARRANTY; without even the implied warranty of
// MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
// GNU General Public License for more details.
// 
// You should have received a copy of the GNU General Public License
// along with this program.  If not, see <http://www.gnu.org/licenses/>.

using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using DustInTheWind.ArchitecturePills.Domain;
using Newtonsoft.Json;

namespace DustInTheWind.ArchitecturePills.Application.Initialize;

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
        using Stream stream = assembly.GetManifestResourceStream("DustInTheWind.ArchitecturePills.Application.Data.inflation-yearly.json")!;
        using StreamReader streamReader = new(stream);
        string json = streamReader.ReadToEnd();
        return JsonConvert.DeserializeObject<List<Inflation>>(json) ?? new List<Inflation>();
    }
}