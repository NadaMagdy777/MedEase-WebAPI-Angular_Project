using MedEase.Core;
using MedEase.Core.Interfaces.Services;
using MedEase.EF.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace MedEase.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GeneralController : ControllerBase
    {
        private readonly IGeneralService _generalService;

        public GeneralController(IGeneralService generalService)
        {
            this._generalService = generalService;
        }


        [HttpGet("Addresses")]
        public async Task<ActionResult<ApiResponse>> GetAddresses()
        {
            return Ok(await _generalService.GetAddresses());
        }

        [HttpGet("Speciality")]
        public async Task<ActionResult<ApiResponse>> GetSpecialities()
        {
            return Ok(await _generalService.GetSpecialities());
        }        
        
        [HttpGet("SubSpeciality")]
        public async Task<ActionResult<ApiResponse>> GetSubSpecialities()
        {
            return Ok(await _generalService.GetSubSpecialities());
        }
        
        [HttpGet("Insurance")]
        public async Task<ActionResult<ApiResponse>> GetInsurances()
        {
            return Ok(await _generalService.GetInsurances());
        }
    }
}
