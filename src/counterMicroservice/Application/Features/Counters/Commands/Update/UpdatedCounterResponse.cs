using NArchitecture.Core.Application.Responses;

namespace Application.Features.Counters.Commands.Update;

public class UpdatedCounterResponse : IResponse
{
    public Guid Id { get; set; }
    public string SerialNumber { get; set; }
    public DateTime MeasurementTime { get; set; }
    public string LatestIndexInfo { get; set; }
    public double VoltageDuringMeasurement { get; set; }
    public double CurrentDuringMeasurement { get; set; }
}