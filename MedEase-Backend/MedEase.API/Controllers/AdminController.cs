using MedEase.Core.Dtos;
using MedEase.Core;
using Microsoft.AspNetCore.Mvc;
using MedEase.EF.Services;
using MedEase.Core.Interfaces.Services;

namespace MedEase.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AdminController : Controller
    {
       
        private readonly IAdminService _adminService;

        public AdminController(IAdminService adminService)
        {
            this._adminService = adminService;
        }
        
        [HttpPost("Add/Insurance")]
        public async Task<IActionResult> AddInsurance(InsuranceDto insurance)
        {
            if (!ModelState.IsValid) { return BadRequest(new ApiResponse(400, false, ModelState)); };
            return Ok(new ApiResponse(200, true, await _adminService.AddInsurance(insurance)));
        }
    }
}
