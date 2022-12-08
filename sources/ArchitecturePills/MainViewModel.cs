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
using Newtonsoft.Json;

namespace DustInTheWind.ArchitecturePills;

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
        // find start index
        int startIndex = inflations.FindIndex(x => x.Key == selectedStartKey);

        // find end index
        int endIndex = inflations.FindIndex(x => x.Key == selectedEndKey);

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