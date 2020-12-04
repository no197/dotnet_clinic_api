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
    [Authorize(Roles = Role.Admin + "," + Role.Employee)]
    [ApiController]
    [Route("/api/patients")]
    public class PatientController : ControllerBase
    {
        private readonly IPatientRepository repository;
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper;


       
        public PatientController(IPatientRepository patientRepository, IUnitOfWork unitOfWork, IMapper mapper)
        {
            this.repository = patientRepository;
            this.unitOfWork = unitOfWork;
            this.mapper = mapper;
        }

        [HttpGet]
        [ProducesResponseType(200)]
        public async Task<ActionResult<QueryResultDto<PatientDto>>> GetPatients([FromQuery]  PatientQuery patientQuery)
        {
            var filter = await repository.getPatients(patientQuery);

            return Ok(mapper.Map<QueryResult<Patient>, QueryResultDto<PatientDto>>(filter));
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetPatient(int id)
        {
            var patient = await repository.GetPatient(id);

            if (patient == null)
                return NotFound();

            var patientResult = mapper.Map<Patient, PatientDto>(patient);

            return Ok(patientResult);
        }

        [HttpPost]
        public async Task<IActionResult> CreatePatient(PatientDto patientDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var patient = mapper.Map<PatientDto, Patient>(patientDto);
            patient.CreatedDate = DateTime.Now;

            repository.Add(patient);
            await unitOfWork.CompleteAsync();

            patient = await repository.GetPatient(patient.PatientId);

            var result = mapper.Map<Patient, PatientDto>(patient);

            return CreatedAtAction(nameof(GetPatient), new { id = result.PatientId }, result);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateVehicle(int id, PatientDto patientDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var patient = await repository.GetPatient(id);

            if (patient == null)
                return NotFound();

            mapper.Map<PatientDto, Patient>(patientDto, patient);

            await unitOfWork.CompleteAsync();

            patient = await repository.GetPatient(patient.PatientId);
            var result = mapper.Map<Patient, PatientDto>(patient);

            return Ok(result);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePatient(int id)
        {
            var patient = await repository.GetPatient(id, includeRelated: false);

            if (patient == null)
                return NotFound();

            repository.Remove(patient);
            await unitOfWork.CompleteAsync();

            return new NoContentResult();
        }

    }
}
