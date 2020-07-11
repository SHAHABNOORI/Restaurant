using System;
using System.Threading.Tasks;
using Restaurant.DataAccess.Data.Repository.Contract;

namespace Restaurant.DataAccess.Data.UnitOfWork.Contract
{
    public interface IUnitOfWork : IDisposable
    {
        ICategoryRepository CategoryRepository{ get; }

        IFoodTypeRepository FoodTypeRepository{ get; }

        IMenuItemRepository MenuItemRepository{ get; }

        void Save();
        Task SaveAsync();
    }
}