using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using DustInTheWind.ArchitecturePills.Domain;
using DustInTheWind.ArchitecturePills.Ports.DataAccess;
using MediatR;

namespace DustInTheWind.ArchitecturePills.Application.CalculateValue
{
    internal class CalculateValueUseCase : IRequestHandler<CalculateValueRequest, CalculateValueResponse>
    {
        private readonly IInflationRepository inflationRepository;

        public CalculateValueUseCase(IInflationRepository inflationRepository)
        {
            this.inflationRepository = inflationRepository ?? throw new ArgumentNullException(nameof(inflationRepository));
        }

        public Task<CalculateValueResponse> Handle(CalculateValueRequest request, CancellationToken cancellationToken)
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

            CalculateValueResponse response = new()
            {
                OutputValue = outputValue
            };

            return Task.FromResult(response);
        }
    }
}