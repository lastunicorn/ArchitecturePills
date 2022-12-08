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