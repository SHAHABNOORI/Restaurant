namespace Restaurant.Models
{
    public class MenuItem : BaseEntity
    {

        public string Name { get; set; }

        public string Description { get; set; }

        public string Image { get; set; }

        public double Price { get; set; }

        public Category Category { get; set; }

        public FoodType FoodType { get; set; }
    }
}