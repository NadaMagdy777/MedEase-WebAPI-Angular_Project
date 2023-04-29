using MedEase.Core;
using MedEase.Core.Consts;
using MedEase.Core.Dtos;
using MedEase.Core.Models;
using MedEase.EF;
using MedEase.EF.Data;
using MedEase.EF.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace MedEase.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DoctorController : ControllerBase
    {
        private readonly IDoctorService _doctorService;    
        public DoctorController(IDoctorService doctorService)
        {
            this._doctorService = doctorService;
        }


        [HttpGet]
        public async Task<IActionResult> getAll()
        {

            return Ok(new ApiResponse(200, true, await _doctorService.GetAllDoctors()));

        }

        [HttpGet("id")]
        public async Task<IActionResult> GetDoctor(int ID)
        {
            if (!ModelState.IsValid) { return BadRequest(new ApiResponse(400, false, ModelState)); };
            var Result = await _doctorService.GetDoctor(ID);
            if (Result == null)
            {
                return Ok(new ApiResponse(200, true, message: "not Found"));
            }
            return Ok(new ApiResponse(200, true, Result))
            ;
        }


        [HttpGet("GetAppointmentsandPattern")]
        public async Task<IActionResult> getAppointmentAndPattern(int Id)
        {
            return Ok( await _doctorService.GetPatternAndAppointmentAsync(Id)); // call Function 
        }

        /*[HttpPost ("reserve/appointment")]
        public async Task<IActionResult> ReserveAppointment(ReserveAppointmentDto appointmentDto)
        {
            return Ok(await _doctorService.ReserveAppointmentAsync(appointmentDto));
        }*/

        [HttpPost("schedule/new")]
        public async Task<ActionResult<ApiResponse>> CreateSchedule(DoctorScheduleDto scheduleDto)
        {
            DoctorSchedule schedule = await _doctorService.CreateScheduleAsync(scheduleDto);
            
            if(schedule is null)
            {
                return BadRequest(new ApiResponse(401, false, "null object"));
            }
            return Ok(new ApiResponse(200, true, schedule));
        }

        [HttpGet ("Reviews")]
        public async Task<ActionResult<ApiResponse>> Reviews (int Id)
        {
            IEnumerable<ReviewDto> reviews = await _doctorService.GetDoctorReviews(Id);

            return Ok(new ApiResponse(200, true, reviews.ToList()));
        }

        
        [HttpPost("Reviews")]
        public async Task<ActionResult<ApiResponse>> Reviews(ReviewDto dto)
        {
            if(!ModelState.IsValid) { return BadRequest(new ApiResponse(400, false, ModelState)); };

            return Ok(new ApiResponse(200, true, await _doctorService.CreateReview(dto)));
        }


        [HttpPut]
        public async Task<IActionResult> Edit(DoctorEditDto doctor,int id)
        {

            return Ok(await _doctorService.EditDoctor(doctor,id));
        }
        [HttpPost("SubSpeciality")]
        public async Task<IActionResult> AddDoctorSubSpeciality(int docID ,SubspecialityDto subspeciality)
        {
            if (!ModelState.IsValid) { return BadRequest(new ApiResponse(400, false, ModelState)); };
            return Ok(new ApiResponse(200, true, await _doctorService.AddDoctorSubspiciality(docID, subspeciality)));
        }

        [HttpPost("Certificate")]
        public async Task<IActionResult> AddDoctorCertificate(int docID, CertificateDto certificate)
        {
            if (!ModelState.IsValid) { return BadRequest(new ApiResponse(400, false, ModelState)); };
            return Ok(new ApiResponse(200, true, await _doctorService.AddDoctorCertificate(docID, certificate)));
        }

        [HttpPost("Insurance")]
        public async Task<IActionResult> AddDoctorInsurance(int docID, int insuranceID)
        {
            if (!ModelState.IsValid) { return BadRequest(new ApiResponse(400, false, ModelState)); };
            return Ok(new ApiResponse(200, true, await _doctorService.AddDoctorInsurance(docID, insuranceID)));
        }

        [HttpPut("/Schedule")]
        public async Task <ActionResult<ApiResponse>> EditSchedule(int Id, DoctorEditScheduleDto doctoreditschedualdto)
        {
            if (!ModelState.IsValid) { return BadRequest(new ApiResponse(400, false, ModelState)); };

            return Ok(await _doctorService.EditScheduleDoctor(Id, doctoreditschedualdto));
        }

        [HttpGet("Speciality")]
        public async Task<ActionResult<ApiResponse>> GetSpecialities()
        {
            return Ok(await _doctorService.GetSpecialities());
        }
        
        [HttpPost("prescription/new")]
        public async Task<ActionResult<ApiResponse>> CreatePrescription(PrescriptionDrugDto prescriptionDto)
        {
            return Ok(new ApiResponse(200, true, await _doctorService.CreatePrescriptionAsync(prescriptionDto)));
        }

        [HttpPost("diagnosis/new")]
        public async Task<ActionResult<ApiResponse>> CreateDiagnosis(DiagnosisDto diagnosisDto)
        {
            return Ok(new ApiResponse(200, true, await _doctorService.CreateDiagnosisAsync(diagnosisDto)));
        }

        //[HttpPost("examination/new")]
        //public async Task<ActionResult<ApiResponse>> CreateExamination(ExaminationDto examinationDto)
        //{    
        //    return Ok(new ApiResponse(200, true, await _doctorService.CreateExaminationAsync(examinationDto)));
        //}
    }
}
