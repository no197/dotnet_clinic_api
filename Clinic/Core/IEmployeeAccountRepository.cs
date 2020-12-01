using Clinic.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Clinic.Core
{
    public interface IEmployeeRepository
    {
        Task<QueryResult<Employee>> getEmployees(EmployeeQuery filter);
        Task<Employee> GetEmployee(int id, bool includeRelated = true);
        void Add(Employee employee);
        void Update(Employee employee);
        void Remove(Employee employee);
        
    }
}
