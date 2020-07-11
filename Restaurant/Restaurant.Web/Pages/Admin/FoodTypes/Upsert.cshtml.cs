using System;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Restaurant.DataAccess.Data.UnitOfWork.Contract;
using Restaurant.Models;
using Restaurant.Web.ViewModels;

namespace Restaurant.Web.Pages.Admin.FoodTypes
{
    public class UpsertModel : PageModel
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        [BindProperty]
        public FoodTypeViewModel FoodTypeObj { get; set; }

        public UpsertModel(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<IActionResult> OnGet(Guid? id)
        {
            FoodTypeObj = new FoodTypeViewModel();

            if (id == null) return Page();

            var foodTypeFromDb =
              await _unitOfWork.FoodTypeRepository.GetFirstOrDefaultAsync(foodType => foodType.Id == id.GetValueOrDefault());

            _mapper.Map(foodTypeFromDb, FoodTypeObj);

            if (FoodTypeObj == null)
                return NotFound();

            return Page();
        }

        public async Task<IActionResult> OnPost()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            var foodType = new FoodType();
            if (FoodTypeObj.Id == Guid.Empty || FoodTypeObj.Id == default)
            {
                await _unitOfWork.FoodTypeRepository.AddAsync(_mapper.Map(FoodTypeObj, foodType));
            }
            else
            {
                await _unitOfWork.FoodTypeRepository.UpdateAsync(_mapper.Map(FoodTypeObj, foodType));
            }
            await _unitOfWork.SaveAsync();
            return RedirectToPage("./Index");
        }
    }
}