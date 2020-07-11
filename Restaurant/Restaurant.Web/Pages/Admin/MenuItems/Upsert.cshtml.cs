using System;
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

        public IActionResult OnGet(Guid? id)
        {
            //var categoryList = DropDownHelper.Get(await _unitOfWorkAsync.CategoryRepositoryAsync.GetAllAsync(), "Name", "Id"); ;
            //var foodTypeList = DropDownHelper.Get(await _unitOfWorkAsync.FoodTypeRepositoryAsync.GetAllAsync(), "Name", "Id"); ;

            MenuItemObj = new MenuItemIndexPageViewModel();

            if (id == null) return Page();

            var categoryFromDb =
                _unitOfWork.CategoryRepository.GetFirstOrDefault(category => category.Id == id.GetValueOrDefault());

            _mapper.Map(categoryFromDb, MenuItemObj);

            if (MenuItemObj == null)
                return NotFound();

            return Page();
        }

        public IActionResult OnPost(/*Category categoryObj*/)
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            var category = new Category();
            //if (MenuItemObj.Id == Guid.Empty || MenuItemObj.Id == default)
            //{
            //    _unitOfWork.CategoryRepository.Add(_mapper.Map(MenuItemObj, category));
            //}
            //else
            //{
            //    _unitOfWork.CategoryRepository.Update(_mapper.Map(MenuItemObj, category));
            //}
            _unitOfWork.Save();
            return RedirectToPage("./Index");
        }
    }
}