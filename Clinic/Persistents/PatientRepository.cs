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
    public class PatientRepository : IPatientRepository
    {
        private readonly ClinicDbContext context;

        public PatientRepository(ClinicDbContext context)
        {
            this.context = context;
        }


        public async Task<QueryResult<Patient>> getPatients(PatientQuery queryObj)
        {
            var result = new QueryResult<Patient>();

            var query = context.Patients.AsQueryable();

            query = query.ApplyFiltering(queryObj);

            var columnsMap = new Dictionary<string, Expression<Func<Patient, object>>>()
            {
                ["Name"] = patient => patient.Name
            };
            query = query.ApplyOrdering(queryObj, columnsMap);

            result.TotalItems = await query.CountAsync();

            query = query.ApplyPaging(queryObj);

            result.Items = await query.ToListAsync();

            return result;
        }


        public async Task<Patient> GetPatient(int id, bool includeRelated = true)
        {
           return  await context.Patients.FindAsync(id);

        }


        public void Add(Patient patient)
        {
            context.Patients.Add(patient);
        }


        public void Update(Patient patient)
        {
            context.Patients.Update(patient);
        }

        public void Remove(Patient patient)
        {
            context.Patients.Remove(patient);
        }

    }
}
