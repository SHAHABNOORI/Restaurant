using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Restaurant.Web.Helpers
{
    public static class DropDownHelper
    {
        public static IEnumerable<SelectListItem> Get<T>(IEnumerable<T> enumerable, string text, string value)
        {

            var result = enumerable.Select(item => new SelectListItem()
            {
                Text = item.GetType().GetProperty(text)?.GetValue(item, null)?.ToString(),
                Value = item.GetType().GetProperty(value)?.GetValue(item, null)?.ToString(),
            });

            return result;
        }
    }
}