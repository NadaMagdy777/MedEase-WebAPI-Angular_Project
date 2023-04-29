using MedEase.Core;
using MedEase.Core.Dtos;
using MedEase.Core.Interfaces.Services;
using MedEase.Core;
using MedEase.Core.Dtos;
using MedEase.Core.Interfaces.Services;
using MedEase.EF.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace MedEase.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PatientController : ControllerBase
    {
        private readonly IPatientService _patientService;

        public PatientController(IPatientService patientService)
        {
            this._patientService = patientService;
        }

        [HttpPost ("Investigation")]
        public async Task<ActionResult<ApiResponse>> AddInvestigation(AppointmentInvestigationDto dto)
        {
            if (!ModelState.IsValid) { return BadRequest(new ApiResponse(400, false, ModelState)); };

            return Ok(await _patientService.AddAppointmentInestigation(dto));
        }

        [HttpGet("id")]
        public async Task<IActionResult> GetPatient(int ID)
        {
            if (!ModelState.IsValid) { return BadRequest(new ApiResponse(400, false, ModelState)); };
            var Result = await _patientService.GetPatient(ID);
            if (Result == null)
            {
                return Ok(new ApiResponse(200, true, message: "not Found"));
            }
            return Ok(new ApiResponse(200, true, Result))
            ;
        }
        
        [HttpPut]
        public async Task<IActionResult> Edit(PatientEditDto patient, int id)
        {
            if (!ModelState.IsValid) { return BadRequest(new ApiResponse(400, false, ModelState)); };

            return Ok(new ApiResponse(200, true, await _patientService.EditPatient(patient,id)));
        }

        [HttpPost("MedicalHistor")]
        public async Task<IActionResult> AddMedicalHistory(PatientMedicalHistoryDto history,int PatientID)
        {

            if (!ModelState.IsValid) { return BadRequest(new ApiResponse(400, false, ModelState)); };
    
            return Ok(new ApiResponse(200, true, await _patientService.AddMedicalHistory(history, PatientID)))
            ;
        }

        [HttpPost("Insurance")]
        public async Task<IActionResult> AddPatientInsurance(int PatientID, int insuranceID)
        {
            if (!ModelState.IsValid) { return BadRequest(new ApiResponse(400, false, ModelState)); };
            return Ok(new ApiResponse(200, true, await _patientService.AddPatientInsurance(PatientID, insuranceID)));
        }
    }
}
