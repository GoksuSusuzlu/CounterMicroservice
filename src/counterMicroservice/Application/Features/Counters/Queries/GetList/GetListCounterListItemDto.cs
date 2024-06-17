using NArchitecture.Core.Application.Dtos;

namespace Application.Features.Counters.Queries.GetList;

public class GetListCounterListItemDto : IDto
{
    public Guid Id { get; set; }
    public string SerialNumber { get; set; }
    public DateTime MeasurementTime { get; set; }
    public string LatestIndexInfo { get; set; }
    public double VoltageDuringMeasurement { get; set; }
    public double CurrentDuringMeasurement { get; set; }
}