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

using System;
using System.Collections.Generic;
using DustInTheWind.ArchitecturePills.Application;
using DustInTheWind.ArchitecturePills.Application.CalculateValue;
using DustInTheWind.ArchitecturePills.Application.Initialize;
using DustInTheWind.ArchitecturePills.DataAccess;

namespace DustInTheWind.ArchitecturePills;

public class MainViewModel : BaseViewModel
{
    private readonly IInflationRepository inflationRepository;
    private string? selectedStartKey;
    private string? selectedEndKey;
    private float inputValue;
    private float? outputValue;

    public List<string>? StartKeys { get; set; }

    public List<string>? EndKeys { get; set; }

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

    public MainViewModel(IInflationRepository inflationRepository)
    {
        this.inflationRepository = inflationRepository ?? throw new ArgumentNullException(nameof(inflationRepository));

        Initialize();
    }

    private void Initialize()
    {
        InitializeUseCase useCase = new(inflationRepository);
        InitializeResponse response = useCase.Execute();

        StartKeys = response.StartKeys;
        SelectedStartKey = response.SelectedStartKey;
        EndKeys = response.EndKeys;
        SelectedEndKey = response.SelectedEndKey;
        InputValue = response.InputValue;
    }

    private void CalculateOutputValue()
    {
        CalculateValueRequest request = new()
        {
            InputValue = inputValue,
            StartKey = selectedStartKey,
            EndKey = selectedEndKey
        };
            
        CalculateValueUseCase useCase = new(inflationRepository);
        CalculateValueResponse response = useCase.Execute(request);

        OutputValue = response.OutputValue;
    }
}