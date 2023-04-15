using MedEase.Core;
using MedEase.Core.Consts;
using MedEase.Core.Dtos;
using MedEase.Core.Models;
using MedEase.EF;
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

        /////////////////////////////////////////
        ///             FOREIGN KEY VALIDATIONS         ==> اللي ياخد باله منها يبقى يسألني عليها
        ///                                     ++++++++
        ///             +++++ انتوا ليه مش حاطين ال     ModelState.IsValid ???
        ////////////////////////////////////////
        [HttpPost("Reviews")]
        public async Task<ActionResult<ApiResponse>> Reviews(ReviewDto dto)
        {
            if(!ModelState.IsValid) { return BadRequest(new ApiResponse(400, false, ModelState)); };

            return Ok(new ApiResponse(200, true, await _doctorService.CreateReview(dto)));
        }

        [HttpPost ("Certificates")]
        public async Task<ActionResult<ApiResponse>> CreateCertificate(CertificationDto dto)
        {
            return Ok();
        }

        [Authorize (Roles = ("Doctor"))]
        [HttpGet ("/Questions/Unanswered")]
        public async Task<ActionResult<ApiResponse>> Questions()
        {
            if(!int.TryParse(User.FindFirstValue(ClaimTypes.NameIdentifier), out int docId))
                { return BadRequest(new ApiResponse(401, false, "User Not Found")); }

            return Ok(await _doctorService.GetQuestionsByDoctorSpeciality(docId));
        }
        
        [Authorize (Roles = ("Doctor"))]
        [HttpGet ("/Questions/Answered")]
        public async Task<ActionResult<ApiResponse>> DoctorQuestions()
        {
            if(!int.TryParse(User.FindFirstValue(ClaimTypes.NameIdentifier), out int docId))
                { return BadRequest(new ApiResponse(401, false, "User Not Found")); }

            return Ok(await _doctorService.GetDoctorAnsweredQuestions(docId));
        }
        
        [Authorize (Roles = ("Doctor"))]                    //Not Finsished
        [HttpPost ("/Questions/Answer")]
        public async Task<ActionResult<ApiResponse>> DoctorAnswerQuestions(AnswerDto dto)
        {
            if (!ModelState.IsValid) { return BadRequest(new ApiResponse(400, false, ModelState)); };

            //if (!int.TryParse(User.FindFirstValue(ClaimTypes.NameIdentifier), out int docId))
            //    { return BadRequest(new ApiResponse(401, false, "User Not Found")); }

            return Ok(await _doctorService.DoctorAnswerQuestions(dto));
        }



    }
}
