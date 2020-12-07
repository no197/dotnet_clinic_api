using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Clinic.Dtos
{
    public class ExaminationDto
    {
        public int ExaminationId { get; set; }

        public string PatientName { get; set; }

        public string Diagnose { set; get; }

        public string Symptom { set; get; }

        public string EmployeeName { set; get; }

        public List<PrescriptionDetailDto> PrescriptionDetails { get; set; }
    }
}
