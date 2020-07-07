using System;
using System.ComponentModel.DataAnnotations;

namespace Restaurant.Web.ViewModels
{
    public class CategoryViewModel
    {
        public Guid Id { get; set; }

        [Display(Name = "Category Name")]
        [Required]
        public string Name { get; set; }

        [Display(Name = "Display Order")]
        [Required]
        public int DisplayOrder { get; set; }
    }
}