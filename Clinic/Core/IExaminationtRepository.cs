using Clinic.Models;
using Clinic.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Clinic.Core
{
    public interface IExaminationRepository
    {
        Task<QueryResult<Examination>> getExaminations(ExaminationQuery filter);
        Task<Examination> GetExamination(int id, bool includeRelated = true);
        void Add(Examination examination);
        void Update(Examination examination);
        void Remove(Examination examination);

    }
}
