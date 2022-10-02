namespace TaskList.Domain.Models.Abstract
{
    public abstract class SqlDomainModel<T> : IModel<T>
    {
        public T Id { get; set; }
    }
}
