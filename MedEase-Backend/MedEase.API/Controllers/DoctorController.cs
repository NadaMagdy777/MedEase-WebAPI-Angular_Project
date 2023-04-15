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

        [HttpGet]
        public async Task<IActionResult> getAll()
        {

            return Ok(await _doctorService.GetAll());
        }
        [HttpPut]
        public async Task<IActionResult> Edit(DoctorEditDto doctor,int id)
        {

            return Ok(await _doctorService.EditDoctor(doctor,id));
        }
        [HttpPost("SubSpeciality")]
        public async Task<IActionResult> AddDoctorSubSpeciality(int docID ,SubspecialityDto subspeciality)
        {
            return Ok(await _doctorService.AddDoctorSubspiciality(docID,subspeciality));
        }
    }
}
