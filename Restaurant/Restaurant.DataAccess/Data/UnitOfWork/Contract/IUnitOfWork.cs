using System;
using Restaurant.DataAccess.Data.Repository.Contract;

namespace Restaurant.DataAccess.Data.UnitOfWork.Contract
{
    public interface IUnitOfWork : IDisposable
    {
        ICategoryRepository CategoryRepository{ get; }

        void Save();
    }
}