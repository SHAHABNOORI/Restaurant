using AutoMapper;
using Restaurant.Models;
using Restaurant.Web.ViewModels;

namespace Restaurant.Web.Profiles
{
    public class MenuItemFullProfile : Profile
    {
        public MenuItemFullProfile()
        {
            CreateMap<MenuItem, MenuItemFullViewModel>().ReverseMap();
        }
    }
}