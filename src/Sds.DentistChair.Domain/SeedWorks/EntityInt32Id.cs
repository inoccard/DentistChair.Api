namespace Sds.DentistChair.Domain.SeedWorks;

public abstract class EntityInt64Id : EntityId<long>
{
    public override bool IsUnassigned() => Id == 0;
}

