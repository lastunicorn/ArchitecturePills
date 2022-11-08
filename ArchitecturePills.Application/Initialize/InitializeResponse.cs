using System.Collections.Generic;

namespace DustInTheWind.ArchitecturePills.Application.Initialize
{
    public struct InitializeResponse
    {
        public List<string> StartTimes { get; set; }

        public string SelectedStartTime { get; set; }

        public List<string> EndTimes { get; set; }

        public string SelectedEndTime { get; set; }

        public float InputValue { get; set; }
    }
}