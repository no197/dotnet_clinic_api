using AutoMapper;
using Clinic.Core;
using Clinic.Dtos;
using Clinic.Errors;
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
    [Route("/api/examinations")]
    public class ExaminationController : ControllerBase
    {
        private readonly IExaminationRepository examinationRepository;
        private readonly IPrescriptionDetailRepository prescriptionDetailRepository;
        private readonly IMedicineRepository medicineRepository;
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper;


       
        public ExaminationController(IExaminationRepository ExaminationRepository, 
                                    IPrescriptionDetailRepository prescriptionDetailRepository,
                                     IMedicineRepository medicineRepository,
                                    IUnitOfWork unitOfWork, IMapper mapper)
        {
            this.examinationRepository = ExaminationRepository;
            this.prescriptionDetailRepository = prescriptionDetailRepository;
            this.medicineRepository = medicineRepository;
            this.unitOfWork = unitOfWork;
            this.mapper = mapper;
        }

        [HttpGet]
        [ProducesResponseType(200)]
        public async Task<ActionResult<QueryResultDto<ExaminationDto>>> GetExaminations([FromQuery]  ExaminationQuery ExaminationQuery)
        {
            var filter = await examinationRepository.getExaminations(ExaminationQuery);

            return Ok(mapper.Map<QueryResult<Examination>, QueryResultDto<ExaminationDto>>(filter));
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetExamination(int id)
        {
            var Examination = await examinationRepository.GetExamination(id);

            if (Examination == null)
                return NotFound();

            var ExaminationResult = mapper.Map<Examination, ExaminationDto>(Examination);

            return Ok(ExaminationResult);
        }

        [HttpPost]
        public async Task<IActionResult> CreateExamination(ExaminationSaveDto examinationDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var examination = mapper.Map<ExaminationSaveDto, Examination>(examinationDto);
            examinationRepository.Add(examination);

            var prescriptionDetailsDtos = examinationDto.PrescriptionDetails;
            var prescriptionDetails = mapper.Map<List<PrescriptionDetailSaveDto>, List<PrescriptionDetail>>(prescriptionDetailsDtos);

            foreach (var prescriptionDetail in prescriptionDetails)
            {
                var medicine = await medicineRepository.GetMedicine(prescriptionDetail.MedicineId);

                var medicineQty = medicine.Quantity;
                string medicineName = medicine.MedicineName;
                if (medicineQty < prescriptionDetail.Quantity)
                    return BadRequest(new ApiResponse(400,$"{medicineName} không đủ số lượng. Vui lòng giảm số lượng hoặc dùng thuốc khác"));
                prescriptionDetail.Examination = examination;
                prescriptionDetailRepository.Add(prescriptionDetail);

            }
           

            await unitOfWork.CompleteAsync();

            examination = await examinationRepository.GetExamination(examination.ExaminationId);

            var result = mapper.Map<Examination, ExaminationDto>(examination);

            return CreatedAtAction(nameof(GetExamination), new { id = result.ExaminationId }, result);
        }

        //Update status apointment
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateExamination(int id, ExaminationSaveDto ExaminationDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var examination = await examinationRepository.GetExamination(id);

            if (examination == null)
                return NotFound();

            mapper.Map<ExaminationSaveDto, Examination>(ExaminationDto, examination);

            await unitOfWork.CompleteAsync();

            examination = await examinationRepository.GetExamination(examination.ExaminationId);
            var result = mapper.Map<Examination, ExaminationDto>(examination);

            return Ok(result);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteExamination(int id)
        {
            var Examination = await examinationRepository.GetExamination(id, includeRelated: false);

            if (Examination == null)
                return NotFound();

            examinationRepository.Remove(Examination);
            await unitOfWork.CompleteAsync();

            return new NoContentResult();
        }

    }
}
