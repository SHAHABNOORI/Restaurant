using AutoMapper;
using Restaurant.Models;
using Restaurant.Web.ViewModels;

namespace Restaurant.Web.Profiles
{
    public class CategoryProfile : Profile
    {
        public CategoryProfile()
        {
            CreateMap<Category, CategoryViewModel>().ReverseMap();
        }
    }
}