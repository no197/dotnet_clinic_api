﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Clinic.Models.Entities
{
    public class Medicine
    {
        public int MedicineId { get; set; }

        [Required]
        [StringLength(50)]
        public string MedicineName { get; set; }

        [Required]
        [StringLength(20)]
        public string Unit { get; set; }


        [Required]
        [Range(0, Int32.MaxValue)]
        public int Quantity { get; set; }

        [Required]
        [Range(0, Int32.MaxValue)]
        public int Price { get; set; }

        public IList<PrescriptionDetail> PrescriptionDetails { get; set; }
    }
}
