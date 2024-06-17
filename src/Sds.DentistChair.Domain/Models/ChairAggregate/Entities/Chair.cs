using Sds.DentistChair.Domain.SeedWorks;

namespace Sds.DentistChair.Domain.Models.ChairAggregate.Entities;

public class Chair : EntityInt64Id
{
    public string Number { get; set; }
    public string Description { get; set; }
    public string AdditionalInfo { get; set; }
}
