using Clinic.Models.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Clinic.Dtos
{
    public class PrescriptionDetailSaveDto
    {
        public int MedicineId { get; set; }
        public Medicine  Medicine { get; set; }

        [Required]
        [Range(0, Int32.MaxValue)]
        public int Quantity { get; set; }
        [Required]
        [StringLength(100)]
        public string Instruction { get; set; }
    }
}
