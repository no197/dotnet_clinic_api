using Clinic.Models;
using Clinic.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Clinic.Core
{
    public interface IEmployeeAccountRepository
    {
        EmployeeAccount Authenticate(string email, string password);
        EmployeeAccount Create(EmployeeAccount employeeAccount, string password);
        void Delete(int id);
    }
}
