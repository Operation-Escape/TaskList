using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using MongoDB.Driver;
using TaskList.Domain.Contexts.Abstract;
using TaskList.Domain.Models;
using Task = System.Threading.Tasks.Task;

namespace TaskList.Domain.Contexts
{
    public class MongoContext : IMongoContext
    {
        private IMongoDatabase _database { get; set; }
        public IClientSessionHandle Session { get; set; }
        public MongoClient MongoClient { get; set; }
        private readonly List<Func<Task>> _commands;

        public MongoContext(IOptions<MongoSettings> options)
        {
            _commands = new List<Func<Task>>();
            MongoClient = new MongoClient(options.Value.ConnectionString);
            _database = MongoClient.GetDatabase(options.Value.Database);
        }

        public async Task<int> SaveChangesAsync()
        {
            //ConfigureMongo();

            // using (Session = await MongoClient.StartSessionAsync())
            // {
            //     Session.StartTransaction();
            //
            //     var commandTasks = _commands.Select(c => c());
            //
            //     await Task.WhenAll(commandTasks);
            //
            //     await Session.CommitTransactionAsync();
            // }
            var commandTasks = _commands.Select(command => command());
            await Task.WhenAll(commandTasks);

            return _commands.Count;
        }

        public IMongoCollection<T> GetCollection<T>(string name) => _database.GetCollection<T>(name);

        public void Dispose()
        {
            Session?.Dispose();
            GC.SuppressFinalize(this);
        }

        public void AddCommand(Func<Task> func)
        {
            _commands.Add(func);
        }
    }
}
