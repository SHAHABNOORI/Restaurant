using System;
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

        public IActionResult OnGet(Guid? id)
        {
            FoodTypeObj = new FoodTypeViewModel();

            if (id == null) return Page();

            var foodTypeFromDb =
                _unitOfWork.FoodTypeRepository.GetFirstOrDefault(foodType => foodType.Id == id.GetValueOrDefault());

            _mapper.Map(foodTypeFromDb, FoodTypeObj);

            if (FoodTypeObj == null)
                return NotFound();

            return Page();
        }

        public IActionResult OnPost()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            var foodType = new FoodType();
            if (FoodTypeObj.Id == Guid.Empty || FoodTypeObj.Id == default)
            {
                _unitOfWork.FoodTypeRepository.Add(_mapper.Map(FoodTypeObj, foodType));
            }
            else
            {
                _unitOfWork.FoodTypeRepository.Update(_mapper.Map(FoodTypeObj, foodType));
            }
            _unitOfWork.Save();
            return RedirectToPage("./Index");
        }
    }
}