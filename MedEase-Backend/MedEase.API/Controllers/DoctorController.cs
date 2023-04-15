using MedEase.Core.Dtos;
using MedEase.Core.Models;
using MedEase.EF;
using MedEase.EF.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

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

            return Ok(await _doctorService.GetAll());
        }

        [HttpGet("id")]
        public async Task<IActionResult> GetDoctor(int ID)
        {
            if (!ModelState.IsValid) { return BadRequest(new ApiResponse(400, false, ModelState)); };
            return Ok(new ApiResponse(200, true, await _doctorService.GetDoctor(ID)));
        }


        [HttpGet("GetAppointmentsandPattern")]
        public async Task<IActionResult> getAppointmentAndPattern(int Id)
        {
            return Ok( await _doctorService.GetPatternAndAppointmentAsync(Id)); // call Function 
        }

        [HttpPost ("reserve/appointment")]
        public async Task<IActionResult> ReserveAppointment(ReserveAppointmentDto appointmentDto)
        {
            return Ok(await _doctorService.ReserveAppointmentAsync(appointmentDto));
        }

        [HttpPost("create/schedule")]
        public async Task<IActionResult> CreateSchedule(DoctorScheduleDto scheduleDto)
        {
            return Ok(await _doctorService.CreateScheduleAsync(scheduleDto));
        }

        [HttpGet ("Reviews")]
        public async Task<ActionResult<ApiResponse>> Reviews (int Id)
        {
            IEnumerable<ReviewDto> reviews = await _doctorService.GetDoctorReviews(Id);

            return Ok(new ApiResponse(200, true, reviews.ToList()));
        }

        [HttpPost("Reviews")]
        /////////////////////////////////////////
        ///             FOREIGN KEY VALIDATIONS         ==> اللي ياخد باله منها يبقى يسألني عليها
        ///                                     ++++++++
        ///             +++++ انتوا ليه مش حاطين ال     ModelState.IsValid ???
        ////////////////////////////////////////
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
        public async Task<IActionResult> AddDoctorInsurance(int docID, InsuranceDto insurance)
        {
            if (!ModelState.IsValid) { return BadRequest(new ApiResponse(400, false, ModelState)); };
            return Ok(new ApiResponse(200, true, await _doctorService.AddDoctorInsurance(docID, insurance)));
        }

       
    }
}
