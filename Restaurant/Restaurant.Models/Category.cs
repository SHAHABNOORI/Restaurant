using System.Collections.Generic;

namespace Restaurant.Models
{
    public class Category : BaseEntity
    {
        public string Name { get; set; }

        public int DisplayOrder { get; set; }

        public ICollection<MenuItem> MenuItems { get; set; }
    }
}