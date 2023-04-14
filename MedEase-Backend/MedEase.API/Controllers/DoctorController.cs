﻿using MedEase.Core.Models;
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






        //public IActionResult getAppointmentAndPattern()
        //{
        //    return Ok(""); // call Function 
        //}

    }
}
