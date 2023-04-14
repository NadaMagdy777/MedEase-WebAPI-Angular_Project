using MedEase.Core.Dtos;
using MedEase.Core.Models;
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
    }
}
