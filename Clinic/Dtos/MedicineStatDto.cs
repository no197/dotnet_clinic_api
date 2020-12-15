using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Clinic.Dtos
{
    public class MedicineStatDto
    {
       
        public string MedicineName { get; set; }
        public string Unit { get; set; }
        public int QtyUsed { get; set; }
        public int TimesUsed { get; set; }

    }
}
