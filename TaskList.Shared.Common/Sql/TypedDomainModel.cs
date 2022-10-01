namespace TaskList.Shared.Common.Sql;

public abstract class TypedDomainModel<T>
{
    public T Id { get;  set; }
}