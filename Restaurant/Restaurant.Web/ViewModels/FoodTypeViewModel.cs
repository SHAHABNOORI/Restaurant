using System;
using System.ComponentModel.DataAnnotations;

namespace Restaurant.Web.ViewModels
{
    public class FoodTypeViewModel
    {
        public Guid Id { get; set; }

        [Required]
        [Display(Name = "Food Type Name")]
        public string Name { get; set; }
    }
}