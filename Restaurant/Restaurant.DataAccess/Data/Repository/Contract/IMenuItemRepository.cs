using System;
using Restaurant.Models;

namespace Restaurant.DataAccess.Data.Repository.Contract
{
    public interface IMenuItemRepository : IRepository<MenuItem, Guid>, IRepositoryAsync<MenuItem, Guid>
    {
        void Update(MenuItem menuItem);
    }
}