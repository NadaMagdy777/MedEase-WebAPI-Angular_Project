using MedEase.Core;
using MedEase.Core.Consts;
using MedEase.Core.Dtos;
using MedEase.Core.Interfaces.Services;
using MedEase.EF.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace MedEase.API.Controllers
{

    /// <summary>
    /// عايز اهندل ان اليوزر يكنسل الحجز قبل ماييجي معاده واخلي الستاتس ب كانسلد
    /// </summary>

    [Route("api/[controller]")]
    [ApiController]
    public class AppointmentController : ControllerBase
    {
        private readonly IAppointmentService _appointmentService;

        public AppointmentController(IAppointmentService appointmentService)
        {
            _appointmentService = appointmentService;
        }

        [Authorize (Roles = Roles.Doctor)]
        [HttpGet("Doctor/Pending")]
        public async Task<ActionResult<ApiResponse>> GetDoctorPendingAppointments()
        {
            if (!int.TryParse(User.FindFirstValue(ClaimTypes.NameIdentifier), out int docId))
            { return BadRequest(new ApiResponse(401, false, "User Not Found")); }

            return Ok(await _appointmentService.GetDoctorPendingAppointmentsAsync(docId));
        }

        [Authorize(Roles = Roles.Doctor)]
        [HttpGet("Doctor/Confirmed")]
        public async Task<ActionResult<ApiResponse>> GetDoctorConfirmedAppointments()
        {
            if (!int.TryParse(User.FindFirstValue(ClaimTypes.NameIdentifier), out int docId))
            { return BadRequest(new ApiResponse(401, false, "User Not Found")); }

            return Ok(await _appointmentService.GetDoctorConfirmedAppointmentsAsync(docId));
        }

        [Authorize(Roles = Roles.Patient)]
        [HttpGet ("Patient/Pending")]
        public async Task<ActionResult<ApiResponse>> GetPatientPendingAppointments()
        {
            if (!int.TryParse(User.FindFirstValue(ClaimTypes.NameIdentifier), out int patientId))
            { return BadRequest(new ApiResponse(401, false, "User Not Found")); }

            return Ok(await _appointmentService.GetPatientPendingAppointmentsAsync(patientId));
        }

        [Authorize(Roles = Roles.Patient)]
        [HttpGet("Patient/Confirmed")]
        public async Task<ActionResult<ApiResponse>> GetPatientConfirmedAppointments()
        {
            if (!int.TryParse(User.FindFirstValue(ClaimTypes.NameIdentifier), out int patientId))
            { return BadRequest(new ApiResponse(401, false, "User Not Found")); }

            return Ok(await _appointmentService.GetPatientConfirmedAppointmentsAsync(patientId));
        }

        [Authorize(Roles = Roles.Patient)]
        [HttpPost("Reserve")]
        public async Task<ActionResult<ApiResponse>> ReserveAppointment(AppointmentReservationDto dto)
        {
            if (!ModelState.IsValid) { return BadRequest(new ApiResponse(400, false, ModelState)); };

            return Ok(await _appointmentService.ReserveAppointment(dto));
        }
        
        [Authorize(Roles = Roles.Doctor)]
        [HttpPut("Doctor/Action")]
        public async Task<ActionResult<ApiResponse>> DoctorAction(AppointmentActionDto dto)
        {
            if (!ModelState.IsValid) { return BadRequest(new ApiResponse(400, false, ModelState)); };

            return Ok(await _appointmentService.DoctorAppointmentAction(dto));
        }
        
        [Authorize(Roles = Roles.Patient)]
        [HttpPut("Patient/Action")]
        public async Task<ActionResult<ApiResponse>> PatientAction(AppointmentActionDto dto)
        {
            if (!ModelState.IsValid) { return BadRequest(new ApiResponse(400, false, ModelState)); };

            return Ok(await _appointmentService.PatientAppointmentAction(dto));
        }
    }
}
