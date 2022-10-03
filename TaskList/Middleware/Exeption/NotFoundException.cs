namespace TaskList.Api.Middleware.Exeption
{
    public sealed class NotFoundException : Exception
    {
        public NotFoundException(string name, int id): base($"Controller {name} with id {id} was not found.")
        { }

        public NotFoundException(string name) : base($"Controller {name} was not found.") { }
    }
}
