using Clinic.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Clinic.Extensions
{
    public static class IEmployeeQueryableExtension
    {
        public static IQueryable<Employee> ApplyFiltering(this IQueryable<Employee> query, EmployeeQuery queryObj)
        {
            if (!String.IsNullOrEmpty(queryObj.Name))
                query = query.Where(Employee => Employee.FullName.Contains(queryObj.Name));

            if (!String.IsNullOrEmpty(queryObj.Position))
                query = query.Where(Employee => Employee.Position.Contains(queryObj.Position));

            return query;
        }

       
    }
}

