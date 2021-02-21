using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Clinic.Dtos
{
    public class AppointmentDto
    {
        public int AppointmentId { get; set; }
        public DateTime DateOfAppointment { get; set; }

        public int PatientId { set; get; }
        public string PatientName { set; get; }
        public string Status { get; set; }
    }
}
