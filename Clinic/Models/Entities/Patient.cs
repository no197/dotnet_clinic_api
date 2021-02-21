using Clinic.Models.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Clinic.Models
{
    public class Patient
    {
        public int PatientId { get; set; }

        [Required]
        [StringLength(50)]
        public string FullName { get; set; }

        [Required]
        [StringLength(4)]
        public string Gender { get; set; }
        
        [Required]
        public DateTime DateOfBirth { get; set; }

        [Required]
        [StringLength(100)]
        public string Address { get; set; }

        [Required]
        [StringLength(15)]
        public string PhoneNumber { get; set; }
 
        public DateTime CreatedDate { get; set; }

        public ICollection<Appointment> Appointments { get; set; }
    }
}
