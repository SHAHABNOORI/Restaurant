using System.Collections.Generic;

namespace Restaurant.Models
{
    public class FoodType : BaseEntity
    {
        public string Name { get; set; }

        public ICollection<MenuItem> MenuItems { get; set; }
    }
}