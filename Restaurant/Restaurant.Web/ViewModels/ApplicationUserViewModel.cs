using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Identity;

namespace Restaurant.Web.ViewModels
{
    public class ApplicationUserViewModel : IdentityUser
    {
        [Display(Name = "Full Name")]
        public string FirstName { get; set; }

        public string LastName { get; set; }

        [NotMapped]
        public string FullName => $"{FirstName} {LastName}";
    }
}