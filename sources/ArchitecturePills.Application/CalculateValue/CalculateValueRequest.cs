namespace DustInTheWind.ArchitecturePills.Application.CalculateValue
{
    public struct CalculateValueRequest
    {
        public float InputValue { get; set; }

        public string StartTime { get; set; }

        public string EndTime { get; set; }
    }
}