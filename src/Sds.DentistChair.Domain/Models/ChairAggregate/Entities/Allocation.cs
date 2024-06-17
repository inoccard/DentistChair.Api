using Sds.DentistChair.Domain.SeedWorks;

namespace Sds.DentistChair.Domain.Models.ChairAggregate.Entities;

public class Allocation : EntityInt64Id
{
    public long ChairId { get; set; }
    public Chair Chair { get; set; }
    public DateTime StartTime { get; set; }
    public DateTime EndTime { get; set; }
}
