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
    public class AppointmentRepository : IAppointmentRepository
    {
        private readonly ClinicDbContext context;

        public AppointmentRepository(ClinicDbContext context)
        {
            this.context = context;
        }


        public async Task<QueryResult<Appointment>> getAppointments(AppointmentQuery queryObj)
        {
            var result = new QueryResult<Appointment>();

            var query = context.Appointments.AsQueryable();

            query = query.ApplyFiltering(queryObj);

            var columnsMap = new Dictionary<string, Expression<Func<Appointment, object>>>()
            {
                ["PatientName"] = Appointment => Appointment.Patient.FullName
            };
            query = query.ApplyOrdering(queryObj, columnsMap);

            result.TotalItems = await query.CountAsync();

            query = query.ApplyPaging(queryObj);

            result.Items = await query.ToListAsync();

            return result;
        }


        public async Task<Appointment> GetAppointment(int id, bool includeRelated = true)
        {
          if(!includeRelated)
            {
                return await context.Appointments.FindAsync(id);
            }

            return await context.Appointments
                .Include(a => a.Patient)
                .SingleOrDefaultAsync(a => a.AppointmentId == id);

        }


        public void Add(Appointment appointment)
        {
            context.Appointments.Add(appointment);
        }


        public void Update(Appointment appointment)
        {
            context.Appointments.Update(appointment);
        }

        public void Remove(Appointment appointment)
        {
            context.Appointments.Remove(appointment);
        }

    }
}
