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
    [Authorize(Roles = Role.Admin + "," + Role.Pharmacist)]
    [ApiController]
    [Route("/api/medicines")]
    public class MedicineController : ControllerBase
    {
        private readonly IMedicineRepository repository;
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper;


        public MedicineController(IMedicineRepository repository, IUnitOfWork unitOfWork, IMapper mapper)
        {
            this.repository = repository;
            this.unitOfWork = unitOfWork;
            this.mapper = mapper;
        }

        [HttpGet]
        [ProducesResponseType(200)]
        public async Task<ActionResult<QueryResultDto<MedicineDto>>> GetMedicines([FromQuery]  MedicineQuery MedicineQuery)
        {
            var filter = await repository.getMedicines(MedicineQuery);

            return Ok(mapper.Map<QueryResult<Medicine>, QueryResultDto<MedicineDto>>(filter));
        }



        [HttpGet("topFiveUsed")]
        public async Task<ActionResult<QueryResultDto<MedicineStatDto>>> GetTopFiveMedicinesUsed()
        {
            var topTimesUsedMedicine = await repository.TopFiveMedicineUsed();

            return Ok(topTimesUsedMedicine);
        }

        [HttpGet("topFiveQuantityUsed")]
        public async Task<ActionResult<QueryResultDto<MedicineStatDto>>> GetTopFiveQuantityMedicinesUsed()
        {
            var topQtyUsedMedicine = await repository.TopFiveMedicineQuantityUsed();

            return Ok(topQtyUsedMedicine);
        }


        [HttpGet("{id}")]
        public async Task<IActionResult> GetMedicine(int id)
        {
            var Medicine = await repository.GetMedicine(id);

            if (Medicine == null)
                return NotFound();

            var MedicineResult = mapper.Map<Medicine, MedicineDto>(Medicine);

            return Ok(MedicineResult);
        }





        [HttpPost]
        public async Task<IActionResult> CreateMedicine(MedicineDto medicineDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var medicine = mapper.Map<MedicineDto, Medicine>(medicineDto);
           

            repository.Add(medicine);
            await unitOfWork.CompleteAsync();

            medicine = await repository.GetMedicine(medicine.MedicineId);

            var result = mapper.Map<Medicine, MedicineDto>(medicine);

            return CreatedAtAction(nameof(GetMedicine), new { id = result.MedicineId }, result);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateMedicine(int id, MedicineDto medicineDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var medicine = await repository.GetMedicine(id);

            if (medicine == null)
                return NotFound();

            mapper.Map<MedicineDto, Medicine>(medicineDto, medicine);

            await unitOfWork.CompleteAsync();

            medicine = await repository.GetMedicine(medicine.MedicineId);
            var result = mapper.Map<Medicine, MedicineDto>(medicine);

            return Ok(result);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteMedicine(int id)
        {
            var medicine = await repository.GetMedicine(id, includeRelated: false);

            if (medicine == null)
                return NotFound();

            repository.Remove(medicine);
            await unitOfWork.CompleteAsync();

            return new NoContentResult();
        }

    }
}
