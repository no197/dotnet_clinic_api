using Clinic.Models;
using Clinic.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Clinic.Extensions
{
    public static class IMedicineQueryableExtension
    {
        public static IQueryable<Medicine> ApplyFiltering(this IQueryable<Medicine> query, MedicineQuery queryObj)
        {
            if (!String.IsNullOrEmpty(queryObj.MedicineName))
                query = query.Where(Medicine => Medicine.MedicineName.Contains(queryObj.MedicineName));

            return query;
        }
       
    }
}

