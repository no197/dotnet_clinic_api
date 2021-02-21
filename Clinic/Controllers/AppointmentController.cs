using AutoMapper;
using Clinic.Core;
using Clinic.Dtos;
using Clinic.Models;
using Clinic.Models.Entities;
using Clinic.Persistents;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Clinic.Controllers
{
    [Authorize(Roles = Role.Admin + "," + Role.Employee + "," + Role.Doctor)]
    [ApiController]
    [Route("/api/appointments")]
    public class AppointmentController : ControllerBase
    {
        private readonly IAppointmentRepository repository;
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper;


       
        public AppointmentController(IAppointmentRepository AppointmentRepository, IUnitOfWork unitOfWork, IMapper mapper)
        {
            this.repository = AppointmentRepository;
            this.unitOfWork = unitOfWork;
            this.mapper = mapper;
        }

        [HttpGet]
        [ProducesResponseType(200)]
        public async Task<ActionResult<QueryResultDto<AppointmentDto>>> GetAppointments([FromQuery]  AppointmentQuery AppointmentQuery)
        {
            var filter = await repository.getAppointments(AppointmentQuery);

            return Ok(mapper.Map<QueryResult<Appointment>, QueryResultDto<AppointmentDto>>(filter));
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetAppointment(int id)
        {
            var Appointment = await repository.GetAppointment(id);

            if (Appointment == null)
                return NotFound();

            var AppointmentResult = mapper.Map<Appointment, AppointmentDto>(Appointment);

            return Ok(AppointmentResult);
        }

        [HttpPost]
        public async Task<IActionResult> CreateAppointment(AppointmentSaveDto appointmentDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var appointment = mapper.Map<AppointmentSaveDto, Appointment>(appointmentDto);
            if(String.IsNullOrEmpty(appointment.Status))
            {
                appointment.Status = "Đang chờ";
            }

            repository.Add(appointment);
            await unitOfWork.CompleteAsync();

            appointment = await repository.GetAppointment(appointment.AppointmentId);

            var result = mapper.Map<Appointment, AppointmentDto>(appointment);

            return CreatedAtAction(nameof(GetAppointment), new { id = result.AppointmentId }, result);
        }

        //Update status apointment
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateAppointment(int id, AppointmentSaveDto appointmentDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var Appointment = await repository.GetAppointment(id);

            if (Appointment == null)
                return NotFound();

            mapper.Map<AppointmentSaveDto, Appointment>(appointmentDto, Appointment);

            await unitOfWork.CompleteAsync();

            Appointment = await repository.GetAppointment(Appointment.AppointmentId);
            var result = mapper.Map<Appointment, AppointmentDto>(Appointment);

            return Ok(result);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAppointment(int id)
        {
            var Appointment = await repository.GetAppointment(id, includeRelated: false);

            if (Appointment == null)
                return NotFound();

            repository.Remove(Appointment);
            await unitOfWork.CompleteAsync();

            return new NoContentResult();
        }

    }
}
