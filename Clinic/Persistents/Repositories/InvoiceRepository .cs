using Clinic.Core;
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
    public class InvoiceRepository : IInvoiceRepository
    {
        private readonly ClinicDbContext context;

        public InvoiceRepository(ClinicDbContext context)
        {
            this.context = context;
        }


        public async Task<QueryResult<Invoice>> GetInvoices(InvoiceQuery queryObj)
        {
            var result = new QueryResult<Invoice>();

            var query = context.Invoices.AsQueryable();

            query = query.ApplyFiltering(queryObj);

            var columnsMap = new Dictionary<string, Expression<Func<Invoice, object>>>()
            {
                ["PatientName"] = Invoice => Invoice.Examination.Appointment.Patient.FullName,
                ["Status"] = Invoice => Invoice.Status,

            };
            query = query.ApplyOrdering(queryObj, columnsMap);

            result.TotalItems = await query.CountAsync();

            query = query.ApplyPaging(queryObj);

            result.Items = await query.ToListAsync();

            return result;
        }


        public async Task<Invoice> GetInvoice(int id, bool includeRelated = true)
        {
          if(!includeRelated)
            {
                return await context.Invoices.FindAsync(id);
            }

            return await context.Invoices 
                .Include(invoice => invoice.Examination)
                .ThenInclude(exam => exam.Employee)

                .Include(invoice => invoice.Examination)
                .ThenInclude(exam => exam.PrescriptionDetails)
                .ThenInclude(pre => pre.Medicine)

                .Include(invoice => invoice.Examination)
                .ThenInclude(exam => exam.Appointment)
                .ThenInclude(app => app.Patient)
                .SingleOrDefaultAsync(a => a.InvoiceId == id);

        }


        public void Add(Invoice Invoice)
        {
            context.Invoices.Add(Invoice);
        }


        public void Update(Invoice Invoice)
        {
            context.Invoices.Update(Invoice);
        }

        public void Remove(Invoice Invoice)
        {
            context.Invoices.Remove(Invoice);
        }

    }
}
