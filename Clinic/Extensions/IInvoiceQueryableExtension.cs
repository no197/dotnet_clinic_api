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
    public static class InvoiceQueryableExtension
    {

        public static IQueryable<Invoice> ApplyFiltering(this IQueryable<Invoice> query, InvoiceQuery queryObj)
        {
            query = query
                .Include(invoice => invoice.Examination)
                .ThenInclude(exam => exam.Appointment)
                .ThenInclude(app => app.Patient);

            if (!String.IsNullOrEmpty(queryObj.PatientName))
            {
                query = query.Where(Invoice => Invoice.Examination.Appointment.Patient.FullName.Contains(queryObj.PatientName));
            }

            if (!String.IsNullOrEmpty(queryObj.Status))
            {
                query = query.Where(Invoice => Invoice.Status.Contains(queryObj.Status));
            }


            return query;
        }

    }
}

