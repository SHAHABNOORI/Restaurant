using System;
using System.IO;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Restaurant.DataAccess.Data.UnitOfWork.Contract;
using Restaurant.Models;
using Restaurant.Web.Helpers;
using Restaurant.Web.ViewModels;

namespace Restaurant.Web.Pages.Admin.MenuItems
{
    public class UpsertModel : PageModel
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IWebHostEnvironment _webHostEnvironment;

        [BindProperty]
        public MenuItemIndexPageViewModel MenuItemObj { get; set; }

        public UpsertModel(IUnitOfWork unitOfWork, IMapper mapper, IWebHostEnvironment webHostEnvironment)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _webHostEnvironment = webHostEnvironment;
        }

        public async Task<IActionResult> OnGet(Guid? id)
        {
            var categoryList = DropDownHelper.Get(await _unitOfWork.CategoryRepository.GetAllAsync(), nameof(Category.Name), nameof(Category.Id));
            var foodTypeList = DropDownHelper.Get(await _unitOfWork.FoodTypeRepository.GetAllAsync(), nameof(FoodType.Name), nameof(FoodType.Id));

            MenuItemObj = new MenuItemIndexPageViewModel()
            {
                CategoryList = categoryList,
                FoodTypeList = foodTypeList
            };

            if (id == Guid.Empty || id == default) return Page();
            var menuItemFromDb =
                await _unitOfWork.MenuItemRepository.
                    GetFirstOrDefaultAsync(menuItem => menuItem.Id == id.GetValueOrDefault());

            _mapper.Map(menuItemFromDb, MenuItemObj.MenuItem);

            if (MenuItemObj == null)
            {
                return NotFound();
            }

            return Page();
        }

        public async Task<IActionResult> OnPost()
        {
            var webRootPath = _webHostEnvironment.WebRootPath;

            // Get Sended Files 
            var files = HttpContext.Request.Form.Files;

            if (!ModelState.IsValid)
            {
                return Page();
            }
            var selectedCategory = await _unitOfWork.CategoryRepository.GetAsync(MenuItemObj.MenuItem.CategoryId);
            var selectedFoodType = await _unitOfWork.FoodTypeRepository.GetAsync(MenuItemObj.MenuItem.FoodTypeId);
            var menuItem = new MenuItem { Category = selectedCategory, FoodType = selectedFoodType };
            if (MenuItemObj.MenuItem.Id == Guid.Empty || MenuItemObj.MenuItem.Id == default)
            {

                var (fileName, extension) = await FileHelper.CreateImageFile(webRootPath, files, @"images\menuItems");
                MenuItemObj.MenuItem.Image = @"\images\menuItems\" + fileName + extension;
                await _unitOfWork.MenuItemRepository.AddAsync(_mapper.Map(MenuItemObj.MenuItem, menuItem));
            }
            else
            {
                var objFromDb = await _unitOfWork.MenuItemRepository.GetAsync(MenuItemObj.MenuItem.Id);
                if (files.Count > 0)
                {
                    // Remove Current Image
                    var currentImagePath = Path.Combine(webRootPath, objFromDb.Image.TrimStart('\\'));
                    if (System.IO.File.Exists(currentImagePath))
                    {
                        System.IO.File.Delete(currentImagePath);
                    }

                    var (fileName, extension) = await FileHelper.CreateImageFile(webRootPath, files, @"images\menuItems");
                    MenuItemObj.MenuItem.Image = @"\images\menuItems\" + fileName + extension;
                }
                else
                {
                    MenuItemObj.MenuItem.Image = objFromDb.Image;
                }

                await _unitOfWork.MenuItemRepository.UpdateAsync(_mapper.Map(MenuItemObj.MenuItem, menuItem));
            }
            await _unitOfWork.SaveAsync();
            return RedirectToPage("./Index");
        }
    }
}


