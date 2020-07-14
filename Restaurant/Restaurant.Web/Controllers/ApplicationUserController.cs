using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Restaurant.DataAccess.Data.UnitOfWork.Contract;

namespace Restaurant.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ApplicationUserController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;

        public ApplicationUserController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            return Json(new { data = await _unitOfWork.ApplicationUserRepository.GetAllAsync() });
        }

        [HttpPost]
        public async Task<IActionResult> LockUnLock([FromBody] string id)
        {
            var objFromDb = await _unitOfWork.ApplicationUserRepository.GetFirstOrDefaultAsync(applicationUser => applicationUser.Id == id);
            if (objFromDb == null)
            {
                return Json(new { success = false, message = "Error while Locking/UnLocking" });
            }

            if (objFromDb.LockoutEnd == null && objFromDb.LockoutEnd > DateTime.Now)
            {
                _unitOfWork.ApplicationUserRepository.Lock(objFromDb);
            }
            else
            {
                _unitOfWork.ApplicationUserRepository.UnLock(objFromDb);
            }

            _unitOfWork.ApplicationUserRepository.Remove(objFromDb);

            await _unitOfWork.SaveAsync();
            return Json(new { success = true, message = "Operation Successful" });
        }
    }
}
