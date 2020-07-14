using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity.UI.Services;

namespace Restaurant.Web.Helpers
{
    public class CustomEmailSender : IEmailSender
    {
        public Task SendEmailAsync(string email, string subject, string htmlMessage)
        {
            throw new System.NotImplementedException();
        }
    }
}