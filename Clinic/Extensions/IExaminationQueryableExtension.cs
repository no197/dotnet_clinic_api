using Clinic.Models;
using Clinic.Models.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Clinic.Extensions
{
    public static class IExaminationQueryableExtension
    {

        public static IQueryable<Examination> ApplyFiltering(this IQueryable<Examination> query, ExaminationQuery queryObj)
        {
            query = query
                   .Include(Examination => Examination.Employee)
                   .Include(Examination => Examination.Appointment)
                   .ThenInclude(Appointment => Appointment.Patient);

            if (!String.IsNullOrEmpty(queryObj.PatientName))
            {
                query = query.Where(Examination => Examination.Appointment.Patient.FullName.Equals(queryObj.PatientName));
            }
               

            return query;
        }

    }
}

