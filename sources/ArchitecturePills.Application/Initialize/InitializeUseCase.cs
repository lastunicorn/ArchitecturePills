using System;
using System.Collections.Generic;
using System.Linq;
using DustInTheWind.ArchitecturePills.DataAccess;
using DustInTheWind.ArchitecturePills.Domain;

namespace DustInTheWind.ArchitecturePills.Application.Initialize
{
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
                .Select(x => x.Time)
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
    }
}