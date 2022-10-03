using Microsoft.Extensions.Configuration;
using MongoDB.Driver;
using TaskList.Domain.Contexts.Abstract;

namespace TaskList.Domain.Contexts
{
    public class MongoContext : IMongoContext
    {
        private IMongoDatabase _database { get; set; }
        public IClientSessionHandle Session { get; set; }
        public MongoClient MongoClient { get; set; }
        private readonly List<Func<Task>> _commands;
        private readonly IConfiguration _configuration;

        public MongoContext(IConfiguration configuration)
        {
            _configuration = configuration;
            _commands = new List<Func<Task>>();
        }

        public async Task<int> SaveChangesAsync()
        {
            ConfigureMongo();

            using (Session = await MongoClient.StartSessionAsync())
            {
                Session.StartTransaction();

                var commandTasks = _commands.Select(c => c());

                await Task.WhenAll(commandTasks);

                await Session.CommitTransactionAsync();
            }

            return _commands.Count;
        }

        private void ConfigureMongo()
        {
            if (MongoClient != null)
            {
                return;
            }

            MongoClient = new MongoClient(_configuration["MongoSettings:Connection"]);

            _database = MongoClient.GetDatabase(_configuration["MongoSettings:DatabaseName"]);
        }

        public IMongoCollection<T> GetCollection<T>(string name)
        {
            ConfigureMongo();

            return _database.GetCollection<T>(name);
        }

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
