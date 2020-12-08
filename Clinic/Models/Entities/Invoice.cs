using Clinic.Models.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Clinic.Models
{
    public class Invoice
    {
        public int InvoiceId { get; set; }

        public int ExaminationId { get; set; }
        public Examination Examination { get; set; }

        public long Price { get; set; }

        public string Status { get; set; }

        public DateTime CreatedDate { get; set; }

    }
}
