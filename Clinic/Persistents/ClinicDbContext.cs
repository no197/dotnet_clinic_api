using Clinic.Models;
using Clinic.Models.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Clinic.Persistents
{
    public class ClinicDbContext :DbContext
    {
        public ClinicDbContext(DbContextOptions<ClinicDbContext> options) : base(options)
        {

        }


        public DbSet<Patient> Patients { get; set; }
        public DbSet<Employee> Employees { get; set; }
        public DbSet<EmployeeAccount> EmployeeAcounts { get; set; }
    }
}
