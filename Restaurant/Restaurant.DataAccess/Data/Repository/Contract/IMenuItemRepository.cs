using Restaurant.Models;

namespace Restaurant.DataAccess.Data.Repository.Contract
{
    public interface IMenuItemRepository : IRepository<MenuItem>
    {
        void Update(MenuItem menuItem);
    }
}