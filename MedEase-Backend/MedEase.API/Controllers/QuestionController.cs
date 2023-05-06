using MedEase.Core.Consts;
using MedEase.Core.Dtos;
using MedEase.Core;
using MedEase.Core.Interfaces.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace MedEase.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class QuestionController : ControllerBase
    {
        private readonly IQuestionService _questionService;

        public QuestionController(IQuestionService questionService)
        {
            _questionService = questionService;
        }

        [Authorize(Roles = Roles.Doctor)]
        [HttpGet("Doctor/Unanswered")]
        public async Task<ActionResult<ApiResponse>> DoctorUnansweredQuestions()
        {
            if (!int.TryParse(User.FindFirstValue(ClaimTypes.NameIdentifier), out int docId))
            { return BadRequest(new ApiResponse(401, false, "User Not Found")); }

            return Ok(await _questionService.GetQuestionsByDoctorSpeciality(docId));
        }

        [Authorize(Roles = Roles.Doctor)]
        [HttpGet("Doctor/Answered")]
        public async Task<ActionResult<ApiResponse>> DoctorAnsweredQuestions()
        {
            if (!int.TryParse(User.FindFirstValue(ClaimTypes.NameIdentifier), out int docId))
            { return BadRequest(new ApiResponse(401, false, "User Not Found")); }

            return Ok(await _questionService.GetDoctorAnsweredQuestions(docId));
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ApiResponse>> GetQuestion(int id)
        {
            if (!ModelState.IsValid) { return BadRequest(new ApiResponse(400, false, ModelState)); };

            //if (!int.TryParse(User.FindFirstValue(ClaimTypes.NameIdentifier), out int docId))
            //    { return BadRequest(new ApiResponse(401, false, "User Not Found")); }

            return Ok(await _questionService.GetQuestion(id));
        }

        [Authorize(Roles = Roles.Doctor)]                    //Not Finsished
        [HttpPut("Doctor/Answer")]
        public async Task<ActionResult<ApiResponse>> DoctorAnswerQuestion(AnswerDto dto)
        {
            if (!ModelState.IsValid) { return BadRequest(new ApiResponse(400, false, ModelState)); };

            //if (!int.TryParse(User.FindFirstValue(ClaimTypes.NameIdentifier), out int docId))
            //    { return BadRequest(new ApiResponse(401, false, "User Not Found")); }

            return Ok(await _questionService.DoctorAnswerQuestion(dto));
        }

        [Authorize(Roles = Roles.Patient)]
        [HttpPost("Patient/Ask")]
        public async Task<ActionResult<ApiResponse>> PatientAskQuestion(PatientQuestionDto dto)
        {
            //if (!int.TryParse(User.FindFirstValue(ClaimTypes.NameIdentifier), out int patientID))
            //{ return BadRequest(new ApiResponse(401, false, "User Not Found")); }

            //dto.PatientId = patientID;

            /////////       ==>> All The Above Code Could be Reomved In case FrontEnd Gives me the PtID  <<==       /////////
            if (!ModelState.IsValid) { return BadRequest(new ApiResponse(400, false, ModelState)); };

            return Ok(await _questionService.PatientAskQuestion(dto));
        }

        [Authorize(Roles = Roles.Patient)]
        [HttpGet("Patient/{isAnswered:bool}")]
        public async Task<ActionResult<ApiResponse>> PatientAnsweredQuestions(bool isAnswered)
        {
            if (!int.TryParse(User.FindFirstValue(ClaimTypes.NameIdentifier), out int patientID))
            { return BadRequest(new ApiResponse(401, false, "User Not Found")); }

            return Ok(await _questionService.GetPatientQuestions(patientID, isAnswered));
        }
    }
}
