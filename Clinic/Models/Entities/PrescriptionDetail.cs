using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Clinic.Models.Entities
{
    public class PrescriptionDetail
    {
        public int ExaminationId { get; set; }
        public Examination Examination { get; set; }
        public int MedicineId { get; set; }
        public Medicine Medicine { get; set; }
        public int MedicinePrice { get; set; }
        public long TotalPrice { get; set; }
        public int Quantity { get; set; }
        public string Instruction { get; set; }


    }
}
