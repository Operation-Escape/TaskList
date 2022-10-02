namespace TaskList.Domain.Models.Abstract
{
    public interface IModel<T>
    {
        public T Id { get; set; }
    }
}
