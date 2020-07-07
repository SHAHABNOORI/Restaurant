using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Restaurant.Web.ViewModels
{
    public class MenuItemIndexPageViewModel
    {
        public MenuItemViewModel MenuItem { get; set; } = new MenuItemViewModel();

        public IEnumerable<SelectListItem> CategoryList { get; set; } = new List<SelectListItem>();

        public IEnumerable<SelectListItem> FoodTypeList { get; set; } = new List<SelectListItem>();
    }
}