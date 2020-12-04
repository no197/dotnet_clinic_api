using Clinic.Models;
using Clinic.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Clinic.Core
{
    public interface IAppointmentRepository
    {
        Task<QueryResult<Appointment>> getAppointments(AppointmentQuery filter);
        Task<Appointment> GetAppointment(int id, bool includeRelated = true);
        void Add(Appointment Appointment);
        void Update(Appointment Appointment);
        void Remove(Appointment Appointment);

    }
}
