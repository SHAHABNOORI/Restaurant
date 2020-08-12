using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Restaurant.Utilities;

namespace Restaurant.Web.Pages
{
    [Authorize(Roles = StaticDetails.ManagerRole)]
    public abstract class AdminPageModel: PageModel
    {
        
    }
}