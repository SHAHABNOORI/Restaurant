using AutoMapper;
using Restaurant.Models;
using Restaurant.Web.ViewModels;

namespace Restaurant.Web.Profiles
{
    public class MenuItemProfile : Profile
    {
        public MenuItemProfile()
        {
            CreateMap<MenuItem, MenuItemViewModel>().ReverseMap();
        }
    }
}