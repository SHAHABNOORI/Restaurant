using AutoMapper;
using Restaurant.Models;
using Restaurant.Web.ViewModels;

namespace Restaurant.Web.Profiles
{
    public class FoodTypeProfile : Profile
    {
        public FoodTypeProfile()
        {
            CreateMap<FoodType, FoodTypeViewModel>().ReverseMap();
        }
    }
}