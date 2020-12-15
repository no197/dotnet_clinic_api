using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Clinic.Dtos
{
    public class InvoiceDto
    {
        public int InvoiceId { get; set; }

        public string PatientName { get; set; }

        public long Price { get; set; }


        public string Status { get; set; }

        public DateTime CreatedDate { get; set; }

    }
}
