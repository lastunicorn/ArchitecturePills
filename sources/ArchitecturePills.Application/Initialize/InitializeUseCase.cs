﻿// Architecture Pills
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
using System.Linq;
using DustInTheWind.ArchitecturePills.Domain;
using DustInTheWind.ArchitecturePills.Ports.DataAccess;

namespace DustInTheWind.ArchitecturePills.Application.Initialize;

public class InitializeUseCase
{
    private readonly IInflationRepository inflationRepository;

    public InitializeUseCase(IInflationRepository inflationRepository)
    {
        this.inflationRepository = inflationRepository ?? throw new ArgumentNullException(nameof(inflationRepository));
    }

    public InitializeResponse Execute()
    {
        List<Inflation> inflations = inflationRepository.GetAll();

        List<string> listValues = inflations
            .Select(x => x.Key)
            .ToList();

        return new InitializeResponse
        {
            StartKeys = listValues,
            SelectedStartKey = listValues.LastOrDefault(),
            EndKeys = listValues,
            SelectedEndKey = listValues.LastOrDefault(),
            InputValue = 100
        };
    }
}