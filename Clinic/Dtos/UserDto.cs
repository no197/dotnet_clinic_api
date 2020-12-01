using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Clinic.Dtos
{
    public class UserDto
    {
        public int EmployeeId { get; set; }

        public string FullName { get; set; }

        public string Position { get; set; }

        public string Role { get; set; }

        public string Token { get; set; }

    }
}
