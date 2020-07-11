using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Restaurant.DataAccess.Data.Repository.Contract;
using Restaurant.Models;

namespace Restaurant.DataAccess.Data.Repository.Implementation
{
    public class MenuItemRepository : Repository<MenuItem, Guid>, IMenuItemRepository
    {
        private readonly ApplicationDbContext _db;

        public MenuItemRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }

        public void Update(MenuItem menuItem)
        {
            var menuItemFromDb = _db.MenuItems.FirstOrDefault(m => m.Id == menuItem.Id);

            if (menuItemFromDb == null) return;
            menuItemFromDb.Name = menuItem.Name;
            menuItemFromDb.Category = menuItem.Category;
            menuItemFromDb.Description = menuItem.Description;
            menuItemFromDb.FoodType = menuItem.FoodType;
            menuItemFromDb.Price = menuItem.Price;
            if (menuItem.Image != null)
            {
                menuItemFromDb.Image = menuItem.Image;
            }
        }

        public async Task UpdateAsync(MenuItem menuItem)
        {
            var menuItemFromDb = await _db.MenuItems.FirstOrDefaultAsync(m => m.Id == menuItem.Id);

            if (menuItemFromDb == null) return;
            menuItemFromDb.Name = menuItem.Name;
            menuItemFromDb.Category = menuItem.Category;
            menuItemFromDb.Description = menuItem.Description;
            menuItemFromDb.FoodType = menuItem.FoodType;
            menuItemFromDb.Price = menuItem.Price;
            if (menuItem.Image != null)
            {
                menuItemFromDb.Image = menuItem.Image;
            }
        }
    }
}