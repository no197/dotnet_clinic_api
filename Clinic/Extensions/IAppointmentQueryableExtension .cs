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
    public static class IAppointmentQueryableExtension
    {

        public static IQueryable<Appointment> ApplyFiltering(this IQueryable<Appointment> query, AppointmentQuery queryObj)
        {
            if (!String.IsNullOrEmpty(queryObj.PatientName))
            {
                query = query
                   .Include(Appointment => Appointment.Patient)
                   .Where(appointment => appointment.Patient.FullName.Equals(queryObj.PatientName));
            }
               

            return query.Include(Appointment => Appointment.Patient);
        }

    }
}

