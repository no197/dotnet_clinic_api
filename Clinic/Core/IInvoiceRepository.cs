using Clinic.Models;
using Clinic.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Clinic.Core
{
    public interface IInvoiceRepository
    {
        Task<QueryResult<Invoice>> GetInvoices(InvoiceQuery filter);
        Task<Invoice> GetInvoice(int id, bool includeRelated = true);
        void Add(Invoice Invoice);
        void Update(Invoice Invoice);
        void Remove(Invoice Invoice);

    }
}
