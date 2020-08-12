using AutoMapper;
using Restaurant.Models;
using Restaurant.Web.ViewModels;

namespace Restaurant.Web.Profiles
{
    public class ApplicationUserProfile : Profile
    {
        public ApplicationUserProfile()
        {
            CreateMap<ApplicationUser, ApplicationUserViewModel>().ReverseMap();
        }
    }
}