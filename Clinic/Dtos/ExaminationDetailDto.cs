using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Clinic.Dtos
{
    public class ExaminationDetailDto
    {
        public int ExaminationId { get; set; }

        public PatientDto Patient { get; set; }

        public string Diagnose { set; get; }

        public string Symptom { set; get; }

        public string EmployeeName { set; get; }

        public DateTime CreatedDate { set; get; }

        public List<PrescriptionDetailDto> PrescriptionDetails { get; set; }
    }
}
