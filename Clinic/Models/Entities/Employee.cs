using Clinic.Models.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Clinic.Models
{
    public class Employee
    {
        public int EmployeeId { get; set; }

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
        [StringLength(20)]
        public string Position { get; set; }
 
        public DateTime CreatedDate { get; set; }

        public EmployeeAccount employeeAccount { get; set; }
    }
}
