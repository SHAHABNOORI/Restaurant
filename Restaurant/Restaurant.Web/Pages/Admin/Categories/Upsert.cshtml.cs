using System;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Restaurant.DataAccess.Data.UnitOfWork.Contract;
using Restaurant.Models;
using Restaurant.Web.ViewModels;

namespace Restaurant.Web.Pages.Admin.Categories
{
    public class UpsertModel : PageModel
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        [BindProperty]
        public CategoryViewModel CategoryObj { get; set; }

        public UpsertModel(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public IActionResult OnGet(Guid? id)
        {
            CategoryObj = new CategoryViewModel();

            if (id == null) return Page();

            var categoryFromDb =
                _unitOfWork.CategoryRepository.GetFirstOrDefault(category => category.Id == id.GetValueOrDefault());

            _mapper.Map(categoryFromDb, CategoryObj);

            if (CategoryObj == null)
                return NotFound();

            return Page();
        }

        public async Task<IActionResult> OnPost(/*Category categoryObj*/)
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            var category = new Category();
            if (CategoryObj.Id == Guid.Empty || CategoryObj.Id == default)
            {
                _unitOfWork.CategoryRepository.Add(_mapper.Map(CategoryObj, category));
            }
            else
            {
                _unitOfWork.CategoryRepository.Update(_mapper.Map(CategoryObj, category));
            }
            await _unitOfWork.SaveAsync();
            return RedirectToPage("./Index");
        }
    }
}