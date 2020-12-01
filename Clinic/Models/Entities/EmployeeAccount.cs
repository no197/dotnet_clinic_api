using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Clinic.Models.Entities
{
    public class EmployeeAccount
    {
        public int Id { get; set; }
        public string Email { get; set; }
        public byte[] PasswordHash { get; set; }
        public byte[] PasswordSalt { get; set; }
        public string Role { get; set; }
        public int EmployeeId { get; set; }
        public Employee Employee { get; set; }
    }

}
