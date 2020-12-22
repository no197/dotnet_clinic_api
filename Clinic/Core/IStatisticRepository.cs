using Clinic.Dtos;
using Clinic.Models;
using Clinic.Models.Queries;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Clinic.Core
{
    public interface IStatisticRepository
    {
        Task<GeneralStatisticDto> GetGeneralStatistic();
        Task<QueryResult<RevenueDto>> GetMonthlyRevenue(MonthYearQuery query);
        Task<QueryResult<MedicineStatDto>> GetMonthlyMedicine(MonthYearQuery query);
        Task<QueryResult<PatientDto>> GetMonthlyPatients(MonthYearQuery query);

        Task<QueryResult<RevenueDto>> GetRevenueInRange(DateRangeQuery query);

        Task<QueryResult<MedicineStatDto>> TopFiveMedicineUsed();
        Task<QueryResult<MedicineStatDto>> TopFiveMedicineQuantityUsed();
    }
}
