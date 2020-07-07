using System;
using Microsoft.AspNetCore.Mvc;
using Restaurant.DataAccess.Data.UnitOfWork.Contract;

namespace Restaurant.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FoodTypeController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;

        public FoodTypeController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        [HttpGet]
        public IActionResult Get()
        {
            return Json(new { data = _unitOfWork.FoodTypeRepository.GetAll() });
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(Guid id)
        {
            var objFromDb = _unitOfWork.FoodTypeRepository.GetFirstOrDefault(foodType => foodType.Id == id);
            if (objFromDb == null)
            {
                return Json(new { success = false, message = "Error while deleting" });
            }
            _unitOfWork.FoodTypeRepository.Remove(objFromDb);
            _unitOfWork.Save();
            return Json(new { success = true, message = "Delete Successful" });
        }
    }
}
