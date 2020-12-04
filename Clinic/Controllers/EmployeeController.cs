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

    [ApiController]
    [Route("/api/employees")]
    [Authorize(Roles = Role.Admin)]
    public class EmployeeController : ControllerBase
    {
        private readonly IEmployeeRepository repository;
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper;


        public EmployeeController(IEmployeeRepository EmployeeRepository, IUnitOfWork unitOfWork, IMapper mapper)
        {
            this.repository = EmployeeRepository;
            this.unitOfWork = unitOfWork;
            this.mapper = mapper;
        }

        [HttpGet]
        [ProducesResponseType(200)]
        public async Task<ActionResult<QueryResultDto<EmployeeDto>>> GetEmployees([FromQuery]  EmployeeQuery employeeQuery)
        {
            var filter = await repository.getEmployees(employeeQuery);

            return Ok(mapper.Map<QueryResult<Employee>, QueryResultDto<EmployeeDto>>(filter));
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetEmployee(int id)
        {
            var employee = await repository.GetEmployee(id);

            if (employee == null)
                return NotFound();

            var EmployeeResult = mapper.Map<Employee, EmployeeDto>(employee);

            return Ok(EmployeeResult);
        }

        [HttpPost]
        public async Task<IActionResult> CreateEmployee(EmployeeDto employeeDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var employee = mapper.Map<EmployeeDto, Employee>(employeeDto);
            employee.CreatedDate = DateTime.Now;

            repository.Add(employee);
            await unitOfWork.CompleteAsync();

            employee = await repository.GetEmployee(employee.EmployeeId);

            var result = mapper.Map<Employee, EmployeeDto>(employee);

            return CreatedAtAction(nameof(GetEmployee), new { id = result.EmployeeId }, result);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateEmployee(int id, EmployeeDto employeeDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var employee = await repository.GetEmployee(id);

            if (employee == null)
                return NotFound();

            mapper.Map<EmployeeDto, Employee>(employeeDto, employee);

            await unitOfWork.CompleteAsync();

            employee = await repository.GetEmployee(employee.EmployeeId);
            var result = mapper.Map<Employee, EmployeeDto>(employee);

            return Ok(result);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteEmployee(int id)
        {
            var employee = await repository.GetEmployee(id, includeRelated: false);

            if (employee == null)
                return NotFound();

            repository.Remove(employee);
            await unitOfWork.CompleteAsync();

            return new NoContentResult();
        }

    }
}
