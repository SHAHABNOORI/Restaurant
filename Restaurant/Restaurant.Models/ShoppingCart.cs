namespace Restaurant.Models
{
    public class ShoppingCart : BaseEntity
    {
        public ShoppingCart()
        {
            Count = 1;
        }

        public MenuItem MenuItem { get; set; }

        public ApplicationUser ApplicationUser { get; set; }

        public int Count { get; set; }

    }
}