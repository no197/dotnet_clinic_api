using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Clinic.Models.Entities
{
    public class Examination
    {
        public int ExaminationId { get; set; }

        public int AppointmentId { get; set; }

        public Appointment Appointment { get; set; }

        public string Diagnose { set; get; }

        public string Symptom { set; get; }

        public int EmployeeId { set; get; }
        public Employee  Employee {set; get;}

        public DateTime  CreatedDate { set; get; }

        public Invoice Invoice { get; set; }
        public IList<PrescriptionDetail> PrescriptionDetails { get; set; }
    }
}
