using Clinic.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Clinic.Core
{
    public interface IPatientRepository
    {
        Task<QueryResult<Patient>> getPatients(PatientQuery filter);
        Task<Patient> GetPatient(int id, bool includeRelated = true);
        void Add(Patient patient);
        void Update(Patient patient);
        void Remove(Patient patient);
        
    }
}
