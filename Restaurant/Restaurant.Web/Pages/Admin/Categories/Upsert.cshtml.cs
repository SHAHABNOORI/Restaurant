using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Restaurant.DataAccess.Data.UnitOfWork.Contract;
using Restaurant.Models;

namespace Restaurant.Web.Pages.Admin.Categories
{
    public class UpsertModel : PageModel
    {
        private readonly IUnitOfWork _unitOfWork;

        [BindProperty]
        public Category CategoryObj { get; set; }

        public UpsertModel(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public IActionResult OnGet(int? id)
        {
            CategoryObj = new Category();

            if (id == null) return Page();

            CategoryObj =
                _unitOfWork.CategoryRepository.GetFirstOrDefault(category => category.Id == id.GetValueOrDefault());

            if (CategoryObj == null)
                return NotFound();

            return Page();
        }

        public IActionResult OnPost(/*Category categoryObj*/)
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            if (CategoryObj.Id == 0)
            {
                _unitOfWork.CategoryRepository.Add(CategoryObj);
            }
            else
            {
                _unitOfWork.CategoryRepository.Update(CategoryObj);
            }
            _unitOfWork.Save();
            return RedirectToPage("./Index");
        }
    }
}