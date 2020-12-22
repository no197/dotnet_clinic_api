using Clinic.Core;
using Clinic.Dtos;
using Clinic.Extensions;
using Clinic.Models;
using Clinic.Models.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Clinic.Persistents
{
    public class MedicineRepository : IMedicineRepository
    {
        private readonly ClinicDbContext context;

        public MedicineRepository(ClinicDbContext context)
        {
            this.context = context;
        }


        public async Task<QueryResult<Medicine>> getMedicines(MedicineQuery queryObj)
        {
            var result = new QueryResult<Medicine>();

            var query = context.Medicines.AsQueryable();

            query = query.ApplyFiltering(queryObj);

            var columnsMap = new Dictionary<string, Expression<Func<Medicine, object>>>()
            {
                ["MedicineName"] = Medicine => Medicine.MedicineName
            };
            query = query.ApplyOrdering(queryObj, columnsMap);

            result.TotalItems = await query.CountAsync();

            query = query.ApplyPaging(queryObj);

            result.Items = await query.ToListAsync();

            return result;
        }


        public async Task<Medicine> GetMedicine(int id, bool includeRelated = true)
        {
           return  await context.Medicines.FindAsync(id);

        }


        public void Add(Medicine medicine)
        {
            context.Medicines.Add(medicine);
        }


        public void Update(Medicine medicine)
        {
            context.Medicines.Update(medicine);
        }

        public void Remove(Medicine medicine)
        {
            context.Medicines.Remove(medicine);
        }

    
    }
}
