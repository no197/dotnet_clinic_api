using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Clinic.Dtos
{
    public class ExaminationSaveDto
    {
        public int ExaminationId { get; set; }

        [Required]
        public int AppointmentId { get; set; }

        [Required]
        [StringLength(100)]
        public string Diagnose { set; get; }

        [Required]
        [StringLength(100)]
        public string Symptom { set; get; }


        [Required]
        public int EmployeeId { set; get; }

        public List<PrescriptionDetailSaveDto> PrescriptionDetails { get; set; }
    }
}
