using System;
using System.Collections.Generic;
using DustInTheWind.ArchitecturePills.Domain;
using DustInTheWind.ArchitecturePills.Ports.DataAccess;

namespace DustInTheWind.ArchitecturePills.Application.CalculateValue
{
    public class CalculateValueUseCase
    {
        private readonly IInflationRepository inflationRepository;

        public CalculateValueUseCase(IInflationRepository inflationRepository)
        {
            this.inflationRepository = inflationRepository ?? throw new ArgumentNullException(nameof(inflationRepository));
        }

        public CalculateValueResponse Execute(CalculateValueRequest request)
        {
            List<Inflation> inflations = inflationRepository.GetAll();

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
    }
}