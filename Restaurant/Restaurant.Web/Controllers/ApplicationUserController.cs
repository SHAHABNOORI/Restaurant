using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Restaurant.DataAccess.Data.UnitOfWork.Contract;
using Restaurant.Web.ViewModels;

namespace Restaurant.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ApplicationUserController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;


        public ApplicationUserController(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var applicationUsers = new List<ApplicationUserViewModel>();
            _mapper.Map(await _unitOfWork.ApplicationUserRepository.GetAllAsync(), applicationUsers);
            return Json(new { data = applicationUsers });
        }

        [HttpPost]
        public async Task<IActionResult> LockUnLock([FromBody] string id)
        {
            var objFromDb = await _unitOfWork.ApplicationUserRepository.GetFirstOrDefaultAsync(applicationUser => applicationUser.Id == id);
            if (objFromDb == null)
            {
                return Json(new { success = false, message = "Error while Locking/UnLocking" });
            }

            if (objFromDb.LockoutEnd != null && (objFromDb.LockoutEnd > DateTime.Now))
            {
                _unitOfWork.ApplicationUserRepository.Lock(objFromDb);
            }
            else
            {
                _unitOfWork.ApplicationUserRepository.UnLock(objFromDb);
            }
            
            await _unitOfWork.SaveAsync();
            return Json(new { success = true, message = "Operation Successful" });
        }
    }
}
