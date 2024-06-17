using NArchitecture.Core.Persistence.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class Counter: Entity<Guid>
    {
        public string SerialNumber { get; set; }
        public DateTime MeasurementTime { get; set; }
        public string LatestIndexInfo { get; set; }
        public double VoltageDuringMeasurement { get; set; }
        public double CurrentDuringMeasurement { get; set; }
    }
}
