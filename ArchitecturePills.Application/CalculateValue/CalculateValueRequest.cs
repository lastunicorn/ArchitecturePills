using MediatR;

namespace DustInTheWind.ArchitecturePills.Application.CalculateValue
{
    public class CalculateValueRequest : IRequest<CalculateValueResponse>
    {
        public float InputValue { get; set; }

        public string StartTime { get; set; }

        public string EndTime { get; set; }
    }
}