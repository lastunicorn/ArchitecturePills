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

namespace DustInTheWind.ArchitecturePills.Domain
{
    public class Calculator
    {
        public List<Inflation> Inflations { get; set; }

        public float InputValue { get; set; }

        public float? OutputValue { get; set; }

        public string? StartKey { get; set; }

        public string? EndKey { get; set; }

        public void Calculate()
        {
            int startIndex = Inflations.FindIndex(x => x.Key == StartKey);

            int endIndex = Inflations.FindIndex(x => x.Key == EndKey);

            if (startIndex == -1 || endIndex == -1)
            {
                OutputValue = null;
            }
            else
            {
                float calculatedValue = InputValue;

                if (startIndex < endIndex)
                {
                    for (int i = startIndex + 1; i <= endIndex; i++)
                    {
                        float inflationRate = Inflations[i].InflationRate;
                        float inflationValue = 1 + inflationRate / 100;
                        calculatedValue *= inflationValue;
                    }
                }
                else if (startIndex > endIndex)
                {
                    for (int i = startIndex; i > endIndex; i--)
                    {
                        float inflationRate = Inflations[i].InflationRate;
                        float inflationValue = 1 + inflationRate / 100;
                        calculatedValue /= inflationValue;
                    }
                }

                OutputValue = calculatedValue;
            }
        }
    }
}