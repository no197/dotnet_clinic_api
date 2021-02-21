using Clinic.Core;
using Clinic.Extensions;
using Clinic.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Clinic.Persistents
{
    public class EmployeeRepository : IEmployeeRepository
    {
        private readonly ClinicDbContext context;

        public EmployeeRepository(ClinicDbContext context)
        {
            this.context = context;
        }


        public async Task<QueryResult<Employee>> getEmployees(EmployeeQuery queryObj)
        {
            var result = new QueryResult<Employee>();

            var query = context.Employees.AsQueryable();

            query = query.ApplyFiltering(queryObj);

            var columnsMap = new Dictionary<string, Expression<Func<Employee, object>>>()
            {
                ["Name"] = Employee => Employee.FullName,
                ["Position"] = Employee => Employee.Position,
            };
            query = query.ApplyOrdering(queryObj, columnsMap);

            result.TotalItems = await query.CountAsync();

            query = query.ApplyPaging(queryObj);

            result.Items = await query.ToListAsync();

            return result;
        }


        public async Task<Employee> GetEmployee(int id, bool includeRelated = true)
        {
           return  await context.Employees.FindAsync(id);

        }


        public void Add(Employee employee)
        {
            context.Employees.Add(employee);
        }


        public void Update(Employee employee)
        {
            context.Employees.Update(employee);
        }

        public void Remove(Employee employee)
        {
            context.Employees.Remove(employee);
        }

    }
}
