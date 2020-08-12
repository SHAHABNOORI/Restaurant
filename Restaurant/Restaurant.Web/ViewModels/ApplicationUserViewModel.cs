using Microsoft.AspNetCore.Identity;

namespace Restaurant.Web.ViewModels
{
    public class ApplicationUserViewModel : IdentityUser
    {
        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string FullName => $"{FirstName} {LastName}";
    }
}