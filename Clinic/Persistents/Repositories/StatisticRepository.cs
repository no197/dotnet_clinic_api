using Clinic.Core;
using Clinic.Dtos;
using Clinic.Extensions;
using Clinic.Models;
using Clinic.Models.Entities;
using Clinic.Models.Queries;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Clinic.Persistents
{
    public class StatisticRepository : IStatisticRepository
    {
        private readonly ClinicDbContext context;

        public StatisticRepository(ClinicDbContext context)
        {
            this.context = context;
        }

        public async Task<GeneralStatisticDto> GetGeneralStatistic()
        {
            DateTime today = DateTime.Today;

            var numOfPatient =  await context.Patients
                .Where(p => p.CreatedDate.Month == today.Month && p.CreatedDate.Year == today.Year)
                .CountAsync();

            var numOfExam = await context.Examinations
                .Where(ex => ex.CreatedDate.Month == today.Month && ex.CreatedDate.Year == today.Year)
                .CountAsync();

            var numOfInvoice = await context.Invoices
                .Where(iv => iv.CreatedDate.Month == today.Month && iv.CreatedDate.Year == today.Year)
                .CountAsync();
            var examRevenue = numOfInvoice * 100000;

            var TotalRevenue = await context.Invoices
                .Where(iv => iv.CreatedDate.Month == today.Month && iv.CreatedDate.Year == today.Year)
                .SumAsync(iv => iv.Price);

            GeneralStatisticDto generalStatistic = new GeneralStatisticDto
            {
                ExamRevenue = examRevenue,
                NumOfExam = numOfExam,
                NumOfPatient = numOfPatient,
                TotalRevenue = TotalRevenue
            };
            return generalStatistic;
        }

        public async Task<QueryResult<MedicineStatDto>> GetMonthlyMedicine(MonthYearQuery monthYearQuery)
        {
            var result = new QueryResult<MedicineStatDto>();
            int month = monthYearQuery.Month;
            int year = monthYearQuery.Year;

            var query = (from pre in context.PrescriptionDetails
                         join medicine in context.Medicines
                              on pre.MedicineId equals medicine.MedicineId
                         join examination in context.Examinations 
                               on pre.ExaminationId equals examination.ExaminationId
                         where examination.CreatedDate.Month == month && examination.CreatedDate.Year == year
                         group pre by new { pre.MedicineId, medicine.MedicineName, medicine.Unit } into g
                         select new MedicineStatDto
                         {
                             MedicineName = g.Key.MedicineName,
                             QtyUsed = g.Sum(pre => pre.Quantity),
                             TimesUsed = g.Count(),
                             Unit = g.Key.Unit
                         });

            result.TotalItems = await query.CountAsync();

            result.Items = await query.ToListAsync();

            return result;
        }

        public async Task<QueryResult<PatientDto>> GetMonthlyPatients(MonthYearQuery monthYearQuery)
        {
            var result = new QueryResult<PatientDto>();
            int month = monthYearQuery.Month;
            int year = monthYearQuery.Year;

            var query = (from exam in context.Examinations
                         join appoint in context.Appointments
                              on exam.AppointmentId equals appoint.AppointmentId
                         join patient in context.Patients
                               on appoint.PatientId equals patient.PatientId
                         where exam.CreatedDate.Month == month && exam.CreatedDate.Year == year
                         group exam by new { patient.PatientId, patient.FullName, patient.Address, patient.Gender, patient.DateOfBirth, patient.PhoneNumber } into g
                         select new PatientDto
                         {
                            PatientId = g.Key.PatientId,
                            FullName = g.Key.FullName,
                            Address = g.Key.Address,
                            PhoneNumber = g.Key.PhoneNumber,
                            DateOfBirth = g.Key.DateOfBirth,
                            Gender = g.Key.Gender,

                         });

            result.TotalItems = await query.CountAsync();

            result.Items = await query.ToListAsync();

            return result;
        }

        public async Task<QueryResult<RevenueDto>> GetMonthlyRevenue(MonthYearQuery monthYearQuery)
        {
            var result = new QueryResult<RevenueDto>();
            int month = monthYearQuery.Month;
            int year = monthYearQuery.Year;

            var TotalRevenue = await context.Invoices
               .Where(iv => iv.CreatedDate.Month ==month && iv.CreatedDate.Year == year)
               .SumAsync(iv => iv.Price);

            var query = (from iv in context.Invoices
                         where iv.CreatedDate.Month == month && iv.CreatedDate.Year == year
                         group iv by new { Date = iv.CreatedDate.Date } into g
                         orderby g.Key.Date
                         select new RevenueDto
                         {
                             Date = g.Key.Date,
                             Revenue = g.Sum(iv => iv.Price),
                             Percent = Math.Round((double)g.Sum(iv => iv.Price) / TotalRevenue * 100 , 2),

                         });

            result.TotalItems = await query.CountAsync();

            result.Items = await query.ToListAsync();

            return result;
        }


        public async Task<QueryResult<RevenueDto>> GetRevenueInRange(DateRangeQuery dateRangeQuery)
        {
            var result = new QueryResult<RevenueDto>();
            DateTime fromDate = dateRangeQuery.FromDate;
            DateTime toDate = dateRangeQuery.ToDate;

            var TotalRevenue = await context.Invoices
               .Where(iv => iv.CreatedDate.Date >= fromDate.Date && iv.CreatedDate.Date <= toDate.Date)
               .SumAsync(iv => iv.Price);

            var query = (from iv in context.Invoices
                         where iv.CreatedDate.Date >= fromDate.Date && iv.CreatedDate.Date <= toDate.Date
                         group iv by new { iv.CreatedDate.Date } into g
                         orderby g.Key.Date
                         select new RevenueDto
                         {
                             Date = g.Key.Date,
                             Revenue = g.Sum(iv => iv.Price),
                             Percent = Math.Round((double)g.Sum(iv => iv.Price) / TotalRevenue * 100, 2),

                         });

            result.TotalItems = await query.CountAsync();

            result.Items = await query.ToListAsync();

            return result;
        }
    }
}
