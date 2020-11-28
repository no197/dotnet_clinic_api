using Clinic.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Clinic.Extensions
{
    public static class IPatientQueryableExtension
    {

        public static IQueryable<Patient> ApplyFiltering(this IQueryable<Patient> query, PatientQuery queryObj)
        {
            if (!String.IsNullOrEmpty(queryObj.Name))
                query = query.Where(patient => patient.Name.Contains(queryObj.Name));

            return query;
        }

    }
}

