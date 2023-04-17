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

            return Ok(new ApiResponse(200, true, await _doctorService.GetAll()));

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

        //[HttpPut]
        //[Route("")]
        //public ActionResult<ApiResponse> EditSchedule([FromServices] ApplicationDbContext context, int Id, DoctorEditScheduleDto dto)
        //{







        //    return Ok(new ApiResponse(200, true, data));
        //}
    }
}
