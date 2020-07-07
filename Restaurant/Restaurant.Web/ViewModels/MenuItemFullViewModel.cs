using System;
using System.ComponentModel.DataAnnotations;

namespace Restaurant.Web.ViewModels
{
    public class MenuItemFullViewModel
    {
        public Guid Id { get; set; }

        [Required]
        public string Name { get; set; }

        public string Description { get; set; }

        public string Image { get; set; }

        [Range(1, int.MaxValue, ErrorMessage = "Price should be greater than $1")]
        public double Price { get; set; }

        [Display(Name = "Category")]
        public Guid CategoryId { get; set; }

        public CategoryViewModel Category { get; set; }

        [Display(Name = "Food Type")]
        public Guid FoodTypeId { get; set; }

        public FoodTypeViewModel FoodType { get; set; }
    }
}