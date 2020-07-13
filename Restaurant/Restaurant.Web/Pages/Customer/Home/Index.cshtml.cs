using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Restaurant.DataAccess.Data.UnitOfWork.Contract;
using Restaurant.Web.ViewModels;

namespace Restaurant.Web.Pages.Customer.Home
{
    public class IndexModel : PageModel
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public IndexModel(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public IEnumerable<MenuItemFullViewModel> MenuItemList { get; set; } = new List<MenuItemFullViewModel>();

        public IEnumerable<CategoryViewModel> CategoryList { get; set; } = new List<CategoryViewModel>();

        public async Task OnGet()
        {
            _mapper.Map(await _unitOfWork.MenuItemRepository.
                GetAllAsync(null, null, "Category,FoodType"), MenuItemList);

            _mapper.Map(await _unitOfWork.CategoryRepository
                .GetAllAsync(null, category => category.OrderBy(c => c.DisplayOrder), null), CategoryList);
        }
    }
}