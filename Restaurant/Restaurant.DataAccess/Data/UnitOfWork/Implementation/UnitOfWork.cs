using Restaurant.DataAccess.Data.Repository.Contract;
using Restaurant.DataAccess.Data.Repository.Implementation;
using Restaurant.DataAccess.Data.UnitOfWork.Contract;

namespace Restaurant.DataAccess.Data.UnitOfWork.Implementation
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationDbContext _db;

        public ICategoryRepository CategoryRepository { get; }

        public UnitOfWork(ApplicationDbContext db)
        {
            _db = db;
            CategoryRepository = new CategoryRepository(_db);
        }

        public void Dispose()
        {
            _db.Dispose();
        }

        public void Save()
        {
            _db.SaveChanges();
        }
    }
}