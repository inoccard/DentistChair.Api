namespace Sds.DentistChair.Domain.SeedWorks;

public abstract class EntityId<T>
{
    public virtual T Id { get; set; }

    public abstract bool IsUnassigned();

    protected EntityId()
    {
    }

    public override bool Equals(object obj)
    {
        var compareTo = obj as EntityId<T>;

        if (ReferenceEquals(this, compareTo)) return true;
        if (ReferenceEquals(null, compareTo)) return false;

        return Id.Equals(compareTo.Id);
    }

    public override int GetHashCode()
    {
        return (GetType().GetHashCode() * 907) + Id.GetHashCode();
    }

    public override string ToString()
    {
        return $"{GetType().Name} [Id={Id}]";
    }
}

