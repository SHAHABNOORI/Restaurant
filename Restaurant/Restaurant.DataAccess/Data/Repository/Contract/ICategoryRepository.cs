using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc.Rendering;
using Restaurant.Models;

namespace Restaurant.DataAccess.Data.Repository.Contract
{
    public interface ICategoryRepository : IRepository<Category, Guid>, IRepositoryAsync<Category, Guid>
    {
        IEnumerable<SelectListItem> GetCategoryListForDropDown();

        void Update(Category category);
    }
}