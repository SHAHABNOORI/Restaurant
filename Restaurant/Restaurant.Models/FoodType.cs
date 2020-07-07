using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Restaurant.Models
{
    public class FoodType : BaseEntity
    {
        public string Name { get; set; }

        public ICollection<MenuItem> MenuItems { get; set; }
    }
}