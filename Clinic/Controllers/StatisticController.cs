﻿using AutoMapper;
using Clinic.Core;
using Clinic.Dtos;
using Clinic.Models;
using Clinic.Models.Entities;
using Clinic.Models.Queries;
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
    [Route("/api/statistic")]
    public class StatisticController : ControllerBase
    {
        private readonly IStatisticRepository repository;
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper;


        public StatisticController(IStatisticRepository repository, IUnitOfWork unitOfWork, IMapper mapper)
        {
            this.repository = repository;
            this.unitOfWork = unitOfWork;
            this.mapper = mapper;
        }


        [HttpGet("general")]
        public async Task<IActionResult> GetGeneralStatistic()
        {
            var statistic = await repository.GetGeneralStatistic();

            return Ok(statistic);
        }

        [HttpGet("monthlyRevenue")]
        public async Task<ActionResult<QueryResultDto<RevenueDto>>> GetMonthlyRevenue([FromQuery] MonthYearQuery query)
        {
            var filter = await repository.GetMonthlyRevenue(query);

            return Ok(filter);
        }

        [HttpGet("monthlyMedicines")]
        public async Task<ActionResult<QueryResultDto<MedicineStatDto>>> GetMonthlyMedicines([FromQuery] MonthYearQuery query)
        {
            var filter = await repository.GetMonthlyMedicine(query);

            return Ok(filter);
        }

        [HttpGet("monthlyPatients")]
        public async Task<ActionResult<QueryResultDto<MedicineStatDto>>> GetMonthlyPatients([FromQuery] MonthYearQuery query)
        {
            var filter = await repository.GetMonthlyPatients(query);

            return Ok(filter);
        }

        [HttpGet("revenueInRange")]
        public async Task<ActionResult<QueryResultDto<MedicineStatDto>>> GetRevenueInRange([FromQuery] DateRangeQuery query)
        {
            var filter = await repository.GetRevenueInRange(query);

            return Ok(filter);
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


    }
}
