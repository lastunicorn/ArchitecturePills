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
using System.Threading.Tasks;
using DustInTheWind.ArchitecturePills.Application.CalculateValue;
using DustInTheWind.ArchitecturePills.Application.Initialize;
using MediatR;

namespace DustInTheWind.ArchitecturePills.Presentation;

public class MainViewModel : BaseViewModel
{
    private readonly IMediator mediator;
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
            _ = CalculateOutputValue();
        }
    }

    public string? SelectedEndKey
    {
        get => selectedEndKey;
        set
        {
            selectedEndKey = value;
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

        StartKeys = response.StartKeys;
        SelectedStartKey = response.SelectedStartKey;
        EndKeys = response.EndKeys;
        SelectedEndKey = response.SelectedEndKey;
        InputValue = response.InputValue;
    }

    private async Task CalculateOutputValue()
    {
        CalculateValueRequest request = new()
        {
            InputValue = inputValue,
            StartKey = selectedStartKey,
            EndKey = selectedEndKey
        };

        CalculateValueResponse response = await mediator.Send(request);

        OutputValue = response.OutputValue;
    }
}