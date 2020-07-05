﻿using System.ComponentModel.DataAnnotations;

namespace Restaurant.Models
{
    public class Category
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [Display(Name = "Category Name")]
        public string Name { get; set; }

        [Required]
        [Display(Name = "Display Order")]
        public int DisplayOrder { get; set; }
    }
}