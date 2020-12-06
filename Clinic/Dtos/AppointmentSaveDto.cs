using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Clinic.Dtos
{
    public class AppointmentSaveDto
    {
        public int AppointmentId { get; set; }

        [Required]
        public DateTime DateOfAppointment { get; set; }
        [Required]
        public int PatientId { set; get; }
        [Required]
        [StringLength(30)]
        public string Status { get; set; }
    }
}
