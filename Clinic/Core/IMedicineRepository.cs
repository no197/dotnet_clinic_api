using Clinic.Dtos;
using Clinic.Models;
using Clinic.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Clinic.Core
{
    public interface IMedicineRepository
    {
        Task<QueryResult<Medicine>> getMedicines(MedicineQuery filter);
        Task<Medicine> GetMedicine(int id, bool includeRelated = true);

        Task<QueryResult<MedicineStatDto>> TopFiveMedicineUsed();
        Task<QueryResult<MedicineStatDto>> TopFiveMedicineQuantityUsed();
        void Add(Medicine medicine);
        void Update(Medicine medicine);
        void Remove(Medicine medicine);

    }
}
