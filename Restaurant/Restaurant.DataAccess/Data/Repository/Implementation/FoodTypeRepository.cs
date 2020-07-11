using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Restaurant.DataAccess.Data.Repository.Contract;
using Restaurant.Models;

namespace Restaurant.DataAccess.Data.Repository.Implementation
{
    public class FoodTypeRepository : Repository<FoodType, Guid>, IFoodTypeRepository
    {
        private readonly ApplicationDbContext _db;

        public FoodTypeRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }

        public IEnumerable<SelectListItem> GetFoodTypeListForDropDown()
        {
            return _db.FoodTypes.Select(i => new SelectListItem()
            {
                Text = i.Name,
                Value = i.Id.ToString()
            });
        }

        public void Update(FoodType foodType)
        {
            var objFromDb = _db.FoodTypes.FirstOrDefault(s => s.Id == foodType.Id);

            if (objFromDb == null) return;
            objFromDb.Name = foodType.Name;
        }

        public async Task UpdateAsync(FoodType foodType)
        {
            var objFromDb = await _db.FoodTypes.FirstOrDefaultAsync(s => s.Id == foodType.Id);

            if (objFromDb == null) return;
            objFromDb.Name = foodType.Name;
        }
    }
}