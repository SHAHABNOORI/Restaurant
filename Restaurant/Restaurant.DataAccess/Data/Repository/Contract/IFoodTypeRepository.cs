using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc.Rendering;
using Restaurant.Models;

namespace Restaurant.DataAccess.Data.Repository.Contract
{
    public interface IFoodTypeRepository : IRepository<FoodType>
    {
        IEnumerable<SelectListItem> GetFoodTypeListForDropDown();

        void Update(FoodType foodType);
    }
}