using TaskList.Domain.UnitOfWorks.Abstract;
using TaskList.Domain.UnitOfWorks.MongoRepository.Abstract;

namespace TaskList.Domain.UnitOfWorks.MongoRepository
{
    public class MongoUnitOfWork : IUnitOfWork
    {
        private readonly IMongoContext _context;

        //TODO release Task Repository injection
        public MongoUnitOfWork(IMongoContext context)
        {
            _context = context;
        }

        public async Task<bool> Commit()
        {
            var changeAmount = await _context.SaveChangesAsync();

            return changeAmount > 0;
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
