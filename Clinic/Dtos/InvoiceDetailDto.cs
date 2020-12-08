using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Clinic.Dtos
{
    public class InvoiceDetailDto
    {
        public int InvoiceId { get; set; }

        public int ExaminationId { get; set; }

        public string PatientName { get; set; }

        public string EmployeeName { get; set; }

        public long Price { get; set; }

        public List<PrescriptionDetailInVoiceDto> PrescriptionDetailsPrice { get; set; }

        public string Status { get; set; }

        public DateTime CreatedDate { get; set; }

    }
}
