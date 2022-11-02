using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using DustInTheWind.ArchitecturePills.Domain;
using DustInTheWind.ArchitecturePills.Ports.DataAccess;
using MediatR;

namespace DustInTheWind.ArchitecturePills.Application.Initialize
{
    internal class InitializeUseCase : IRequestHandler<InitializeRequest, InitializeResponse>
    {
        private readonly IInflationRepository inflationRepository;

        public InitializeUseCase(IInflationRepository inflationRepository)
        {
            this.inflationRepository = inflationRepository ?? throw new ArgumentNullException(nameof(inflationRepository));
        }

        public Task<InitializeResponse> Handle(InitializeRequest request, CancellationToken cancellationToken)
        {
            List<Inflation> inflations = inflationRepository.GetAll();

            List<string> listValues = inflations
                .Select(x => x.Time)
                .ToList();

            InitializeResponse response = new()
            {
                StartTimes = listValues,
                SelectedStartTime = listValues.LastOrDefault(),
                EndTimes = listValues,
                SelectedEndTime = listValues.LastOrDefault(),
                InputValue = 100
            };

            return Task.FromResult(response);
        }
    }
}