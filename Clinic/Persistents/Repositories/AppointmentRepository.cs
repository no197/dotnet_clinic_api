using Clinic.Core;
using Clinic.Extensions;
using Clinic.Models;
using Clinic.Models.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Clinic.Persistents
{
    public class ExaminationRepository : IExaminationRepository
    {
        private readonly ClinicDbContext context;

        public ExaminationRepository(ClinicDbContext context)
        {
            this.context = context;
        }


        public async Task<QueryResult<Examination>> getExaminations(ExaminationQuery queryObj)
        {
            var result = new QueryResult<Examination>();

            var query = context.Examinations.AsQueryable();

            query = query.ApplyFiltering(queryObj);

            var columnsMap = new Dictionary<string, Expression<Func<Examination, object>>>()
            {
                ["PatientName"] = Examination => Examination.Appointment.Patient.FullName
            };
            query = query.ApplyOrdering(queryObj, columnsMap);

            result.TotalItems = await query.CountAsync();

            query = query.ApplyPaging(queryObj);

            result.Items = await query.ToListAsync();

            return result;
        }


        public async Task<Examination> GetExamination(int id, bool includeRelated = true)
        {
          if(!includeRelated)
            {
                return await context.Examinations.FindAsync(id);
            }

            return await context.Examinations
                .Include(exam => exam.Employee)
                .Include(exam => exam.Appointment)
                .ThenInclude(a => a.Patient)
                .SingleOrDefaultAsync(a => a.ExaminationId == id);

        }


        public void Add(Examination examination)
        {
            context.Examinations.Add(examination);
        }


        public void Update(Examination examination)
        {
            context.Examinations.Update(examination);
        }

        public void Remove(Examination Examination)
        {
            context.Examinations.Remove(Examination);
        }

    }
}
